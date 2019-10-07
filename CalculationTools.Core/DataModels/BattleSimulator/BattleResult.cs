using System.Collections.Generic;
using System.Linq;
using CalculationTools.Core.Enums;

namespace CalculationTools.Core
{
    public class BattleResult
    {
        #region Constructors

        public BattleResult()
        {
        }

        public BattleResult(BattleConfig battleConfig)
        {
            BattleConfig = battleConfig;
        }

        #endregion Constructors

        #region Properties

        public int AtkBattleModifier { get; set; }
        public BattleConfig BattleConfig { get; set; }

        #region Attacking

        public UnitSet AtkUnitsLost;

        public UnitSet AtkUnits
        {
            get => BattleConfig.AtkUnits;
            set => BattleConfig.SetAtkUnits(value);
        }

        public UnitSet AtkUnitsLeft => AtkUnits - AtkUnitsLost;
        public WeaponSet AtkWeapon { get; set; }

        #endregion Attacking

        #region Defending

        public UnitSet DefUnitsLost;

        public UnitSet DefUnits
        {
            get => BattleConfig.DefUnits;
            set => BattleConfig.SetDefUnits(value);
        }

        public UnitSet DefUnitsLeft => DefUnits - DefUnitsLost;
        public WeaponSet DefWeapon { get; set; }

        #endregion Defending

        /// <summary>
        /// The defense modifier before the battle has started
        /// </summary>
        public int DefModifierBeforeBattle { get; set; }

        /// <summary>
        /// The defense modifer after the pre-round, where the rams/cats have damaged the wall.
        /// </summary>
        public int DefModifierDuringBattle { get; set; }

        public bool ShowWallResult => WallLevelBefore != WallLevelAfter;
        public int WallDefenseAfter => GameData.GetWallDefense(WallLevelAfter);
        public int WallLevelAfter { get; set; }
        public int WallLevelBefore => BattleConfig.BattleMeta.WallLevel;
        public int WallLevelFinal { get; set; }
        public string WallResult => ShowWallResult ? $"Wall went from level {WallLevelBefore}, to {WallLevelAfter} during the battle and finally {WallLevelFinal}." : "No damage was done to the wall.";

        #region ResultList

        #region AtkResults

        public List<BattleResultValueViewModel> ListOfAtkFromArchers
        {
            get
            {
                List<BattleResultValueViewModel> list = new List<BattleResultValueViewModel>();

                for (int i = 0; i < GameData.UnitList.Count; i++)
                {
                    BattleResultValueViewModel battleResultValue = new BattleResultValueViewModel();
                    if (GameData.UnitList[i].AttackType == AttackType.Archer)
                    {
                        battleResultValue.Value = GameData.GetAtkFightingPower(AtkUnits.UnitList[i], (UnitType)i, AtkWeapon);
                    }
                    list.Add(battleResultValue);
                }
                return list;
            }
        }

        public List<BattleResultValueViewModel> ListOfAtkFromCavalry
        {
            get
            {
                List<BattleResultValueViewModel> list = new List<BattleResultValueViewModel>();

                for (int i = 0; i < GameData.UnitList.Count; i++)
                {
                    BattleResultValueViewModel battleResultValue = new BattleResultValueViewModel();
                    if (GameData.UnitList[i].AttackType == AttackType.Cavalry)
                    {
                        battleResultValue.Value = GameData.GetAtkFightingPower(AtkUnits.UnitList[i], (UnitType)i, AtkWeapon);
                    }
                    list.Add(battleResultValue);
                }
                return list;
            }
        }

        public List<BattleResultValueViewModel> ListOfAtkFromInfantry
        {
            get
            {
                List<BattleResultValueViewModel> list = new List<BattleResultValueViewModel>();

                for (int i = 0; i < GameData.UnitList.Count; i++)
                {
                    BattleResultValueViewModel battleResultValue = new BattleResultValueViewModel();
                    if (GameData.UnitList[i].AttackType == AttackType.Infantry)
                    {
                        battleResultValue.Value = GameData.GetAtkFightingPower(AtkUnits.UnitList[i], (UnitType)i, AtkWeapon);
                    }
                    list.Add(battleResultValue);
                }
                return list;
            }
        }

        public List<BattleResultValueViewModel> ListOfAtkLeftNumbers => AtkUnitsLeft.ToBattleResultList();

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

        public List<BattleResultValueViewModel> ListOfAtkLostNumbers => AtkUnitsLost.ToBattleResultList();

        public List<BattleResultValueViewModel> ListOfAtkLostWood
        {
            get
            {
                List<ResourceSet> resourceCosts = GameData.UnitCostList;
                return AtkUnitsLost.UnitList.Select((unitAmount, i) => new BattleResultValueViewModel(unitAmount * resourceCosts[i].Wood)).ToList();
            }
        }

        public List<BattleResultValueViewModel> ListOfAtkNumbers => AtkUnits.ToBattleResultList();

        #endregion AtkResults

        #region DefResults

        public List<BattleResultValueViewModel> ListOfDefFromArchers => DefUnits.UnitList.Select((unitAmount, i) => new BattleResultValueViewModel(unitAmount * GameData.UnitList[i].DefenseFromArchers)).ToList();
        public List<BattleResultValueViewModel> ListOfDefFromCavalry => DefUnits.UnitList.Select((unitAmount, i) => new BattleResultValueViewModel(unitAmount * GameData.UnitList[i].DefenseFromCavalry)).ToList();
        public List<BattleResultValueViewModel> ListOfDefFromInfantry => DefUnits.UnitList.Select((unitAmount, i) => new BattleResultValueViewModel(unitAmount * GameData.UnitList[i].DefenseFromInfantry)).ToList();
        public List<BattleResultValueViewModel> ListOfDefLeftNumbers => DefUnitsLeft.ToBattleResultList();
        public List<BattleResultValueViewModel> ListOfDefNumbers => DefUnits.ToBattleResultList();

        #region Losses

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

        public List<BattleResultValueViewModel> ListOfDefLostNumbers => DefUnitsLost.ToBattleResultList();

        public List<BattleResultValueViewModel> ListOfDefLostWood
        {
            get
            {
                List<ResourceSet> resourceCosts = GameData.UnitCostList;
                return DefUnitsLost.UnitList.Select((unitAmount, i) => new BattleResultValueViewModel(unitAmount * resourceCosts[i].Wood)).ToList();
            }
        }

        #endregion Losses

        #endregion DefResults

        #endregion ResultList

        #endregion Properties

        #region Methods

        public BattleResult Copy()
        {
            return (BattleResult)this.MemberwiseClone();
        }

        #endregion Methods
    }
}