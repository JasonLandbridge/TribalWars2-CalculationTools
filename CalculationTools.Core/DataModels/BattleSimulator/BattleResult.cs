using System.Collections.Generic;
using System.Linq;

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
            AtkUnits = battleConfig.AtkUnits;
            DefUnits = battleConfig.DefUnits;
        }

        #endregion Constructors

        #region Properties

        public int AtkBattleModifier { get; set; }
        public BattleConfig BattleConfig { get; set; }

        #region Attacking

        public UnitSet AtkUnitsLost { get; set; } = new UnitSet();

        public UnitSet AtkUnits { get; set; } = new UnitSet();

        public UnitSet AtkUnitsLeft => AtkUnits - AtkUnitsLost;
        public WeaponSet AtkWeapon { get; set; }

        #endregion Attacking

        #region Defending

        public UnitSet DefUnitsLost { get; set; } = new UnitSet();

        public UnitSet DefUnits { get; set; } = new UnitSet();

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

        public BattleResultRow ListOfAtkFromArchers
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
                return new BattleResultRow(list);
            }
        }

        public BattleResultRow ListOfAtkFromCavalry
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
                return new BattleResultRow(list);
            }
        }

        public BattleResultRow ListOfAtkFromInfantry
        {
            get
            {
                BattleResultRow battleResultRow = new BattleResultRow();

                for (int i = 0; i < GameData.UnitList.Count; i++)
                {
                    int attackPower = 0;
                    if (GameData.UnitList[i].AttackType == AttackType.Infantry)
                    {
                        attackPower = GameData.GetAtkFightingPower(AtkUnits.UnitList[i], (UnitType)i, AtkWeapon);
                    }
                    battleResultRow.Add(attackPower);
                }
                return battleResultRow;
            }
        }
        public BattleResultRow ListOfAtkNumbers => AtkUnits.ToBattleResultRow();

        #region Losses

        public BattleResultRow ListOfAtkLostNumbers => AtkUnitsLost.ToBattleResultRow();

        #region ResourcesLost
        public BattleResultRow AtkWoodLost
        {
            get
            {
                BattleResultRow resultRow = new BattleResultRow();

                for (int i = 0; i < AtkUnitsLost.UnitList.Count; i++)
                {
                    resultRow.Add(AtkUnitsLost.UnitList[i] * GameData.UnitCostList[i].Wood);
                }

                return resultRow;

            }
        }

        public BattleResultRow AtkClayLost
        {
            get
            {
                BattleResultRow resultRow = new BattleResultRow();

                for (int i = 0; i < AtkUnitsLost.UnitList.Count; i++)
                {
                    resultRow.Add(AtkUnitsLost.UnitList[i] * GameData.UnitCostList[i].Clay);
                }

                return resultRow;
            }
        }

        public BattleResultRow AtkIronLost
        {
            get
            {
                BattleResultRow resultRow = new BattleResultRow();

                for (int i = 0; i < AtkUnitsLost.UnitList.Count; i++)
                {
                    resultRow.Add(AtkUnitsLost.UnitList[i] * GameData.UnitCostList[i].Iron);
                }

                return resultRow;
            }
        }

        public BattleResultRow AtkTotalResourcesLost => AtkWoodLost + AtkClayLost + AtkIronLost;




        #endregion

        #endregion

        public BattleResultRow ListOfAtkLeftNumbers => AtkUnitsLeft.ToBattleResultRow();
        #endregion AtkResults

        #region DefResults

        public BattleResultRow ListOfDefFromArchers => new BattleResultRow(DefUnits.UnitList.Select((unitAmount, i) => new BattleResultValueViewModel(unitAmount * GameData.UnitList[i].DefenseFromArchers)).ToList());
        public BattleResultRow ListOfDefFromCavalry => new BattleResultRow(DefUnits.UnitList.Select((unitAmount, i) => new BattleResultValueViewModel(unitAmount * GameData.UnitList[i].DefenseFromCavalry)).ToList());
        public BattleResultRow ListOfDefFromInfantry => new BattleResultRow(DefUnits.UnitList.Select((unitAmount, i) => new BattleResultValueViewModel(unitAmount * GameData.UnitList[i].DefenseFromInfantry)).ToList());
        public BattleResultRow ListOfDefLeftNumbers => DefUnitsLeft.ToBattleResultRow();
        public BattleResultRow ListOfDefNumbers => DefUnits.ToBattleResultRow();

        #region Losses
        public BattleResultRow ListOfDefLostNumbers => DefUnitsLost.ToBattleResultRow();

        #region ResourcesLost
        public BattleResultRow DefWoodLost
        {
            get
            {
                BattleResultRow resultRow = new BattleResultRow();

                for (int i = 0; i < DefUnitsLost.UnitList.Count; i++)
                {
                    resultRow.Add(DefUnitsLost.UnitList[i] * GameData.UnitCostList[i].Wood);
                }

                return resultRow;
            }
        }

        public BattleResultRow DefClayLost
        {
            get
            {
                BattleResultRow resultRow = new BattleResultRow();

                for (int i = 0; i < DefUnitsLost.UnitList.Count; i++)
                {
                    resultRow.Add(DefUnitsLost.UnitList[i] * GameData.UnitCostList[i].Clay);
                }

                return resultRow;
            }
        }

        public BattleResultRow DefIronLost
        {
            get
            {
                BattleResultRow resultRow = new BattleResultRow();

                for (int i = 0; i < DefUnitsLost.UnitList.Count; i++)
                {
                    resultRow.Add(DefUnitsLost.UnitList[i] * GameData.UnitCostList[i].Iron);
                }

                return resultRow;
            }
        }

        public BattleResultRow DefTotalResourcesLost => DefWoodLost + DefClayLost + DefIronLost;
        #endregion
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