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
using TribalWars2_CalculationTools.Class.Structs;
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

        public CalculatorData()
        {

        }
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

            decimal atkModifier = GameData.GetAtkBattleModifier(atkFaithBonus, morale, luck, officerBonus);
            result.AtkBattleModifier = (int)(atkModifier * 100m);

            decimal defModifier = GameData.GetDefBattleModifier(defFaithBonus, wallBonus, nightBonus);
            result.DefBattleModifier = (int)(defModifier * 100m);

            // Stop here if there are no units given
            if (!input.IsValid)
            {
                LastBattleResult = result;

                _battleResultViewModel.UpdateBattleResult(LastBattleResult);

                return;
            }



            int resultingWallLevel = WallLevelBeforeBattle(result.AtkUnits.Ram, wallLevel, atkFaithBonus, false);

            int wallDefense = 0;

            if (resultingWallLevel > 0)
            {
                wallDefense = GameData.GetWallDefense(resultingWallLevel);
            }

            List<BattleResult> BattleHistory = new List<BattleResult>();

            BattleHistory.Add(result.Copy());

            // Simulate for 3 rounds (infantry, cavalry and archers)
            bool battleDetermined = false;
            while (!battleDetermined)
            {
                BattleResult currentRound = BattleHistory.Last();
                //Reset the UnitLost for subsequent rounds
                currentRound.AtkUnitsLost = new UnitSet();
                currentRound.DefUnitsLost = new UnitSet();

                UnitSet atkUnits = currentRound.AtkUnits;
                UnitSet atkUnitsLost = currentRound.AtkUnitsLost;
                UnitSet defUnits = currentRound.DefUnits;

                int atkInfantryProvisions = atkUnits.GetTotalInfantryProvisions();
                int atkCavalryProvisions = atkUnits.GetTotalCavalryProvisions();
                int atkArchersProvisions = atkUnits.GetTotalArcherProvisions();
                int totalAtkProvisions = atkInfantryProvisions + atkCavalryProvisions + atkArchersProvisions;

                int totalDefProvisions = defUnits.GetTotalProvisions();
                if (totalAtkProvisions == 0)
                {
                    break;
                }

                decimal atkInfantryRatio = GameData.GetUnitProvisionRatio(atkInfantryProvisions, totalAtkProvisions);
                decimal atkCavalryRatio = GameData.GetUnitProvisionRatio(atkCavalryProvisions, totalAtkProvisions);
                decimal atkArchersRatio = GameData.GetUnitProvisionRatio(atkArchersProvisions, totalAtkProvisions);
                decimal totalRatio = atkInfantryRatio + atkCavalryRatio + atkArchersRatio;

                bool defSuperior = (totalAtkProvisions * 2 <= totalDefProvisions);

                int atkInfantry = atkUnits.GetTotalInfantryAttack(atkModifier, defSuperior);
                int atkCavalry = atkUnits.GetTotalCavalryAttack(atkModifier);
                int atkArchers = atkUnits.GetTotalArcherAttack(atkModifier);
                int totalAtk = atkInfantry + atkCavalry + atkArchers;

                // These units sets contains the defensive units proportionate to the atkInfantry ratio
                UnitSet infantryGroupDefUnitSet = defUnits.GetUnitsByRatio(atkInfantryRatio);
                UnitSet cavalryGroupDefUnitSet = defUnits.GetUnitsByRatio(atkCavalryRatio);
                UnitSet archerGroupDefUnitSet = defUnits.GetUnitsByRatio(atkArchersRatio);

                int totalDefFromInfantry = infantryGroupDefUnitSet.GetTotalDefFromInfantry(defModifier, wallDefense);
                int totalDefFromCavalry = cavalryGroupDefUnitSet.GetTotalDefFromCavalry(defModifier, wallDefense);
                int totalDefFromArchers = archerGroupDefUnitSet.GetTotalDefFromArchers(defModifier, wallDefense);
                int totalDef = totalDefFromInfantry + totalDefFromCavalry + totalDefFromArchers;
                // Used to keep track of how many units are lost this round
                UnitSet infantryGroupDefUnitSetLost, cavalryGroupDefUnitSetLost, archerGroupDefUnitSetLost;

                bool atkWonRound1 = (atkInfantry >= totalDefFromInfantry);
                bool atkWonRound2 = (atkCavalry >= totalDefFromCavalry);
                bool atkWonRound3 = (atkArchers >= totalDefFromArchers);

                // For every round that Defense won, kill the attacking party and vice versa
                // General / Infantry round
                decimal killRate;
                if (atkWonRound1)
                {
                    killRate = GameData.GetAtkKillRate(atkInfantry, totalDefFromInfantry);
                    atkUnitsLost += atkUnits.ApplyKillRateAtkInfantry(killRate);
                    infantryGroupDefUnitSetLost = infantryGroupDefUnitSet.ApplyKillRate(1);
                }
                else
                {
                    killRate = GameData.GetDefKillRate(atkInfantry, totalDefFromInfantry);
                    atkUnitsLost += atkUnits.ApplyKillRateAtkInfantry(1);
                    infantryGroupDefUnitSetLost = infantryGroupDefUnitSet.ApplyKillRate(killRate);
                }

                // Cavalry round
                if (atkWonRound2)
                {
                    killRate = GameData.GetAtkKillRate(atkCavalry, totalDefFromCavalry);
                    atkUnitsLost += atkUnits.ApplyKillRateAtkCavalry(killRate);
                    cavalryGroupDefUnitSetLost = cavalryGroupDefUnitSet.ApplyKillRate(1);
                }
                else
                {
                    killRate = GameData.GetDefKillRate(atkCavalry, totalDefFromCavalry);
                    atkUnitsLost += atkUnits.ApplyKillRateAtkCavalry(1);
                    cavalryGroupDefUnitSetLost = cavalryGroupDefUnitSet.ApplyKillRate(killRate);
                }

                // Archer round
                if (atkWonRound3)
                {
                    killRate = GameData.GetAtkKillRate(atkArchers, totalDefFromArchers);
                    atkUnitsLost += atkUnits.ApplyKillRateAtkArchers(killRate);
                    archerGroupDefUnitSetLost = archerGroupDefUnitSet.ApplyKillRate(1);
                }
                else
                {
                    killRate = GameData.GetDefKillRate(atkArchers, totalDefFromArchers);
                    atkUnitsLost += atkUnits.ApplyKillRateAtkArchers(1);
                    archerGroupDefUnitSetLost = archerGroupDefUnitSet.ApplyKillRate(killRate);

                }

                UnitSet survivingDefUnits = infantryGroupDefUnitSet + cavalryGroupDefUnitSet + archerGroupDefUnitSet;
                UnitSet defUnitsLost = infantryGroupDefUnitSetLost + cavalryGroupDefUnitSetLost + archerGroupDefUnitSetLost;

                currentRound.AtkUnits = atkUnits;
                currentRound.AtkUnitsLost = atkUnitsLost;
                currentRound.DefUnits = survivingDefUnits;
                currentRound.DefUnitsLost = defUnitsLost;

                // Check if during the 3 mini-battles either attack of defense won all mini-battles
                battleDetermined = (atkWonRound1 && atkWonRound2 && atkWonRound3) || (!atkWonRound1 && !atkWonRound2 && !atkWonRound3);
                if (!battleDetermined)
                {
                    // WallDefense is only added the first round
                    wallDefense = 0;
                    BattleHistory.Add(currentRound.Copy());
                }
            }


            BattleResult finalResult = result.Copy();

            foreach (BattleResult battleResult in BattleHistory)
            {
                finalResult.AtkUnitsLost += battleResult.AtkUnitsLost;
                finalResult.DefUnitsLost += battleResult.DefUnitsLost;
            }

            LastBattleResult = finalResult;
            _battleResultViewModel.UpdateBattleResult(LastBattleResult);

            //int attackStrength = attackTypeList[i];
            //int attackProvisions = atkProvisionTypeList[i];

            //// The Paladin fights with the strongest (highest fighting power) group. 
            //// The weapon boost is applied based on the type and not if he joins the round or not. 
            //if (attackStrength == attackTypeList.Max())
            //{
            //    attackStrength += GameData.Paladin.FightingPower;
            //    // TODO It is assumed that the Paladin provision is counted towards the group it joins
            //    attackProvisions += GameData.Paladin.ProvisionCost;

            //}

            //// If the attackStrength is zero then skip this attack round.
            //if (attackStrength == 0)
            //{
            //    continue;
            //}

            //// Todo split up based on provision ratio
            //decimal ratio = attackProvisions / (decimal)totalAtkProvisions;


            //decimal attack = attackStrength * atkModifier;
            //// + (wallDefense * ratio)
            //decimal defense = (defenseTypeList[i] * ratio * defModifier);


            //    decimal lostCoefficient = (decimal)Math.Sqrt((double)victor) * victor;
            //    // Set the loses of the defending infantry
            //    // currentRound.KillDefInfantry(lostCoefficient);
            //}
            //else
            //{
            //    // Attack won, kill off all defense infantry
            //    currentRound.KillAllDefInfantry();

            //    decimal lostCoefficient = (decimal)Math.Sqrt(1 / (double)victor) / victor;
            //    // Set the loses of the attacking infantry
            //    //currentRound.KillAtkInfantry(lostCoefficient);
            //}


        }




        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
