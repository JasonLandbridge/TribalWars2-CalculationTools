using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TribalWars2_CalculationTools.Class;
using TribalWars2_CalculationTools.Class.Buildings;
using TribalWars2_CalculationTools.Class.Enum;
using TribalWars2_CalculationTools.Class.Units;
using TribalWars2_CalculationTools.Class.Weapons;
using TribalWars2_CalculationTools.Models;

namespace TribalWars2_CalculationTools.Class
{
    public static class GameData
    {
        #region Units

        public static Spearman Spearman { get; } = new Spearman();
        public static Swordsman Swordsman { get; } = new Swordsman();
        public static AxeFighter AxeFighter { get; } = new AxeFighter();
        public static Archer Archer { get; } = new Archer();
        public static LightCavalry LightCavalry { get; } = new LightCavalry();
        public static MountedArcher MountedArcher { get; } = new MountedArcher();
        public static HeavyCavalry HeavyCavalry { get; } = new HeavyCavalry();
        public static Ram Ram { get; } = new Ram();
        public static Catapult Catapult { get; } = new Catapult();
        public static Berserker Berserker { get; } = new Berserker();
        public static Trebuchet Trebuchet { get; } = new Trebuchet();
        public static Nobleman Nobleman { get; } = new Nobleman();
        public static Paladin Paladin { get; } = new Paladin();
        #endregion

        #region Buildings

        public static Wall Wall { get; } = new Wall();

        #endregion


        #region Weapons

        public static SpearmanWeapon SpearmanWeapon { get; } = new SpearmanWeapon();
        public static SwordsmanWeapon SwordsmanWeapon { get; } = new SwordsmanWeapon();
        public static AxeFighterWeapon AxeFighterWeapon { get; } = new AxeFighterWeapon();
        public static ArcherWeapon ArcherWeapon { get; } = new ArcherWeapon();
        public static LightCavalryWeapon LightCavalryWeapon { get; } = new LightCavalryWeapon();
        public static MountedArcherWeapon MountedArcherWeapon { get; } = new MountedArcherWeapon();
        public static HeavyCavalryWeapon HeavyCavalryWeapon { get; } = new HeavyCavalryWeapon();
        public static RamWeapon RamWeapon { get; } = new RamWeapon();
        public static CatapultWeapon CatapultWeapon { get; } = new CatapultWeapon();

        public static decimal GetAtkModifierFromWeapon(UnitType belongsToUnitType, int weaponLevel)
        {
            foreach (BaseWeapon weapon in WeaponOptions)
            {
                if (weapon.BelongsToUnitType == belongsToUnitType)
                {
                    return weapon.GetAtkModifier(weaponLevel);
                }
            }

            return 1m;
        }

        public static decimal GetDefModifierFromWeapon(UnitType belongsToUnitType, int weaponLevel)
        {
            foreach (BaseWeapon weapon in WeaponOptions)
            {
                if (weapon.BelongsToUnitType == belongsToUnitType)
                {
                    return weapon.GetDefModifier(weaponLevel);
                }
            }

            return 1m;
        }

        #endregion

        public static List<BaseUnit> UnitList => new List<BaseUnit>
        {
            Spearman,
            Swordsman,
            AxeFighter,
            Archer,
            LightCavalry,
            MountedArcher,
            HeavyCavalry,
            Ram,
            Catapult,
            Berserker,
            Trebuchet,
            Nobleman,
            Paladin,
        };

        public static List<UnitType> UnitTypeList = new List<UnitType>
        {
            UnitType.Spearman,
            UnitType.Swordsman,
            UnitType.AxeFighter,
            UnitType.Archer,
            UnitType.LightCavalry,
            UnitType.MountedArcher,
            UnitType.HeavyCavalry,
            UnitType.Ram,
            UnitType.Catapult,
            UnitType.Berserker,
            UnitType.Trebuchet,
            UnitType.Nobleman,
            UnitType.Paladin,
        };

        public static ObservableCollection<UnitRow> UnitImageList
        {
            get
            {
                ObservableCollection<UnitRow> unitImageList = new ObservableCollection<UnitRow>();

                foreach (BaseUnit baseUnit in UnitList)
                {
                    unitImageList.Add(new UnitRow
                    {
                        ImagePath = baseUnit.ImagePath,
                        Name = baseUnit.Name,
                    });
                }

                return unitImageList;
            }
        }

        public static List<FaithLevel> FaithOptions { get; } = new List<FaithLevel>
        {
            new FaithLevel
            {
                Code ="Faith_0",
                Name = "None",
                Modifier = 0.5m
            },
            new FaithLevel
            {
                Code ="Faith_1",
                Name = "Chapel",
                Modifier = 1m
            },
            new FaithLevel
            {
                Code ="Faith_2",
                Name = "Church Level 1",
                Modifier = 1m
            },
            new FaithLevel
            {
                Code ="Faith_3",
                Name = "Church Level 2",
                Modifier = 1.05m,

            },
            new FaithLevel
            {
                Code ="Faith_4",
                Name = "Church Level 3",
                Modifier = 1.1m
            }
        };

        public static List<BaseWeapon> WeaponOptions { get; } = new List<BaseWeapon>
        {
            new EmptyWeapon(),
            SpearmanWeapon,
            SwordsmanWeapon,
            AxeFighterWeapon,
            ArcherWeapon,
            LightCavalryWeapon,
            MountedArcherWeapon,
            HeavyCavalryWeapon,
            RamWeapon,
            CatapultWeapon
        };

        public static int NumberOfUnits => UnitList.Count;

        public static BaseUnit GetUnit(UnitType unitType)
        {
            if (unitType == UnitType.None) return null;

            return UnitList.First(unit => unit.UnitType == unitType);
        }

        #region Formulas
        /// <summary>
        /// Applies the killRate to the number of units and returns the number of killed units.
        /// </summary>
        /// <param name="numberOfUnits"></param>
        /// <param name="killRate"></param>
        /// <returns></returns>
        public static int GetUnitsKilled(int numberOfUnits, decimal killRate)
        {
            decimal x = numberOfUnits * killRate;
            return (int)Math.Round(x + 0.000001m, MidpointRounding.AwayFromZero);
        }

        public static decimal GetDefKillRate(int atkStrength, int defStrength)
        {
            if (atkStrength <= 0 || defStrength <= 0)
            {
                return 0;
            }

            decimal atkDefRatio = Math.Round(defStrength / (decimal)atkStrength, 9, MidpointRounding.AwayFromZero);
            decimal atkKillRate = defStrength <= atkStrength ? 1 : (decimal)Math.Sqrt(1 / (double)atkDefRatio) / atkDefRatio;

            return Math.Round(atkKillRate, 6, MidpointRounding.AwayFromZero);
        }

        public static decimal GetAtkKillRate(int atkStrength, int defStrength)
        {
            if (atkStrength <= 0 || defStrength <= 0)
            {
                return 0;
            }

            decimal atkDefRatio = Math.Round(atkStrength / (decimal)defStrength, 9, MidpointRounding.AwayFromZero);
            decimal defKillRate = atkStrength <= defStrength ? 1 : (decimal)Math.Sqrt(1 / (double)atkDefRatio) / atkDefRatio;

            return Math.Round(defKillRate, 6, MidpointRounding.AwayFromZero);
        }

        public static decimal GetUnitProvisionRatio(int unitProvisions, int totalUnitProvisions)
        {
            if (totalUnitProvisions <= 0)
            {
                return 0;
            }
            decimal x = unitProvisions / (decimal)totalUnitProvisions;
            return Math.Round(x, 6, MidpointRounding.AwayFromZero);
        }
        public static int GetUnitRatio(decimal ratio, int numberOfUnits)
        {
            decimal unitRatio = ratio * (decimal)numberOfUnits;
            int unitCount = (int)Math.Round(unitRatio, MidpointRounding.AwayFromZero);
            return unitCount;
        }

        public static decimal GetAtkBattleModifier(decimal faithBonus, decimal morale, decimal luck, decimal officerBonus)
        {
            // Based on the wiki https://en.wiki.tribalwars2.com/index.php?title=Battles
            // Math round is important 0.545 -> 0.55
            decimal y = Math.Round(faithBonus * morale * luck, 2, MidpointRounding.AwayFromZero);
            return 1m * y + officerBonus;
        }

        public static decimal GetDefBattleModifier(decimal faithBonus, int wallLevel, decimal nightBonus)
        {
            // 5% for every wall level
            decimal wallBonus = 1m + wallLevel * 0.05m;
            // Based on the wiki https://en.wiki.tribalwars2.com/index.php?title=Battles
            // Math round is important 0.545 -> 0.55
            decimal y = Math.Round(faithBonus * wallBonus * nightBonus, 2, MidpointRounding.AwayFromZero);
            return 1m * y;
        }

        public static int GetWallDefense(int wallLevel)
        {
            if (wallLevel == 0)
            {
                return 0;
            }
            return (int)Math.Round(Math.Pow(1.2515, wallLevel - 1) * 20, MidpointRounding.AwayFromZero);

        }

        public static int AddAtkModifier(int atkStrength, decimal atkModifier)
        {
            return (int)Math.Round(atkStrength * atkModifier, MidpointRounding.AwayFromZero);
        }
        public static int AddDefModifier(int defStrength, decimal defModifier)
        {
            return AddAtkModifier(defStrength, defModifier);
        }

        public static int GetRamsKilled(int numberOfRams, int numberOfCatapults, int numberOfTrebuchet)
        {
            decimal totalSiege = numberOfRams + (decimal)numberOfCatapults;
            if (totalSiege == 0)
            {
                return 0;
            }
            decimal x = numberOfTrebuchet * (numberOfRams / totalSiege);
            return (int)Math.Round(x, MidpointRounding.AwayFromZero);
        }

        public static int GetAtkFightingPower(int numberOfUnits, UnitType unitType, WeaponSet weapon)
        {
            int FightingPower = GetUnit(unitType)?.FightingPower ?? 0;
            // paladin Modifier is in percentage 0.15, 0.3 etc so add 1
            decimal paladinModifier = (unitType == weapon.BelongsToUnitType ? weapon.AtkModifier : 0) + 1;

            decimal fightingPower = FightingPower * paladinModifier;
            return (int)Math.Round(numberOfUnits * fightingPower, MidpointRounding.AwayFromZero);
        }

        #endregion
    }
}
