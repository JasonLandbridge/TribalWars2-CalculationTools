using CalculationTools.Core;
using CalculationTools.WebSocket;
using Xunit;

namespace CalculationTools.Tests
{
    public static class BattleSimulatorTests
    {
        [Theory]
        [MemberData(nameof(BattleSimulatorTestData.BattleTestCaseIndexes), MemberType = typeof(BattleSimulatorTestData))]
        public static void AssertBattleModifierAccuracy(int i)
        {
            //Arrange
            BattleConfig battleConfig = (BattleConfig)BattleSimulatorTestData.BattleTestCases[i][0];
            RealBattleResult realBattleResult = (RealBattleResult)BattleSimulatorTestData.BattleTestCases[i][1];
            //Act
            BattleResult result = GameData.SimulateBattle(battleConfig);

            //Assert
            Assert.Equal(realBattleResult.AtkBattleModifier, result.AtkBattleModifier);
            Assert.Equal(realBattleResult.DefModifierBeforeBattle, result.DefModifierBeforeBattle);
            Assert.Equal(realBattleResult.DefModifierDuringBattle, result.DefModifierDuringBattle);
        }

        [Theory]
        [MemberData(nameof(BattleSimulatorTestData.BattleTestCaseIndexes), MemberType = typeof(BattleSimulatorTestData))]
        public static void AssertBattleSimulatorAccuracy(int @case)
        {
            //Arrange
            BattleConfig battleConfig = (BattleConfig)BattleSimulatorTestData.BattleTestCases[@case][0];
            RealBattleResult realBattleResult = (RealBattleResult)BattleSimulatorTestData.BattleTestCases[@case][1];
            //Act
            BattleResult result = GameData.SimulateBattle(battleConfig);

            //Assert
            Assert.Equal(realBattleResult.AtkUnits, result.AtkUnits);
            Assert.Equal(realBattleResult.AtkUnitsLost, result.AtkUnitsLost);

            Assert.Equal(realBattleResult.DefUnits, result.DefUnits);
            Assert.Equal(realBattleResult.DefUnitsLost, result.DefUnitsLost);

            Assert.Equal(realBattleResult.WallLevelBefore, result.WallLevelBefore);
            Assert.Equal(realBattleResult.WallLevelAfter, result.WallLevelAfter);
            Assert.Equal(realBattleResult.WallLevelFinal, result.WallLevelFinal);
        }
    }
}