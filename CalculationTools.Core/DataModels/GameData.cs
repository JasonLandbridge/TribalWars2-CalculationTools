using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CalculationTools.Core.BattleSimulator;
using CalculationTools.Core.Enums;

namespace CalculationTools.Core
{
    public static class GameData
    {
        /// <summary>
        /// Assuming the game uses the same method of rounding everywhere
        /// </summary>
        private static readonly MidpointRounding GameRounding = MidpointRounding.ToPositiveInfinity;

        #region Properties

        #region Units

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

        public static Archer Archer { get; } = new Archer();
        public static AxeFighter AxeFighter { get; } = new AxeFighter();
        public static Berserker Berserker { get; } = new Berserker();
        public static Catapult Catapult { get; } = new Catapult();
        public static HeavyCavalry HeavyCavalry { get; } = new HeavyCavalry();
        public static LightCavalry LightCavalry { get; } = new LightCavalry();
        public static MountedArcher MountedArcher { get; } = new MountedArcher();
        public static Nobleman Nobleman { get; } = new Nobleman();
        public static Paladin Paladin { get; } = new Paladin();
        public static Ram Ram { get; } = new Ram();
        public static Spearman Spearman { get; } = new Spearman();
        public static Swordsman Swordsman { get; } = new Swordsman();
        public static Trebuchet Trebuchet { get; } = new Trebuchet();

        #endregion Units

        #region Weapons

        public static ArcherWeapon ArcherWeapon { get; } = new ArcherWeapon();
        public static AxeFighterWeapon AxeFighterWeapon { get; } = new AxeFighterWeapon();
        public static CatapultWeapon CatapultWeapon { get; } = new CatapultWeapon();
        public static HeavyCavalryWeapon HeavyCavalryWeapon { get; } = new HeavyCavalryWeapon();
        public static LightCavalryWeapon LightCavalryWeapon { get; } = new LightCavalryWeapon();
        public static MountedArcherWeapon MountedArcherWeapon { get; } = new MountedArcherWeapon();
        public static RamWeapon RamWeapon { get; } = new RamWeapon();
        public static SpearmanWeapon SpearmanWeapon { get; } = new SpearmanWeapon();
        public static SwordsmanWeapon SwordsmanWeapon { get; } = new SwordsmanWeapon();

        /// <summary>
        /// Returns the Attack modifier from the paladin weapon based on the weapon level.
        /// </summary>
        /// <param name="belongsToUnitType">Which <see cref="BaseUnit"/> does this weapon influence.</param>
        /// <param name="weaponLevel">The weapon level 1-3</param>
        /// <returns></returns>
        public static decimal GetAtkModifierFromWeapon(UnitType belongsToUnitType, int weaponLevel)
        {
            foreach (BaseWeapon weapon in WeaponOptions.Where(weapon => weapon.BelongsToUnitType == belongsToUnitType))
            {
                return weapon.GetAtkModifier(weaponLevel);
            }
            return 1m;
        }

        /// <summary>
        /// Returns the Defense modifier from the paladin weapon based on the weapon level.
        /// </summary>
        /// <param name="belongsToUnitType">Which <see cref="BaseUnit"/> does this weapon influence.</param>
        /// <param name="weaponLevel">The weapon level 1-3</param>
        /// <returns></returns>
        public static decimal GetDefModifierFromWeapon(UnitType belongsToUnitType, int weaponLevel)
        {
            foreach (BaseWeapon weapon in WeaponOptions.Where(weapon => weapon.BelongsToUnitType == belongsToUnitType))
            {
                return weapon.GetDefModifier(weaponLevel);
            }
            return 1m;
        }

        #endregion Weapons

        #region Buildings

        public static Wall Wall { get; } = new Wall();

        #endregion Buildings

        #region BattleMeta

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

        #endregion BattleMeta

        public static int NumberOfUnits => UnitList.Count;

        /// <summary>
        /// Returns a list of Resource Costs for all units.
        /// </summary>
        public static List<ResourceSet> UnitCostList => UnitList.Select(unit => unit.ResourceCost).ToList();

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

        #endregion Properties

        #region Methods

        public static BaseUnit GetUnit(UnitType unitType)
        {
            return UnitList.First(unit => unit.UnitType == unitType);
        }

        public static WeaponSet GetWeapon(UnitType unitType, int weaponLevel)
        {
            decimal atkModifier = GetAtkModifierFromWeapon(unitType, weaponLevel);
            decimal defModifier = GetDefModifierFromWeapon(unitType, weaponLevel);
            return new WeaponSet(unitType, atkModifier, defModifier);
        }

        #region Formulas

        #region BattleModifiers

        public static int AddAtkModifier(int atkStrength, decimal atkModifier)
        {
            return (int)Math.Round(atkStrength * atkModifier, GameRounding);
        }

        public static int AddDefModifier(int defStrength, decimal defModifier)
        {
            return AddAtkModifier(defStrength, defModifier);
        }

        public static decimal GetAtkBattleModifier(BattleMeta battleMeta)
        {
            // Based on the wiki https://en.wiki.tribalwars2.com/index.php?title=Battles
            // Multiply meta values
            decimal result = (battleMeta.AtkFaithBonus * battleMeta.MoralePercentage * battleMeta.LuckPercentage);

            // Add officer bonus and tribe bonus
            result += battleMeta.GrandMasterBonusPercentage + battleMeta.WeaponMasteryPercentage;

            // Test case of rounding by battle report: 0.91066 -> 0.92'
            result = 1m * Math.Round(result, 2, GameRounding);
            return result;
        }

        public static decimal GetDefBattleModifier(BattleMeta battleMeta)
        {
            // Based on the wiki https://en.wiki.tribalwars2.com/index.php?title=Battles

            // 5% for every wall level
            decimal wallBonus = 1m + battleMeta.WallLevel * 0.05m;

            // Math round is important 0.545 -> 0.55
            decimal result = Math.Round(battleMeta.DefFaithBonus * wallBonus * battleMeta.NightBonusPercentage, 2, GameRounding);
            return 1m * result;
        }

        #endregion BattleModifiers

        public static int GetAtkFightingPower(int numberOfUnits, UnitType unitType, WeaponSet weapon)
        {
            int FightingPower = GetUnit(unitType)?.FightingPower ?? 0;
            // paladin Modifier is in percentage 0.15, 0.3 etc so add 1
            decimal paladinModifier = (unitType == weapon.BelongsToUnitType ? weapon.AtkModifier : 0) + 1;

            decimal fightingPower = FightingPower * paladinModifier;
            return (int)Math.Round(numberOfUnits * fightingPower, GameRounding);
        }

        public static decimal GetAtkKillRate(int atkStrength, int defStrength)
        {
            if (atkStrength <= 0 || defStrength <= 0) return 0;

            decimal atkDefRatio = atkStrength / (decimal)defStrength;
            decimal defKillRate = atkStrength <= defStrength ? 1 : (decimal)Math.Sqrt(1 / (double)atkDefRatio) / atkDefRatio;

            return defKillRate;
        }

        public static decimal GetDefKillRate(int atkStrength, int defStrength)
        {
            if (atkStrength <= 0 || defStrength <= 0) return 0;

            decimal atkDefRatio = defStrength / (decimal)atkStrength;
            decimal atkKillRate = defStrength <= atkStrength ? 1 : (decimal)Math.Sqrt(1 / (double)atkDefRatio) / atkDefRatio;

            return atkKillRate;
        }

        public static int GetRamsKilled(int numberOfRams, int numberOfCatapults, int numberOfTrebuchet)
        {
            decimal totalSiege = numberOfRams + (decimal)numberOfCatapults;
            if (totalSiege == 0) return 0;

            decimal x = numberOfTrebuchet * (numberOfRams / totalSiege);
            return (int)Math.Round(x, GameRounding);
        }

        public static decimal GetUnitProvisionRatio(int unitProvisions, int totalUnitProvisions)
        {
            if (totalUnitProvisions <= 0) return 0;

            decimal x = unitProvisions / (decimal)totalUnitProvisions;
            return x;
        }

        public static int GetUnitRatio(decimal ratio, int numberOfUnits)
        {
            if (ratio == 1m) return numberOfUnits;

            decimal unitRatio = ratio * numberOfUnits;
            int unitCount = (int)Math.Round(unitRatio, MidpointRounding.AwayFromZero);
            return unitCount;
        }

        /// <summary>
        /// Applies the killRate to the number of units and returns the number of killed units.
        /// </summary>
        /// <param name="numberOfUnits"></param>
        /// <param name="killRate"></param>
        /// <returns></returns>
        public static int GetUnitsKilled(int numberOfUnits, decimal killRate)
        {
            if (numberOfUnits == 0) return 0;
            if (killRate == 1m) return numberOfUnits;

            decimal x = numberOfUnits * killRate;
            return (int)Math.Round(x + 0.000001m, MidpointRounding.AwayFromZero);
        }

        public static int GetWallDefense(int wallLevel)
        {
            if (wallLevel == 0) return 0;

            return (int)Math.Round(Math.Pow(1.2515, wallLevel - 1) * 20, GameRounding);
        }

        #endregion Formulas

        #region BattleSimulation

        public static int PreRound(ref BattleResult result, WeaponSet atkWeapon)
        {
            // Abort when there are no attacking rams or catapults, assumes nothing else damages the wall
            if (result.AtkUnits.Ram == 0 && result.AtkUnits.Catapult == 0)
            {
                result.WallLevelAfter = result.WallLevelBefore;
                return result.WallLevelBefore;
            }

            // Based on https://en.forum.tribalwars2.com/index.php?threads/unraveling-some-myths-regarding-the-battle-engine.3959/

            int wallLevel = Math.Clamp(result.WallLevelBefore, 0, 20);
            decimal atkModifier = result.AtkBattleModifier / 100m;
            int ironWall = 0; //TODO Need to make an battleCalculatorInput that takes the Tribe skill Iron wall into account
            decimal paladinModifier = (atkWeapon.BelongsToUnitType == UnitType.Ram ? atkWeapon.AtkModifier : 0) + 1;

            UnitSet atkUnits = result.AtkUnits;
            UnitSet defUnits = result.DefUnits;
            UnitSet atkUnitsLost = result.AtkUnitsLost;

            // Calculate how many rams were killed by the Trebuchet
            int ramsKilled = GetRamsKilled(atkUnits.Ram, atkUnits.Catapult, defUnits.Trebuchet);
            atkUnits.Ram -= ramsKilled;
            atkUnitsLost.Ram = ramsKilled;

            // Get the total attacking provisions without the ram provisions included.
            int totalProvisionsWithNoRams = atkUnits.GetTotalProvisions() - atkUnits.GetTotalRamProvisions();

            // Get base wall defense
            int wallDefense = GetWallDefense(wallLevel);

            // This is meant to calculate the "active" rams that will do damage in relation to the attacking force.
            // More infantry = more damage with the rams
            int provisionDefense = (defUnits.GetTotalProvisions() + wallDefense);
            decimal ramRatio = 0;
            if (provisionDefense > 0)
            {
                ramRatio = Math.Clamp((decimal)totalProvisionsWithNoRams / provisionDefense, 0, 1);
            }

            int wallHitPoints = (Wall.GetHitPoints(wallLevel) * 2);

            // This is the net wall damage done by the rams
            decimal wallDamage = (atkUnits.Ram * ramRatio * atkModifier * paladinModifier);

            // If wallHitPoints is not zero then divide by
            if (wallHitPoints > 0)
            {
                wallDamage /= wallHitPoints;
            }

            // Calculate the new wall level after damage applied
            int resultingWallLevel;
            // If the wall is already below the Iron Wall threshold then don't change anything.
            if (wallLevel <= ironWall)
            {
                resultingWallLevel = wallLevel;
            }
            else
            {
                if (wallLevel - ironWall < -wallDamage)
                {
                    resultingWallLevel = wallLevel < ironWall ? wallLevel : ironWall;
                }
                else
                {
                    decimal rawLevel = wallLevel - wallDamage;
                    resultingWallLevel = (int)Math.Round(rawLevel, GameRounding);
                }
            }

            result.WallLevelAfter = Math.Clamp(resultingWallLevel, 0, 20);
            result.AtkUnitsLost = atkUnitsLost;
            return result.WallLevelAfter;
        }

        public static BattleResult SimulateBattle(BattleConfig battleConfig)
        {
            // Based on: Tribal Wars 2 - Tutorial: Basic Battle System - https://www.youtube.com/watch?v=SG_qI1-go88
            // Based on: Battle Simulator - http://www.ds-pro.de/2/simulator.php
            BattleResult result = new BattleResult(battleConfig);

            BattleMeta battleMeta = battleConfig.BattleMeta;
            WeaponSet paladinAtkWeapon = battleConfig.GetAtkWeapon();
            WeaponSet paladinDefWeapon = battleConfig.GetDefWeapon();

            decimal atkModifier = GetAtkBattleModifier(battleMeta);
            result.AtkBattleModifier = (int)(atkModifier * 100m);
            decimal defModifier = GetDefBattleModifier(battleMeta);
            result.DefModifierBeforeBattle = (int)(defModifier * 100m);

            // Stop here if there are no units given
            if (!battleConfig.HasUnits)
            {
                return result;
            }

            // Re-calculate the defense modifier when the wall has been damaged
            int wallLevelAfterPreRound = PreRound(ref result, paladinAtkWeapon);
            battleMeta.WallLevel = wallLevelAfterPreRound;
            defModifier = GetDefBattleModifier(battleMeta);
            result.DefModifierDuringBattle = (int)(defModifier * 100m);

            List<BattleResult> BattleHistory = new List<BattleResult> { result.Copy() };
            BattleResult currentRound = BattleHistory.Last();

            // Set the atkUnits - minus the lost siege units and set that as the attacking group
            currentRound.AtkUnits -= result.AtkUnitsLost;
            currentRound.AtkUnitsLost.Clear();
            // Simulate for 3 rounds (infantry, cavalry and archers)
            bool battleDetermined = false;
            int wallDefense = result.WallDefenseAfter;

            while (!battleDetermined)
            {
                currentRound = BattleHistory.Last();

                UnitSet atkUnits = currentRound.AtkUnits;
                UnitSet atkUnitsLost = currentRound.AtkUnitsLost;
                UnitSet defUnits = currentRound.DefUnits;

                int atkInfantryProvisions = atkUnits.GetTotalInfantryProvisions();
                int atkCavalryProvisions = atkUnits.GetTotalCavalryProvisions();
                int atkArchersProvisions = atkUnits.GetTotalArcherProvisions();
                int atkSpecialProvisions = atkUnits.GetTotalSpecialProvisions();

                int totalAtkProvisions = atkInfantryProvisions + atkCavalryProvisions + atkArchersProvisions + atkSpecialProvisions;

                int totalDefProvisions = defUnits.GetTotalProvisions();
                if (totalAtkProvisions == 0)
                {
                    break;
                }

                // Get the number of units for each group, this is used to determine which group the special group joins
                int atkInfantryUnits = atkUnits.GetTotalInfantryUnits();
                int atkCavalryUnits = atkUnits.GetTotalCavalryUnits();
                int atkArchersUnits = atkUnits.GetTotalArcherUnits();
                int atkSpecialUnits = atkUnits.GetTotalSpecialUnits();

                int totalAtkUnits = atkInfantryUnits + atkCavalryUnits + atkArchersUnits + atkSpecialUnits;

                // Determine if the defense is superior, if so, then double the berserker attack strength.
                bool defSuperior = (totalAtkProvisions * 2 <= totalDefProvisions);
                int atkInfantry = atkUnits.GetTotalInfantryAttack(paladinAtkWeapon, defSuperior);
                int atkCavalry = atkUnits.GetTotalCavalryAttack(paladinAtkWeapon);
                int atkArchers = atkUnits.GetTotalArcherAttack(paladinAtkWeapon);
                int atkSpecial = atkUnits.GetTotalSpecialAtk();
                int totalAtk = atkInfantry + atkCavalry + atkArchers + atkSpecial;

                int strongestGroupIndex;

                // Determine which group the special units join during battle, the group with the highest number of units gets them.
                if (atkInfantryUnits > atkCavalryUnits && atkInfantryUnits > atkArchersUnits)
                {
                    //AtkInfantry is the strongest group
                    atkInfantry += atkSpecial;
                    atkInfantryProvisions += atkSpecialProvisions;
                    strongestGroupIndex = 1;
                }
                else if (atkCavalryUnits > atkInfantryUnits && atkCavalryUnits > atkArchersUnits)
                {
                    //atkCavalry is the strongest group
                    atkCavalry += atkSpecial;
                    atkCavalryProvisions += atkSpecialProvisions;
                    strongestGroupIndex = 2;
                }
                else
                {
                    //AtkArchers is the strongest group
                    atkArchers += atkSpecial;
                    atkArchersProvisions += atkSpecialProvisions;
                    strongestGroupIndex = 3;
                }

                // Add Atk modifier
                atkInfantry = AddAtkModifier(atkInfantry, atkModifier);
                atkCavalry = AddAtkModifier(atkCavalry, atkModifier);
                atkArchers = AddAtkModifier(atkArchers, atkModifier);

                decimal atkInfantryRatio = GetUnitProvisionRatio(atkInfantryProvisions, totalAtkProvisions);
                decimal atkCavalryRatio = GetUnitProvisionRatio(atkCavalryProvisions, totalAtkProvisions);
                decimal atkArchersRatio = GetUnitProvisionRatio(atkArchersProvisions, totalAtkProvisions);
                decimal totalRatio = atkInfantryRatio + atkCavalryRatio + atkArchersRatio;

                // These units sets contains the defensive units proportionate to the atkInfantry ratio
                UnitSet infantryGroupDefUnitSet = defUnits.GetUnitsByRatio(atkInfantryRatio);
                UnitSet cavalryGroupDefUnitSet = defUnits.GetUnitsByRatio(atkCavalryRatio);
                UnitSet archerGroupDefUnitSet = defUnits.GetUnitsByRatio(atkArchersRatio);

                int totalDefFromInfantry = infantryGroupDefUnitSet.GetTotalDefFromInfantry(paladinDefWeapon);
                int totalDefFromCavalry = cavalryGroupDefUnitSet.GetTotalDefFromCavalry(paladinDefWeapon);
                int totalDefFromArchers = archerGroupDefUnitSet.GetTotalDefFromArchers(paladinDefWeapon);

                // Add defense modifier
                totalDefFromInfantry = AddDefModifier(totalDefFromInfantry, defModifier) + wallDefense;
                totalDefFromCavalry = AddDefModifier(totalDefFromCavalry, defModifier) + wallDefense;
                totalDefFromArchers = AddDefModifier(totalDefFromArchers, defModifier) + wallDefense;

                int totalDef = totalDefFromInfantry + totalDefFromCavalry + totalDefFromArchers;

                // Used to keep track of how many units are lost this round
                UnitSet infantryGroupDefUnitSetLost = new UnitSet(), cavalryGroupDefUnitSetLost = new UnitSet(), archerGroupDefUnitSetLost = new UnitSet();

                // Determine the result of each mini-round by comparing the attack vs defense strength.
                // If both sides have 0 forces then ignore the result and leave the result undefined (null)
                bool? atkWonRound1 = null, atkWonRound2 = null, atkWonRound3 = null;
                List<bool> miniBattleResult = new List<bool>();
                if (atkInfantry > 0 && totalDefFromInfantry > 0)
                {
                    atkWonRound1 = atkInfantry >= totalDefFromInfantry;
                    miniBattleResult.Add((bool)atkWonRound1);
                }

                if (atkCavalry > 0 && totalDefFromCavalry > 0)
                {
                    atkWonRound2 = (atkCavalry >= totalDefFromCavalry);
                    miniBattleResult.Add((bool)atkWonRound2);
                }

                if (atkArchers > 0 && totalDefFromArchers > 0)
                {
                    atkWonRound3 = (atkArchers >= totalDefFromArchers);
                    miniBattleResult.Add((bool)atkWonRound3);
                }

                // For every round that Defense won, kill the attacking party and vice versa
                // General / Infantry round
                decimal killRate;
                if (atkWonRound1 != null)
                {
                    if ((bool)atkWonRound1)
                    {
                        killRate = GetAtkKillRate(atkInfantry, totalDefFromInfantry);
                        atkUnitsLost += atkUnits.ApplyKillRateAtkInfantry(killRate);
                        infantryGroupDefUnitSetLost = infantryGroupDefUnitSet.ApplyKillRate(1);
                        if (strongestGroupIndex == 1) atkUnitsLost += atkUnits.ApplyKillRateAtkSpecial(killRate);
                    }
                    else
                    {
                        killRate = GetDefKillRate(atkInfantry, totalDefFromInfantry);
                        atkUnitsLost += atkUnits.ApplyKillRateAtkInfantry(1);
                        atkUnitsLost += atkUnits.ApplyKillRateAtkSpecial(1);
                        infantryGroupDefUnitSetLost = infantryGroupDefUnitSet.ApplyKillRate(killRate);
                    }
                }

                // Cavalry round
                if (atkWonRound2 != null)
                {
                    if ((bool)atkWonRound2)
                    {
                        killRate = GetAtkKillRate(atkCavalry, totalDefFromCavalry);
                        atkUnitsLost += atkUnits.ApplyKillRateAtkCavalry(killRate);
                        cavalryGroupDefUnitSetLost = cavalryGroupDefUnitSet.ApplyKillRate(1);
                        if (strongestGroupIndex == 2) atkUnitsLost += atkUnits.ApplyKillRateAtkSpecial(killRate);
                    }
                    else
                    {
                        killRate = GetDefKillRate(atkCavalry, totalDefFromCavalry);
                        atkUnitsLost += atkUnits.ApplyKillRateAtkCavalry(1);
                        atkUnitsLost += atkUnits.ApplyKillRateAtkSpecial(1);
                        cavalryGroupDefUnitSetLost = cavalryGroupDefUnitSet.ApplyKillRate(killRate);
                    }
                }

                // Archer round
                if (atkWonRound3 != null)
                {
                    if ((bool)atkWonRound3)
                    {
                        killRate = GetAtkKillRate(atkArchers, totalDefFromArchers);
                        atkUnitsLost += atkUnits.ApplyKillRateAtkArchers(killRate);
                        archerGroupDefUnitSetLost = archerGroupDefUnitSet.ApplyKillRate(1);
                        if (strongestGroupIndex == 3) atkUnitsLost += atkUnits.ApplyKillRateAtkSpecial(killRate);
                    }
                    else
                    {
                        killRate = GetDefKillRate(atkArchers, totalDefFromArchers);
                        atkUnitsLost += atkUnits.ApplyKillRateAtkArchers(1);
                        atkUnitsLost += atkUnits.ApplyKillRateAtkSpecial(1);
                        archerGroupDefUnitSetLost = archerGroupDefUnitSet.ApplyKillRate(killRate);
                    }
                }

                UnitSet survivingDefUnits = infantryGroupDefUnitSet + cavalryGroupDefUnitSet + archerGroupDefUnitSet;
                UnitSet defUnitsLost = infantryGroupDefUnitSetLost + cavalryGroupDefUnitSetLost + archerGroupDefUnitSetLost;

                currentRound.AtkUnits = atkUnits;
                currentRound.AtkUnitsLost = atkUnitsLost;
                currentRound.DefUnits = survivingDefUnits;
                currentRound.DefUnitsLost = defUnitsLost;

                // Check if during the 3 mini-battles either attack of defense won all mini-battles
                battleDetermined = !(miniBattleResult.Contains(false) && miniBattleResult.Contains(true));
                if (!battleDetermined)
                {
                    // WallDefense is only added the first round
                    wallDefense = 0;
                    BattleHistory.Add(currentRound.Copy());
                    //Reset the UnitLost for subsequent rounds
                    BattleHistory.Last().AtkUnitsLost = new UnitSet();
                    BattleHistory.Last().DefUnitsLost = new UnitSet();
                }
            }

            BattleResult finalResult = result.Copy();

            foreach (BattleResult battleResult in BattleHistory)
            {
                finalResult.AtkUnitsLost += battleResult.AtkUnitsLost;
                finalResult.DefUnitsLost += battleResult.DefUnitsLost;
            }

            // Simulate the post battle calculations
            PostBattle(ref finalResult, paladinAtkWeapon);

            return finalResult;
        }

        public static void PostBattle(ref BattleResult result, WeaponSet atkWeapon)
        {
            if (result.WallLevelAfter == 0)
            {
                return;
            }
            decimal atkModifier = result.AtkBattleModifier / 100m;

            decimal paladinModifier = (atkWeapon.BelongsToUnitType == UnitType.Ram ? atkWeapon.AtkModifier : 0) + 1;

            int wallHitPoints = Wall.GetHitPoints(result.WallLevelAfter) * 2;
            decimal damage = (result.AtkUnitsLeft.Ram * atkModifier * paladinModifier) / wallHitPoints;
            decimal wallDamage = damage / wallHitPoints;

            // Calculate the new wall level after damage applied
            int finalWallLevel;
            int afterBattleWall = result.WallLevelAfter;
            int ironWall = 0; //TODO Need to make an battleConfig that takes the Tribe skill Iron wall into account
            // If the wall is already below the Iron Wall threshold then don't change anything.
            if (afterBattleWall <= ironWall)
            {
                finalWallLevel = afterBattleWall;
            }
            else
            {
                if (afterBattleWall - ironWall < -wallDamage)
                {
                    finalWallLevel = afterBattleWall < ironWall ? afterBattleWall : ironWall;
                }
                else
                {
                    decimal rawLevel = Math.Clamp(afterBattleWall - wallDamage, 0, 20);
                    finalWallLevel = (int)Math.Round(rawLevel, GameRounding);
                }
            }
            result.WallLevelFinal = finalWallLevel;
        }

        #endregion BattleSimulation

        #endregion Methods
    }
}