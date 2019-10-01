using System.Collections.Generic;

namespace CalculationTools.Core.BattleSimulator
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

        public List<BattleResultValueViewModel> ListOfAtkNumbers => AtkUnits.ToBattleResultList();
        public List<BattleResultValueViewModel> ListOfAtkLostNumbers => AtkUnitsLost.ToBattleResultList();
        public List<BattleResultValueViewModel> ListOfAtkLeftNumbers => AtkUnitsLeft.ToBattleResultList();


        public List<BattleResultValueViewModel> ListOfDefNumbers => DefUnits.ToBattleResultList();
        public List<BattleResultValueViewModel> ListOfDefLostNumbers => DefUnitsLost.ToBattleResultList();
        public List<BattleResultValueViewModel> ListOfDefLeftNumbers => DefUnitsLeft.ToBattleResultList();

        #endregion

        #region Methods

        public BattleResult Copy()
        {
            return (BattleResult)this.MemberwiseClone();
        }

        #endregion Methods

    }
}
