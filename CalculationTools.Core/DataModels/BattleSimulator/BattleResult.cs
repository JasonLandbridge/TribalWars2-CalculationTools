using System.Collections.Generic;
using System.Linq;
using CalculationTools.Core.Enums;

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

        #region AtkResults

        public List<BattleResultValueViewModel> ListOfAtkNumbers => AtkUnits.ToBattleResultList();
        public List<BattleResultValueViewModel> ListOfAtkLostNumbers => AtkUnitsLost.ToBattleResultList();
        public List<BattleResultValueViewModel> ListOfAtkLostWood
        {
            get
            {
                List<ResourceSet> resourceCosts = GameData.UnitCostList;
                return AtkUnitsLost.UnitList.Select((unitAmount, i) => new BattleResultValueViewModel(unitAmount * resourceCosts[i].Wood)).ToList();
            }
        }
        public List<BattleResultValueViewModel> ListOfAtkLostClay
        {
            get
            {
                List<ResourceSet> resourceCosts = GameData.UnitCostList;
                return AtkUnitsLost.UnitList.Select((unitAmount, i) => new BattleResultValueViewModel(unitAmount * resourceCosts[i].Clay)).ToList();
            }
        }
        public List<BattleResultValueViewModel> ListOfAtkLostIron
        {
            get
            {
                List<ResourceSet> resourceCosts = GameData.UnitCostList;
                return AtkUnitsLost.UnitList.Select((unitAmount, i) => new BattleResultValueViewModel(unitAmount * resourceCosts[i].Iron)).ToList();
            }
        }

        public List<BattleResultValueViewModel> ListOfAtkLeftNumbers => AtkUnitsLeft.ToBattleResultList();

        #endregion

        #region DefResults
        public List<BattleResultValueViewModel> ListOfDefNumbers => DefUnits.ToBattleResultList();
        public List<BattleResultValueViewModel> ListOfDefLostNumbers => DefUnitsLost.ToBattleResultList();
        public List<BattleResultValueViewModel> ListOfDefLostWood
        {
            get
            {
                List<ResourceSet> resourceCosts = GameData.UnitCostList;
                return DefUnitsLost.UnitList.Select((unitAmount, i) => new BattleResultValueViewModel(unitAmount * resourceCosts[i].Wood)).ToList();
            }
        }
        public List<BattleResultValueViewModel> ListOfDefLostClay
        {
            get
            {
                List<ResourceSet> resourceCosts = GameData.UnitCostList;
                return DefUnitsLost.UnitList.Select((unitAmount, i) => new BattleResultValueViewModel(unitAmount * resourceCosts[i].Clay)).ToList();
            }
        }
        public List<BattleResultValueViewModel> ListOfDefLostIron
        {
            get
            {
                List<ResourceSet> resourceCosts = GameData.UnitCostList;
                return DefUnitsLost.UnitList.Select((unitAmount, i) => new BattleResultValueViewModel(unitAmount * resourceCosts[i].Iron)).ToList();
            }
        }
        public List<BattleResultValueViewModel> ListOfDefLeftNumbers => DefUnitsLeft.ToBattleResultList();

        #endregion

        #endregion

        #region Methods

        public BattleResult Copy()
        {
            return (BattleResult)this.MemberwiseClone();
        }

        #endregion Methods

    }
}
