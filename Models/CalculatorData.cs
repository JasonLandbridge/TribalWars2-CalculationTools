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

namespace TribalWars2_CalculationTools.Models
{
    public class CalculatorData : INotifyPropertyChanged
    {

        private BindingList<BaseUnit> _units = new BindingList<BaseUnit>();
        private BattleResult _lastBattleResult;

        public BattleResult LastBattleResult
        {
            get => _lastBattleResult;
            set
            {
                _lastBattleResult = value;
                OnPropertyChanged();
            }
        }





        //public BindingList<BaseUnit> Units
        //{
        //    get => _units;
        //    set
        //    {
        //        Debug.WriteLine("SUCCESS!!!");

        //        _units = value;
        //    }
        //}

        //public Spearman Spearman
        //{
        //    get => (Spearman)Units[0];
        //    set
        //    {
        //        Units[0] = value;
        //        OnPropertyChanged(); ValueUpdated();
        //    }
        //}
        //public Swordsman Swordsman
        //{
        //    get => (Swordsman)Units[1];
        //    set
        //    {
        //        Units[1] = value;
        //        OnPropertyChanged(); ValueUpdated();
        //    }
        //}
        //public AxeFighter AxeFighter
        //{
        //    get => (AxeFighter)Units[2];
        //    set
        //    {
        //        Units[2] = value;
        //        OnPropertyChanged(); ValueUpdated();
        //    }
        //}
        //public Archer Archer
        //{
        //    get => (Archer)Units[3];
        //    set
        //    {
        //        Units[3] = value;
        //        OnPropertyChanged(); ValueUpdated();
        //    }
        //}
        //public LightCavalry LightCavalry
        //{
        //    get => (LightCavalry)Units[4];
        //    set
        //    {
        //        Units[4] = value;
        //        NotifyOfPropertyChange(() => LightCavalry);
        //        ValueUpdated();
        //    }
        //}
        //public MountedArcher MountedArcher
        //{
        //    get => (MountedArcher)Units[5];
        //    set
        //    {
        //        Units[5] = value;
        //        NotifyOfPropertyChange(() => MountedArcher);
        //        ValueUpdated();
        //    }
        //}
        //public HeavyCavalry HeavyCavalry
        //{
        //    get => (HeavyCavalry)Units[6];
        //    set
        //    {
        //        Units[6] = value;
        //        NotifyOfPropertyChange(() => HeavyCavalry);
        //        ValueUpdated();
        //    }
        //}
        //public Ram Ram
        //{
        //    get => (Ram)Units[7];
        //    set
        //    {
        //        Units[7] = value;
        //        NotifyOfPropertyChange(() => Ram);
        //        ValueUpdated();
        //    }
        //}

        //public Catapult Catapult
        //{
        //    get => (Catapult)Units[8];
        //    set
        //    {
        //        Units[8] = value;
        //        NotifyOfPropertyChange(() => Catapult);
        //        ValueUpdated();
        //    }
        //}

        //public Berserker Berserker
        //{
        //    get => (Berserker)Units[9];
        //    set
        //    {
        //        Units[9] = value;
        //        NotifyOfPropertyChange(() => Berserker);
        //        ValueUpdated();
        //    }
        //}

        //public Trebuchet Trebuchet
        //{
        //    get => (Trebuchet)Units[10];
        //    set
        //    {
        //        Units[10] = value;
        //        NotifyOfPropertyChange(() => Trebuchet);
        //        ValueUpdated();
        //    }
        //}

        //public Nobleman Nobleman
        //{
        //    get => (Nobleman)Units[11];
        //    set
        //    {
        //        Units[11] = value;
        //        NotifyOfPropertyChange(() => Nobleman);
        //        ValueUpdated();
        //    }
        //}

        //public Paladin Paladin
        //{
        //    get => (Paladin)Units[12];
        //    set
        //    {
        //        Units[12] = value;
        //        NotifyOfPropertyChange(() => Paladin);
        //        ValueUpdated();
        //    }
        //}

        public CalculatorData()
        {
            // Do not change the order!
            //Units.Add(new Spearman());
            //Units.Add(new Swordsman());
            //Units.Add(new AxeFighter());
            //Units.Add(new Archer());

            //Units.Add(new LightCavalry());
            //Units.Add(new MountedArcher());
            //Units.Add(new HeavyCavalry());
            //Units.Add(new Ram());

            //Units.Add(new Catapult());
            //Units.Add(new Berserker());
            //Units.Add(new Trebuchet());

            //Units.Add(new Nobleman());
            //Units.Add(new Paladin());

            LastBattleResult = new BattleResult();

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

        public void SimulateBattle(InputCalculatorClass input)
        {

            if (!input.IsValid)
            {
                return;
            }

            // Based on: Tribal Wars 2 - Tutorial: Basic Battle System - https://www.youtube.com/watch?v=SG_qI1-go88
            // Based on: Battle Simulator - http://www.ds-pro.de/2/simulator.php
            BattleResult result = new BattleResult(input);

            decimal faithBonus = (input.InputAtkChurch ? 0.5m : 1m) * (input.InputDefChurch ? 2.0m : 1m);
            int wallLevel = input.InputWall;
            decimal morale = input.InputMorale;
            decimal luck = 1m + (input.InputLuck / 100m);
            decimal nightBonus = (input.InputNightBonus ? 2m : 1m);
            decimal officerBonus = (input.InputGrandmasterBonus ? 0.1m : 0m);

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

            int atkInfantry = result.GetTotalInfantryAttack(defSuperior);
            int atkCavalry = result.GetTotalCavalryAttack();
            int atkArchers = result.GetTotalArcherAttack();
            List<int> attackTypeList = new List<int>
            {
                atkInfantry,
                atkCavalry,
                atkArchers
            };
            int atkStrength = atkInfantry + atkCavalry + atkArchers;

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


            int resultingWallLevel = WallLevelBeforeBattle(result.AtkRam, wallLevel, faithBonus, false);

            // 5% for every wall level
            decimal wallBonus = 1m + wallLevel * 0.05m;

            int wallDefense = 0;

            if (resultingWallLevel > 0)
            {
                wallDefense = (int)Math.Round(Math.Pow(1.24, resultingWallLevel) * 20, MidpointRounding.AwayFromZero);
            }

            // Based on the wiki https://en.wiki.tribalwars2.com/index.php?title=Battles
            decimal atkModifier = 100m * (faithBonus * morale * luck) + officerBonus;
           // decimal defModifier = 100m * (faithBonus)
            // Simulate for 3 rounds (infantry, cavalry and archers)
            for (int i = 0; i < 2; i++)
            {
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

                decimal ratio = attackProvisions / (decimal)totalAtkProvisions;


                decimal attack = (attackStrength * morale * luck * faithBonus);
                decimal defense = (defenseTypeList[i] * ratio * wallBonus * nightBonus) + (wallDefense * ratio);

                // Prevent dividing by zero
                if (defense == 0)
                {
                    continue;
                }

                decimal victor = attack / defense;

                if (victor < 1)
                {
                    // Defense won, kill off all attack infantry
                    result.KillAllAtkInfantry();

                    decimal lostCoefficient = (decimal)Math.Sqrt((double)victor) * victor;
                    // Set the loses of the defending infantry
                    result.KillDefInfantry(lostCoefficient);
                }
                else
                {
                    // Attack won, kill off all defense infantry
                    result.KillAllDefInfantry();

                    decimal lostCoefficient = (decimal)Math.Sqrt(1 / (double)victor) / victor;
                    // Set the loses of the attacking infantry
                    result.KillAtkInfantry(lostCoefficient);
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

        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
