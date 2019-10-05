using System.Security.Authentication.ExtendedProtection;
using CalculationTools.Core.Base;
using CalculationTools.Core.BattleSimulator;

namespace CalculationTools.Core
{
    public class BattleResultTableViewModel : BaseViewModel
    {
        #region Properties

        public string BattleModifier { get; set; }
        public string Header { get; set; }

        /// <summary>
        /// Returns whether this table is for the attacking units
        /// </summary>
        public bool IsAttackTable { get; set; }

        /// <summary>
        /// Returns whether this table is for the defending units
        /// </summary>
        public bool IsDefenseTable => !IsAttackTable;

        public bool ShowWallResult
        {
            get => _showWallResult;
            set
            {
                // Hide the battleResultValues if the wall result is shown. 
                if (value)
                {
                    WallResult.BattleResultValues = null;
                }
                else
                {
                    WallResult.Content = string.Empty;
                }
                _showWallResult = value;

            }
        }

        public BattleResultRowViewModel UnitAmount { get; set; } = new BattleResultRowViewModel();
        public BattleResultRowViewModel UnitsLeft { get; set; } = new BattleResultRowViewModel();
        public BattleResultRowViewModel WallResult { get; set; } = new BattleResultRowViewModel();
        private bool _showWallResult = false;

        #region AttackStrength

        public BattleResultRowViewModel AttackFromArchers { get; set; } = new BattleResultRowViewModel();
        public BattleResultRowViewModel AttackFromCavalry { get; set; } = new BattleResultRowViewModel();
        public BattleResultRowViewModel AttackFromInfantry { get; set; } = new BattleResultRowViewModel();
        #endregion

        #region DefenseStrength

        public BattleResultRowViewModel DefenseFromArchers { get; set; } = new BattleResultRowViewModel();
        public BattleResultRowViewModel DefenseFromCavalry { get; set; } = new BattleResultRowViewModel();
        public BattleResultRowViewModel DefenseFromInfantry { get; set; } = new BattleResultRowViewModel();
        #endregion
        #region UnitsLost
        public BattleResultRowViewModel UnitLost { get; set; } = new BattleResultRowViewModel();
        public BattleResultRowViewModel UnitsLostClay { get; set; } = new BattleResultRowViewModel();
        public BattleResultRowViewModel UnitsLostIron { get; set; } = new BattleResultRowViewModel();
        public BattleResultRowViewModel UnitsLostWood { get; set; } = new BattleResultRowViewModel();
        #endregion
        #region Filters

        public bool IsAttackStrengthShown { get; set; }
        public bool IsDefenseStrengthShown { get; set; }
        public bool IsResourcesLostShown { get; set; }

        #endregion


        #endregion Properties


        #region Constructors

        public BattleResultTableViewModel()
        {
            UnitAmount.Header = "Amount";

            AttackFromInfantry.Header = "Infantry Strength";
            AttackFromCavalry.Header = "Cavalry Strength";
            AttackFromArchers.Header = "Archer Strength";

            DefenseFromInfantry.Header = "Against Infantry";
            DefenseFromCavalry.Header = "Against Cavalry";
            DefenseFromArchers.Header = "Against Archers";

            UnitLost.Header = "Lost units";
            UnitsLostWood.Header = "Wood Loss";
            UnitsLostClay.Header = "Clay Loss";
            UnitsLostIron.Header = "Iron Loss";

            UnitsLeft.Header = "Survivors";
            WallResult.Header = "Wall";

        }

        #endregion Constructors
    }
}