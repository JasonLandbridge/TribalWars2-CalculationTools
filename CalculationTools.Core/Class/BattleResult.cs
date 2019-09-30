using System.Collections.Generic;

namespace CalculationTools.Core
{
    public class BattleResult
    {
        #region Attacking

        public UnitSet AtkUnits { get; set; }
        public UnitSet AtkUnitsLost;
        public UnitSet AtkUnitsLeft => AtkUnits - AtkUnitsLost;

        #endregion

        #region Defending

        public UnitSet DefUnits { get; set; }
        public UnitSet DefUnitsLost;
        public UnitSet DefUnitsLeft => DefUnits - DefUnitsLost;

        #endregion

        #region Properties

        public int AtkBattleModifier { get; set; }
        public int DefBattleModifier { get; set; }

        public int WallLevelBefore { get; set; }
        public int WallLevelAfter { get; set; }
        public int WallDefenseAfter => GameData.GetWallDefense(WallLevelAfter);
        public int WallLevelFinal { get; set; }



        public bool ShowWallResult => WallLevelBefore != WallLevelAfter;
        public string WallResult => ShowWallResult ? $"Wall went from level {WallLevelBefore} to {WallLevelAfter}." : "No damage was done to the wall.";

        #endregion Properties

        #region ResultList

        public List<BattleResultValue> ListOfAtkNumbers => AtkUnits.ToBattleResultList();
        public List<BattleResultValue> ListOfAtkLostNumbers => AtkUnitsLost.ToBattleResultList();
        public List<BattleResultValue> ListOfAtkLeftNumbers => AtkUnitsLeft.ToBattleResultList();


        public List<BattleResultValue> ListOfDefNumbers => DefUnits.ToBattleResultList();
        public List<BattleResultValue> ListOfDefLostNumbers => DefUnitsLost.ToBattleResultList();
        public List<BattleResultValue> ListOfDefLeftNumbers => DefUnitsLeft.ToBattleResultList();

        #endregion

        #region Methods

        public BattleResult Copy()
        {
            return (BattleResult)this.MemberwiseClone();
        }

        #endregion Methods

    }
}
