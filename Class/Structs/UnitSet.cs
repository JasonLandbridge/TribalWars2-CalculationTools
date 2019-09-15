using System;
using System.Collections.Generic;
using System.Text;

namespace TribalWars2_CalculationTools.Class.Structs
{
    public struct UnitSet
    {
        #region Constructors

        public UnitSet(int defaultValue = 0)
        {
            Spearman = defaultValue;
            Swordsman = defaultValue;
            AxeFighter = defaultValue;
            Archer = defaultValue;
            LightCavalry = defaultValue;
            MountedArcher = defaultValue;
            HeavyCavalry = defaultValue;
            Ram = defaultValue;
            Catapult = defaultValue;
            Berserker = defaultValue;
            Trebuchet = defaultValue;
            Nobleman = defaultValue;
            Paladin = defaultValue;
        }

        public UnitSet(List<int> unitList)
        {
            Spearman = 0;
            Swordsman = 0;
            AxeFighter = 0;
            Archer = 0;
            LightCavalry = 0;
            MountedArcher = 0;
            HeavyCavalry = 0;
            Ram = 0;
            Catapult = 0;
            Berserker = 0;
            Trebuchet = 0;
            Nobleman = 0;
            Paladin = 0;
            UnitList = unitList;
        }

        #endregion Constructors

        #region Properties

        public int Archer { get; set; }
        public int AxeFighter { get; set; }
        public int Berserker { get; set; }
        public int Catapult { get; set; }
        public int HeavyCavalry { get; set; }
        public int LightCavalry { get; set; }
        public int MountedArcher { get; set; }
        public int Nobleman { get; set; }
        public int Paladin { get; set; }
        public int Ram { get; set; }
        public int Spearman { get; set; }
        public int Swordsman { get; set; }
        public int Trebuchet { get; set; }
        public List<int> UnitList
        {
            get =>
                new List<int>
                {
                    Spearman     ,
                    Swordsman    ,
                    AxeFighter   ,
                    Archer       ,
                    LightCavalry ,
                    MountedArcher,
                    HeavyCavalry ,
                    Ram          ,
                    Catapult     ,
                    Berserker    ,
                    Trebuchet    ,
                    Nobleman     ,
                    Paladin      ,
                };
            set
            {
                if (value.Count == 13)
                {
                    Spearman = value[0];
                    Swordsman = value[1];
                    AxeFighter = value[2];
                    Archer = value[3];
                    LightCavalry = value[4];
                    MountedArcher = value[5];
                    HeavyCavalry = value[6];
                    Ram = value[7];
                    Catapult = value[8];
                    Berserker = value[9];
                    Trebuchet = value[10];
                    Nobleman = value[11];
                    Paladin = value[12];
                }
                else
                {
                    throw new IndexOutOfRangeException("UnitList needs to exactly contain 13 entries!");
                }
            }
        }

        #endregion Properties

        #region Methods

        public static UnitSet operator -(UnitSet u1, UnitSet u2) => new UnitSet
        {
            Spearman = u1.Spearman - u2.Spearman,
            Swordsman = u1.Swordsman - u2.Swordsman,
            AxeFighter = u1.AxeFighter - u2.AxeFighter,
            Archer = u1.Archer - u2.Archer,
            LightCavalry = u1.LightCavalry - u2.LightCavalry,
            MountedArcher = u1.MountedArcher - u2.MountedArcher,
            HeavyCavalry = u1.HeavyCavalry - u2.HeavyCavalry,
            Ram = u1.Ram - u2.Ram,
            Catapult = u1.Catapult - u2.Catapult,
            Berserker = u1.Berserker - u2.Berserker,
            Trebuchet = u1.Trebuchet - u2.Trebuchet,
            Nobleman = u1.Nobleman - u2.Nobleman,
            Paladin = u1.Paladin - u2.Paladin,
        };

        public static UnitSet operator +(UnitSet u1, UnitSet u2) => new UnitSet
        {
            Spearman = u1.Spearman + u2.Spearman,
            Swordsman = u1.Swordsman + u2.Swordsman,
            AxeFighter = u1.AxeFighter + u2.AxeFighter,
            Archer = u1.Archer + u2.Archer,
            LightCavalry = u1.LightCavalry + u2.LightCavalry,
            MountedArcher = u1.MountedArcher + u2.MountedArcher,
            HeavyCavalry = u1.HeavyCavalry + u2.HeavyCavalry,
            Ram = u1.Ram + u2.Ram,
            Catapult = u1.Catapult + u2.Catapult,
            Berserker = u1.Berserker + u2.Berserker,
            Trebuchet = u1.Trebuchet + u2.Trebuchet,
            Nobleman = u1.Nobleman + u2.Nobleman,
            Paladin = u1.Paladin + u2.Paladin,
        };

        public UnitSet GetUnitsByRatio(decimal ratio = 1m)
        {
            List<int> ratioUnitList = new List<int>();
            foreach (int numberOfUnits in UnitList)
            {
                ratioUnitList.Add(GameData.GetUnitRatio(ratio, numberOfUnits));
            }
            return new UnitSet(ratioUnitList);
        }

        public UnitSet GetUnitsLost(decimal killRate)
        {
            return new UnitSet
            {
                Spearman = GameData.GetUnitsKilled(Spearman, killRate),
                Swordsman = GameData.GetUnitsKilled(Swordsman, killRate),
                AxeFighter = GameData.GetUnitsKilled(AxeFighter, killRate),
                Archer = GameData.GetUnitsKilled(Archer, killRate),
                LightCavalry = GameData.GetUnitsKilled(LightCavalry, killRate),
                MountedArcher = GameData.GetUnitsKilled(MountedArcher, killRate),
                HeavyCavalry = GameData.GetUnitsKilled(HeavyCavalry, killRate),
                Ram = GameData.GetUnitsKilled(Ram, killRate),
                Catapult = GameData.GetUnitsKilled(Catapult, killRate),
                Berserker = GameData.GetUnitsKilled(Berserker, killRate),
                Trebuchet = GameData.GetUnitsKilled(Trebuchet, killRate),
                Nobleman = GameData.GetUnitsKilled(Nobleman, killRate),
                Paladin = GameData.GetUnitsKilled(Paladin, killRate),
            };
        }
        public List<BattleResultValue> ToBattleResultList()
        {
            List<BattleResultValue> list = new List<BattleResultValue>();
            foreach (int value in UnitList)
            {
                list.Add(new BattleResultValue(value));
            }
            return list;
        }

        #region ApplyKillRate


        /// <summary>
        /// Applies the kill rate to this UnitSet and returns another UnitSet with the number of lost units.
        /// </summary>
        /// <param name="killRate"></param>
        /// <returns></returns>
        public UnitSet ApplyKillRate(decimal killRate)
        {
            List<int> newList = new List<int>();
            List<int> lostList = GetUnitsLost(killRate).UnitList;

            // Subtract the loses from the current number of units
            for (int i = 0; i < this.UnitList.Count; i++)
            {
                newList.Add(UnitList[i] - lostList[i]);
            }

            UnitList = newList;
            return new UnitSet(lostList);
        }
        public UnitSet ApplyKillRateAtkInfantry(decimal killRate)
        {
            UnitSet newLostUnitSet = new UnitSet
            {
                Spearman = GameData.GetUnitsKilled(Spearman, killRate),
                Swordsman = GameData.GetUnitsKilled(Swordsman, killRate),
                AxeFighter = GameData.GetUnitsKilled(AxeFighter, killRate),
            };

            Spearman -= newLostUnitSet.Spearman;
            Swordsman -= newLostUnitSet.Swordsman;
            AxeFighter -= newLostUnitSet.AxeFighter;

            return newLostUnitSet;
        }
        public UnitSet ApplyKillRateAtkArchers(decimal killRate)
        {
            UnitSet newLostUnitSet = new UnitSet
            {
                Archer = GameData.GetUnitsKilled(Archer, killRate),
                MountedArcher = GameData.GetUnitsKilled(MountedArcher, killRate),
            };

            Archer -= newLostUnitSet.Archer;
            MountedArcher -= newLostUnitSet.MountedArcher;

            return newLostUnitSet;
        }

        public UnitSet ApplyKillRateAtkCavalry(decimal killRate)
        {
            UnitSet newLostUnitSet = new UnitSet
            {
                LightCavalry = GameData.GetUnitsKilled(LightCavalry, killRate),
                HeavyCavalry = GameData.GetUnitsKilled(HeavyCavalry, killRate),
            };

            LightCavalry -= newLostUnitSet.LightCavalry;
            HeavyCavalry -= newLostUnitSet.HeavyCavalry;

            return newLostUnitSet;
        }

        public UnitSet ApplyKillRateAtkSpecial(decimal killRate)
        {
            UnitSet newLostUnitSet = new UnitSet
            {
                Ram = GameData.GetUnitsKilled(Ram, killRate),
                Catapult = GameData.GetUnitsKilled(Catapult, killRate),
                Trebuchet = GameData.GetUnitsKilled(Trebuchet, killRate),
                Paladin = GameData.GetUnitsKilled(Paladin, killRate),
                Nobleman = GameData.GetUnitsKilled(Nobleman, killRate),
            };

            Ram -= newLostUnitSet.Ram;
            Catapult -= newLostUnitSet.Catapult;
            Trebuchet -= newLostUnitSet.Trebuchet;
            Paladin -= newLostUnitSet.Paladin;
            Nobleman -= newLostUnitSet.Nobleman; //TODO How the nobleman dies is still a mystery, needs confirmation

            return newLostUnitSet;
        }


        #endregion
        #region AtkStrength
        public int GetTotalInfantryAttack(bool defenseIsSuperior)
        {
            int totalAttack = 0;
            totalAttack += Spearman * GameData.Spearman.FightingPower;
            totalAttack += Swordsman * GameData.Swordsman.FightingPower;
            totalAttack += AxeFighter * GameData.AxeFighter.FightingPower;

            // Take into account that berserkers
            // are twice as strong when fighting a superior army
            totalAttack += Berserker * (GameData.Berserker.FightingPower * (defenseIsSuperior ? 2 : 1));

            return totalAttack;
        }

        public int GetTotalCavalryAttack()
        {
            int totalAttack = 0;
            totalAttack += LightCavalry * GameData.LightCavalry.FightingPower;
            totalAttack += HeavyCavalry * GameData.HeavyCavalry.FightingPower;

            return totalAttack;
        }

        public int GetTotalArcherAttack()
        {
            int totalAttack = 0;
            totalAttack += Archer * GameData.Archer.FightingPower;
            totalAttack += MountedArcher * GameData.MountedArcher.FightingPower;

            return totalAttack;
        }

        public int GetTotalSpecialAtk()
        {
            // The nobleman attack is not counted, only its provisions
            int totalAttack = 0;
            totalAttack += Ram * GameData.Ram.FightingPower;
            totalAttack += Catapult * GameData.Catapult.FightingPower;
            totalAttack += Trebuchet * GameData.Trebuchet.FightingPower;
            totalAttack += Paladin * GameData.Paladin.FightingPower;
            return totalAttack;


        }




        #endregion

        #region TotalDefenseFrom

        public int GetTotalDefFromArchers(decimal defModifier, int wallDefense)
        {
            int totalDefense = 0;

            totalDefense += Spearman * GameData.GetUnit("Spearman").DefenseFromArchers;
            totalDefense += Swordsman * GameData.GetUnit("Swordsman").DefenseFromArchers;
            totalDefense += AxeFighter * GameData.GetUnit("AxeFighter").DefenseFromArchers;
            totalDefense += Archer * GameData.GetUnit("Archer").DefenseFromArchers;
            totalDefense += LightCavalry * GameData.GetUnit("LightCavalry").DefenseFromArchers;
            totalDefense += MountedArcher * GameData.GetUnit("MountedArcher").DefenseFromArchers;
            totalDefense += HeavyCavalry * GameData.GetUnit("HeavyCavalry").DefenseFromArchers;
            totalDefense += Ram * GameData.GetUnit("Ram").DefenseFromArchers;
            totalDefense += Catapult * GameData.GetUnit("Catapult").DefenseFromArchers;
            totalDefense += Berserker * GameData.GetUnit("Berserker").DefenseFromArchers;
            totalDefense += Trebuchet * GameData.GetUnit("Trebuchet").DefenseFromArchers;
            totalDefense += Nobleman * GameData.GetUnit("Nobleman").DefenseFromArchers;
            totalDefense += Paladin * GameData.GetUnit("Paladin").DefenseFromArchers;

            return (int)Math.Round(totalDefense * defModifier, MidpointRounding.AwayFromZero) + wallDefense;
        }

        public int GetTotalDefFromCavalry(decimal defModifier, int wallDefense)
        {
            int totalDefense = 0;

            totalDefense += Spearman * GameData.GetUnit("Spearman").DefenseFromCavalry;
            totalDefense += Swordsman * GameData.GetUnit("Swordsman").DefenseFromCavalry;
            totalDefense += AxeFighter * GameData.GetUnit("AxeFighter").DefenseFromCavalry;
            totalDefense += Archer * GameData.GetUnit("Archer").DefenseFromCavalry;
            totalDefense += LightCavalry * GameData.GetUnit("LightCavalry").DefenseFromCavalry;
            totalDefense += MountedArcher * GameData.GetUnit("MountedArcher").DefenseFromCavalry;
            totalDefense += HeavyCavalry * GameData.GetUnit("HeavyCavalry").DefenseFromCavalry;
            totalDefense += Ram * GameData.GetUnit("Ram").DefenseFromCavalry;
            totalDefense += Catapult * GameData.GetUnit("Catapult").DefenseFromCavalry;
            totalDefense += Berserker * GameData.GetUnit("Berserker").DefenseFromCavalry;
            totalDefense += Trebuchet * GameData.GetUnit("Trebuchet").DefenseFromCavalry;
            totalDefense += Nobleman * GameData.GetUnit("Nobleman").DefenseFromCavalry;
            totalDefense += Paladin * GameData.GetUnit("Paladin").DefenseFromCavalry;

            return (int)Math.Round(totalDefense * defModifier, MidpointRounding.AwayFromZero) + wallDefense;
        }

        public int GetTotalDefFromInfantry(decimal defModifier, int wallDefense)
        {
            int totalDefense = 0;

            totalDefense += Spearman * GameData.GetUnit("Spearman").DefenseFromInfantry;
            totalDefense += Swordsman * GameData.GetUnit("Swordsman").DefenseFromInfantry;
            totalDefense += AxeFighter * GameData.GetUnit("AxeFighter").DefenseFromInfantry;
            totalDefense += Archer * GameData.GetUnit("Archer").DefenseFromInfantry;
            totalDefense += LightCavalry * GameData.GetUnit("LightCavalry").DefenseFromInfantry;
            totalDefense += MountedArcher * GameData.GetUnit("MountedArcher").DefenseFromInfantry;
            totalDefense += HeavyCavalry * GameData.GetUnit("HeavyCavalry").DefenseFromInfantry;
            totalDefense += Ram * GameData.GetUnit("Ram").DefenseFromInfantry;
            totalDefense += Catapult * GameData.GetUnit("Catapult").DefenseFromInfantry;
            totalDefense += Berserker * GameData.GetUnit("Berserker").DefenseFromInfantry;
            totalDefense += Trebuchet * GameData.GetUnit("Trebuchet").DefenseFromInfantry;
            totalDefense += Nobleman * GameData.GetUnit("Nobleman").DefenseFromInfantry;
            totalDefense += Paladin * GameData.GetUnit("Paladin").DefenseFromInfantry;

            return (int)Math.Round(totalDefense * defModifier, MidpointRounding.AwayFromZero) + wallDefense;
        }

        #endregion

        #region Provisions

        public int GetTotalArcherProvisions()
        {
            int totalProvisions = 0;

            totalProvisions += Archer * GameData.Archer.ProvisionCost;
            totalProvisions += MountedArcher * GameData.MountedArcher.ProvisionCost;

            return totalProvisions;
        }

        public int GetTotalCavalryProvisions()
        {
            int totalProvisions = 0;

            totalProvisions += LightCavalry * GameData.LightCavalry.ProvisionCost;
            totalProvisions += HeavyCavalry * GameData.HeavyCavalry.ProvisionCost;

            return totalProvisions;
        }

        public int GetTotalInfantryProvisions()
        {
            int totalProvisions = 0;
            // Count all units that are NOT Cavalry and NOT archers
            totalProvisions += Spearman * GameData.Spearman.ProvisionCost;
            totalProvisions += Swordsman * GameData.Swordsman.ProvisionCost;
            totalProvisions += AxeFighter * GameData.AxeFighter.ProvisionCost;
            totalProvisions += Berserker * GameData.Berserker.ProvisionCost;

            totalProvisions += Nobleman * GameData.Nobleman.ProvisionCost;

            return totalProvisions;
        }

        public int GetTotalProvisions()
        {
            return GetTotalInfantryProvisions() + GetTotalCavalryProvisions() + GetTotalArcherProvisions() + GetTotalSpecialProvisions();
        }

        public int GetTotalRamProvisions()
        {
            return Ram * GameData.Ram.ProvisionCost;
        }
        public int GetTotalSpecialProvisions()
        {
            int totalProvisions = 0;
            totalProvisions += Ram * GameData.Ram.ProvisionCost;
            totalProvisions += Catapult * GameData.Catapult.ProvisionCost;
            totalProvisions += Trebuchet * GameData.Trebuchet.ProvisionCost;
            totalProvisions += Nobleman * GameData.Nobleman.ProvisionCost;
            totalProvisions += Paladin * GameData.Paladin.ProvisionCost;
            return totalProvisions;

        }
        #endregion

        public void Clear()
        {
            // Create 0 list and set over the UnitList
            List<int> zeroList = new List<int>();

            for (int i = 0; i < UnitList.Count; i++)
            {
                zeroList.Add(0);
            }

            UnitList = zeroList;
        }

        #endregion Methods
    }
}
