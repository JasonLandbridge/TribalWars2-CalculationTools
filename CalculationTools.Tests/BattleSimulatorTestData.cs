using CalculationTools.Core;
using System.Collections.Generic;

namespace CalculationTools.Tests
{
    public static class BattleSimulatorTestData
    {
        #region TestCase1

        private static readonly RealBattleResult RealBattleResult1 = new RealBattleResult
        {
            AtkBattleModifier = 79,
            AtkUnits = new UnitSet
            {
                AxeFighter = 5479,
                LightCavalry = 1919,
                MountedArcher = 1620,
                Ram = 300,
                Paladin = 1
            },
            AtkUnitsLost = new UnitSet
            {
                AxeFighter = 2929,
                LightCavalry = 579,
                MountedArcher = 781,
                Ram = 161,
                Paladin = 1
            },
            DefModifierBeforeBattle = 200,
            DefModifierDuringBattle = 165,
            DefUnits = new UnitSet
            {
                Spearman = 3000,
                Swordsman = 3000,
                Archer = 1327,
            },
            DefUnitsLost = new UnitSet
            {
                Spearman = 3000,
                Swordsman = 3000,
                Archer = 1327,
            },
            WallLevelBefore = 20,
            WallLevelAfter = 13,
            WallLevelFinal = 7
        };

        private static readonly BattleConfig BattleConfig1 = new BattleConfig
        {
            AtkUnits = RealBattleResult1.AtkUnits,

            DefUnits = RealBattleResult1.DefUnits,

            BattleMeta = new BattleMeta
            {
                GrandmasterBonus = true,
                AtkWeaponLevel = 2,
                AtkWeapon = GameData.AxeFighterWeapon,
                DefWeaponLevel = 0,
                DefWeapon = GameData.WeaponOptions[0],
                AtkChurch = GameData.FaithOptions[4],
                DefChurch = GameData.FaithOptions[1],
                Luck = -7,
                Morale = 63,
                NightBonus = false,
                WeaponMastery = 2,
                WallLevel = 20
            }
        };

        #endregion TestCase1

        #region TestCase2

        private static readonly RealBattleResult RealBattleResult2 = new RealBattleResult
        {
            AtkBattleModifier = 92,
            AtkUnits = new UnitSet
            {
                AxeFighter = 13469,
                LightCavalry = 1645,
                MountedArcher = 1021,
                Paladin = 1
            },
            AtkUnitsLost = new UnitSet
            {
                AxeFighter = 3131,
                LightCavalry = 654,
                MountedArcher = 654,
                Paladin = 0
            },
            DefModifierBeforeBattle = 200,
            DefModifierDuringBattle = 200,
            DefUnits = new UnitSet
            {
                Spearman = 2000,
                Swordsman = 2000,
                AxeFighter = 4300,
                Archer = 2000,
                LightCavalry = 45,
                MountedArcher = 1,
                Ram = 300,
                Catapult = 10,
                Paladin = 1
            },
            DefUnitsLost = new UnitSet
            {
                Spearman = 2000,
                Swordsman = 2000,
                AxeFighter = 4300,
                Archer = 2000,
                LightCavalry = 45,
                MountedArcher = 1,
                Ram = 300,
                Catapult = 10,
                Paladin = 1
            },
            WallLevelBefore = 20,
            WallLevelAfter = 20,
            WallLevelFinal = 20,
        };

        private static readonly BattleConfig BattleConfig2 = new BattleConfig
        {
            AtkUnits = RealBattleResult2.AtkUnits,

            DefUnits = RealBattleResult2.DefUnits,

            BattleMeta = new BattleMeta
            {
                GrandmasterBonus = true,
                AtkWeaponLevel = 2,
                AtkWeapon = GameData.AxeFighterWeapon,
                DefWeaponLevel = 0,
                DefWeapon = GameData.WeaponOptions[0],
                AtkChurch = GameData.FaithOptions[4],
                DefChurch = GameData.FaithOptions[1],
                Luck = 13,
                Morale = 62,
                NightBonus = false,
                WeaponMastery = 2,
                WallLevel = 20
            }
        };

        #endregion TestCase2

        #region TestCase3

        /// <summary>
        /// 28-09-2019 17:18:17 - Attack report
        /// </summary>
        private static readonly RealBattleResult RealBattleResult3 = new RealBattleResult
        {
            AtkBattleModifier = 111,
            AtkUnits = new UnitSet
            {
                AxeFighter = 2550,
                LightCavalry = 1124,
                MountedArcher = 200,
                Ram = 224
            },
            AtkUnitsLost = new UnitSet
            {
                AxeFighter = 2550,
                LightCavalry = 1124,
                MountedArcher = 200,
                Ram = 224
            },
            DefModifierBeforeBattle = 140,
            DefModifierDuringBattle = 110,
            DefUnits = new UnitSet
            {
                AxeFighter = 37,
                HeavyCavalry = 4300,
            },
            DefUnitsLost = new UnitSet
            {
                AxeFighter = 9,
                HeavyCavalry = 1047,
            },
            WallLevelBefore = 8,
            WallLevelAfter = 2,
            WallLevelFinal = 2,
        };

        private static readonly BattleConfig BattleConfig3 = new BattleConfig
        {
            AtkUnits = RealBattleResult3.AtkUnits,

            DefUnits = RealBattleResult3.DefUnits,

            BattleMeta = new BattleMeta
            {
                GrandmasterBonus = false,
                AtkWeaponLevel = 0,
                AtkWeapon = GameData.WeaponOptions[0],
                DefWeaponLevel = 0,
                DefWeapon = GameData.WeaponOptions[0],
                AtkChurch = GameData.FaithOptions[1],
                DefChurch = GameData.FaithOptions[1],
                Luck = 11,
                Morale = 100,
                NightBonus = false,
                WeaponMastery = 0,
                WallLevel = 8
            }
        };

        #endregion TestCase3

        public static readonly List<object[]> BattleTestCases = new List<object[]>
        {
            new object[]{ BattleConfig1, RealBattleResult1},
            new object[]{ BattleConfig2, RealBattleResult2},
            new object[]{ BattleConfig3, RealBattleResult3},
        };

        public static IEnumerable<object[]> BattleTestCaseIndexes
        {
            get
            {
                List<object[]> tmp = new List<object[]>();
                for (int i = 0; i < BattleTestCases.Count; i++)
                    tmp.Add(new object[] { i });
                return tmp;
            }
        }
    }
}