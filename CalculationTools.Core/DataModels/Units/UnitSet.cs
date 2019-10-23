using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculationTools.Core
{
    /// <summary>
    /// Holds the number of units for every possible unit type.
    /// </summary>
    public class UnitSet
    {
        #region Constructors

        public UnitSet()
        {

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

                Spearman = 0 < value.Count ? value[0] : 0;
                Swordsman = 1 < value.Count ? value[1] : 0;
                AxeFighter = 2 < value.Count ? value[2] : 0;
                Archer = 3 < value.Count ? value[3] : 0;
                LightCavalry = 4 < value.Count ? value[4] : 0;
                MountedArcher = 5 < value.Count ? value[5] : 0;
                HeavyCavalry = 6 < value.Count ? value[6] : 0;
                Ram = 7 < value.Count ? value[7] : 0;
                Catapult = 8 < value.Count ? value[8] : 0;
                Berserker = 9 < value.Count ? value[9] : 0;
                Trebuchet = 10 < value.Count ? value[10] : 0;
                Nobleman = 11 < value.Count ? value[11] : 0;
                Paladin = 12 < value.Count ? value[12] : 0;
            }
        }

        public bool HasUnits => UnitList.Sum(unit => unit) > 0;

        #endregion Properties

        #region Methods

        #region Operators

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

        public static bool operator ==(UnitSet u1, UnitSet u2)
        {
            return u1.Equals(u2);
        }

        public static bool operator !=(UnitSet u1, UnitSet u2)
        {
            return !u1.Equals(u2);
        }

        public override bool Equals(object obj)
        {
            return obj is UnitSet other && Equals(other);
        }

        public bool Equals(UnitSet other)
        {
            return
                Spearman == other.Spearman &&
                Swordsman == other.Swordsman &&
                AxeFighter == other.AxeFighter &&
                Archer == other.Archer &&
                LightCavalry == other.LightCavalry &&
                MountedArcher == other.MountedArcher &&
                HeavyCavalry == other.HeavyCavalry &&
                Ram == other.Ram &&
                Catapult == other.Catapult &&
                Berserker == other.Berserker &&
                Trebuchet == other.Trebuchet &&
                Nobleman == other.Nobleman &&
                Paladin == other.Paladin;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Archer;
                hashCode = (hashCode * 397) ^ AxeFighter;
                hashCode = (hashCode * 397) ^ Berserker;
                hashCode = (hashCode * 397) ^ Catapult;
                hashCode = (hashCode * 397) ^ HeavyCavalry;
                hashCode = (hashCode * 397) ^ LightCavalry;
                hashCode = (hashCode * 397) ^ MountedArcher;
                hashCode = (hashCode * 397) ^ Nobleman;
                hashCode = (hashCode * 397) ^ Paladin;
                hashCode = (hashCode * 397) ^ Ram;
                hashCode = (hashCode * 397) ^ Spearman;
                hashCode = (hashCode * 397) ^ Swordsman;
                hashCode = (hashCode * 397) ^ Trebuchet;
                return hashCode;
            }
        }

        #endregion

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

        public BattleResultRow ToBattleResultRow()
        {
            BattleResultRow battleResultRow = new BattleResultRow();
            foreach (int value in UnitList)
            {
                battleResultRow.Add(value);
            }
            return battleResultRow;
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
            for (int i = 0; i < UnitList.Count; i++)
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

        #endregion ApplyKillRate

        #region AtkStrength

        public int GetTotalInfantryAttack(WeaponSet weapon, bool defenseIsSuperior)
        {
            int totalAttack = 0;
            totalAttack += GameData.GetAtkFightingPower(Spearman, UnitType.Spearman, weapon);
            totalAttack += GameData.GetAtkFightingPower(Swordsman, UnitType.Swordsman, weapon);
            totalAttack += GameData.GetAtkFightingPower(AxeFighter, UnitType.AxeFighter, weapon);

            // Take into account that berserkers
            // are twice as strong when fighting a superior army
            totalAttack += Berserker * (GameData.Berserker.FightingPower * (defenseIsSuperior ? 2 : 1));

            return totalAttack;
        }

        public int GetTotalCavalryAttack(WeaponSet weapon)
        {
            int totalAttack = 0;
            totalAttack += GameData.GetAtkFightingPower(LightCavalry, UnitType.LightCavalry, weapon);
            totalAttack += GameData.GetAtkFightingPower(HeavyCavalry, UnitType.HeavyCavalry, weapon);
            return totalAttack;
        }

        public int GetTotalArcherAttack(WeaponSet weapon)
        {
            int totalAttack = 0;
            totalAttack += GameData.GetAtkFightingPower(Archer, UnitType.Archer, weapon);
            totalAttack += GameData.GetAtkFightingPower(MountedArcher, UnitType.MountedArcher, weapon);

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

        #endregion AtkStrength

        #region TotalDefenseFrom

        public int GetTotalDefFromArchers(WeaponSet weapon)
        {
            int totalDefense = 0;

            for (int i = 0; i < UnitList.Count; i++)
            {
                // Apply the paladin weapon if set
                decimal defenseStrength = GameData.UnitList[i].DefenseFromArchers * (GameData.UnitTypeList[i] == weapon.BelongsToUnitType ? weapon.DefModifier + 1 : 1);
                totalDefense += (int)Math.Round(UnitList[i] * defenseStrength, MidpointRounding.AwayFromZero);
            }

            return totalDefense;
        }

        public int GetTotalDefFromCavalry(WeaponSet weapon)
        {
            int totalDefense = 0;

            for (int i = 0; i < UnitList.Count; i++)
            {
                // Apply the paladin weapon if set
                decimal defenseStrength = GameData.UnitList[i].DefenseFromCavalry * (GameData.UnitTypeList[i] == weapon.BelongsToUnitType ? weapon.DefModifier + 1 : 1);
                totalDefense += (int)Math.Round(UnitList[i] * defenseStrength, MidpointRounding.AwayFromZero);
            }

            return totalDefense;
        }

        public int GetTotalDefFromInfantry(WeaponSet weapon)
        {
            int totalDefense = 0;

            for (int i = 0; i < UnitList.Count; i++)
            {
                // Apply the paladin weapon if set
                decimal defenseStrength = GameData.UnitList[i].DefenseFromInfantry * (GameData.UnitTypeList[i] == weapon.BelongsToUnitType ? weapon.DefModifier + 1 : 1);
                totalDefense += (int)Math.Round(UnitList[i] * defenseStrength, MidpointRounding.AwayFromZero);
            }

            return totalDefense;
        }

        #endregion TotalDefenseFrom

        #region NumberOfUnits

        public int GetTotalInfantryUnits()
        {
            int totalNumberOfUnits = 0;
            // Count all units that are NOT Cavalry and NOT archers
            totalNumberOfUnits += Spearman;
            totalNumberOfUnits += Swordsman;
            totalNumberOfUnits += AxeFighter;
            totalNumberOfUnits += Berserker;

            return totalNumberOfUnits;
        }

        public int GetTotalCavalryUnits()
        {
            int totalNumberOfUnits = 0;

            totalNumberOfUnits += LightCavalry;
            totalNumberOfUnits += HeavyCavalry;

            return totalNumberOfUnits;
        }

        public int GetTotalArcherUnits()
        {
            int totalNumberOfUnits = 0;

            totalNumberOfUnits += Archer;
            totalNumberOfUnits += MountedArcher;

            return totalNumberOfUnits;
        }

        public int GetTotalSpecialUnits()
        {
            int totalNumberOfUnits = 0;
            totalNumberOfUnits += Ram;
            totalNumberOfUnits += Catapult;
            totalNumberOfUnits += Trebuchet;
            totalNumberOfUnits += Nobleman;
            totalNumberOfUnits += Paladin;
            return totalNumberOfUnits;
        }

        public int GetTotalUnits()
        {
            return GetTotalInfantryUnits() + GetTotalCavalryUnits() + GetTotalArcherUnits() + GetTotalSpecialUnits();
        }

        #endregion NumberOfUnits

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

        #endregion Provisions

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

        public override string ToString()
        {
            // Turning this into an loop causes error in debugging
            // Visual Studio Debug Error:
            // To prevent an unsafe abort when evaluating the function *.toString all threads where allowed to run.
            // This may have changed the state of the process and any breakpoints encountered have been skipped.
            StringBuilder s = new StringBuilder();
            if (Spearman > 0) s.Append($"Spearman = { Spearman }, { Environment.NewLine}");
            if (Swordsman > 0) s.Append($"Swordsman = { Swordsman}, {Environment.NewLine}");
            if (AxeFighter > 0) s.Append($"AxeFighter = { AxeFighter }, {Environment.NewLine}");
            if (Archer > 0) s.Append($"Archer = { Archer }, {Environment.NewLine}");
            if (LightCavalry > 0) s.Append($"LightCavalry = { LightCavalry }, {Environment.NewLine}");
            if (MountedArcher > 0) s.Append($"MountedArcher = { MountedArcher}, {Environment.NewLine}");
            if (HeavyCavalry > 0) s.Append($"HeavyCavalry = { HeavyCavalry }, {Environment.NewLine}");
            if (Ram > 0) s.Append($"Ram = { Ram}, {Environment.NewLine}");
            if (Catapult > 0) s.Append($"Catapult = { Catapult }, {Environment.NewLine}");
            if (Berserker > 0) s.Append($"Berserker = { Berserker}, {Environment.NewLine}");
            if (Trebuchet > 0) s.Append($"Trebuchet = { Trebuchet}, {Environment.NewLine}");
            if (Nobleman > 0) s.Append($"Nobleman = { Nobleman }, {Environment.NewLine}");
            if (Paladin > 0) s.Append($"Paladin = { Paladin}, {Environment.NewLine}");
            return s.ToString();
        }

        #endregion Methods
    }
}