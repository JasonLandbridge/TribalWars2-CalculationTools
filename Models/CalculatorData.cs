using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Documents;
using TribalWars2_CalculationTools.Annotations;
using TribalWars2_CalculationTools.Class;
using TribalWars2_CalculationTools.Class.Enum;
using TribalWars2_CalculationTools.Class.Units;
using TribalWars2_CalculationTools.ViewModels;

namespace TribalWars2_CalculationTools.Models
{
    public class CalculatorData : INotifyPropertyChanged
    {

        private BattleResult _lastBattleResult = new BattleResult();

        public BattleResult LastBattleResult
        {
            get => _lastBattleResult;
            set
            {
                _lastBattleResult = value;
                OnPropertyChanged();

            }
        }

        private BattleResultViewModel _battleResultViewModel;

        public CalculatorData(BattleResultViewModel BattleResultViewModel)
        {
            _battleResultViewModel = BattleResultViewModel;
        }


        public int WallLevelBeforeBattle(int ramNumber, int wallLevel, decimal faithBonus, bool paladinMorningStar)
        {
            //Taken from http://www.ds-pro.de/2/simulator.php
            //Todo take into account the MorningStar weapon levels

            decimal morningStarModifier = (paladinMorningStar ? 2 : 1);

            decimal ramAtkStrength = (ramNumber * faithBonus * morningStarModifier);

            decimal a = wallLevel - Math.Round(ramAtkStrength / (4 * (decimal)Math.Pow(1.09, wallLevel)));
            decimal b = Math.Round(wallLevel / (2 * morningStarModifier));
            int newWall = (int)Math.Round(Math.Max(a, b), MidpointRounding.ToZero);
            return newWall;
        }

        public void SimulateBattle(InputCalculatorData input)
        {


            // Based on: Tribal Wars 2 - Tutorial: Basic Battle System - https://www.youtube.com/watch?v=SG_qI1-go88
            // Based on: Battle Simulator - http://www.ds-pro.de/2/simulator.php
            BattleResult result = new BattleResult(input);

            decimal atkFaithBonus = input.InputAtkChurch.Modifier;
            decimal defFaithBonus = input.InputDefChurch.Modifier;

            int wallLevel = input.InputWall;
            decimal morale = input.InputMorale / 100m;
            decimal luck = 1m + (input.InputLuck / 100m);
            decimal nightBonus = (input.InputNightBonus ? 2m : 1m);
            decimal officerBonus = (input.InputGrandmasterBonus ? 0.1m : 0m);

            // 5% for every wall level
            decimal wallBonus = 1m + wallLevel * 0.05m;

            // Based on the wiki https://en.wiki.tribalwars2.com/index.php?title=Battles
            // Math round is important 0.545 -> 0.55
            decimal x = Math.Round(atkFaithBonus * morale * luck, 2, MidpointRounding.AwayFromZero);
            decimal atkModifier = 1m * x + officerBonus;
            result.AtkBattleModifier = (int)(atkModifier * 100m);

            decimal y = Math.Round(defFaithBonus * wallBonus * nightBonus, 2, MidpointRounding.AwayFromZero);
            decimal defModifier = 1m * y;
            result.DefBattleModifier = (int)(defModifier * 100m);

            // Stop here if there are no units given
            if (!input.IsValid)
            {
                LastBattleResult = result;

                _battleResultViewModel.UpdateBattleResult(LastBattleResult);

                return;
            }

            int atkInfantryProvisions = result.GetTotalInfantryProvisions();
            int atkCavalryProvisions = result.GetTotalCavalryProvisions();
            int atkArchersProvisions = result.GetTotalArcherProvisions();
            List<int> atkProvisionTypeList = new List<int>
            {
                atkInfantryProvisions,
                atkCavalryProvisions,
                atkArchersProvisions
            };
            int totalAtkProvisions = atkInfantryProvisions + atkCavalryProvisions + atkArchersProvisions;
            int totalDefProvisions = result.GetTotalDefProvisions();

            // Determines if the Berserker fights with double strength
            bool defSuperior = (totalAtkProvisions < totalDefProvisions / 2);


            List<int> attackTypeList = new List<int>
            {
                atkInfantry,
                atkCavalry,
                atkArchers
            };

            int defInfantry = result.GetTotalDefFromInfantry();
            int defCavalry = result.GetTotalDefFromCavalry();
            int defArchers = result.GetTotalDefFromArchers();
            List<int> defenseTypeList = new List<int>
            {
                defInfantry,
                defCavalry,
                defArchers
            };
            int defenseStrength = defInfantry + defCavalry + defArchers;


            int resultingWallLevel = WallLevelBeforeBattle(result.AtkRam, wallLevel, atkFaithBonus, false);

            int wallDefense = 0;

            if (resultingWallLevel > 0)
            {
                wallDefense = (int)Math.Round(Math.Pow(1.24, resultingWallLevel) * 20, MidpointRounding.AwayFromZero);
            }

            List<BattleResult> BattleHistory = new List<BattleResult>();

            // Simulate for 3 rounds (infantry, cavalry and archers)
            for (int i = 0; i < 2; i++)
            {
                BattleHistory.Add(result.Copy());
                BattleResult currentRound = BattleHistory[i];



                int atkInfantry = currentRound.GetTotalInfantryAttack(defSuperior);
                int atkCavalry = currentRound.GetTotalCavalryAttack();
                int atkArchers = currentRound.GetTotalArcherAttack();

                int attackStrength = attackTypeList[i];
                int attackProvisions = atkProvisionTypeList[i];

                // The Paladin fights with the strongest (highest fighting power) group. 
                // The weapon boost is applied based on the type and not if he joins the round or not. 
                if (attackStrength == attackTypeList.Max())
                {
                    attackStrength += GameData.Paladin.FightingPower;
                    // TODO It is assumed that the Paladin provision is counted towards the group it joins
                    attackProvisions += GameData.Paladin.ProvisionCost;

                }

                // If the attackStrength is zero then skip this attack round.
                if (attackStrength == 0)
                {
                    continue;
                }

                // Todo split up based on provision ratio
                decimal ratio = attackProvisions / (decimal)totalAtkProvisions;


                decimal attack = attackStrength * atkModifier;
                decimal defense = (defenseTypeList[i] * ratio * defModifier) + (wallDefense * ratio);

                // Prevent dividing by zero
                if (defense == 0)
                {
                    continue;
                }

                decimal victor = attack / defense;

                if (victor < 1)
                {

                    // Defense won, kill off all attack infantry
                    switch (i)
                    {
                        case 0:
                            currentRound.KillAllAtkInfantry();
                            break;
                        case 1:
                            currentRound.KillAllAtkCavalry();
                            break;
                        case 2:
                            currentRound.KillAllAtkArchers();
                            break;
                    }

                    decimal lostCoefficient = (decimal)Math.Sqrt((double)victor) * victor;
                    // Set the loses of the defending infantry
                    currentRound.KillDefInfantry(lostCoefficient);
                }
                else
                {
                    // Attack won, kill off all defense infantry
                    currentRound.KillAllDefInfantry();

                    decimal lostCoefficient = (decimal)Math.Sqrt(1 / (double)victor) / victor;
                    // Set the loses of the attacking infantry
                    currentRound.KillAtkInfantry(lostCoefficient);
                }
            }


            //100 Axt vs 100 Spear , 
            //4500 power vs 2500 power.
            //coef will be a = 4500 / 2500;
            //c = sqrt(1 / a) / a;
            //axt losses are calculated
            //(round) total_number_of_loosing_units* c
            //float lostCoefficient = (float)Math.Sqrt(1 / a) / a;

            LastBattleResult = result;

            _battleResultViewModel.UpdateBattleResult(LastBattleResult);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
