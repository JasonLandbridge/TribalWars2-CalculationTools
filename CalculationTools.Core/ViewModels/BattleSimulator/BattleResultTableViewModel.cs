using System.Collections.Generic;
using CalculationTools.Core.Base;

namespace CalculationTools.Core.BattleSimulator
{
    public class BattleResultTableViewModel : BaseViewModel
    {
        #region Properties

        public string Header { get; set; }

        private bool _showWallResult = false;
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

        public string BattleModifier { get; set; }

        /// <summary>
        /// This list is used to do batch operations such as naming all headers in one go. 
        /// The order is important, don't change!'
        /// </summary>
        public List<BattleResultRowViewModel> TableRows => new List<BattleResultRowViewModel>
        {
            UnitAmount,
            UnitLost,
            UnitsLostWood,
            UnitsLostClay,
            UnitsLostIron,
            UnitsLeft,
            WallResult,
        };

        public BattleResultRowViewModel UnitAmount { get; set; } = new BattleResultRowViewModel();
        public BattleResultRowViewModel UnitsLeft { get; set; } = new BattleResultRowViewModel();

        #region UnitsLost
        public BattleResultRowViewModel UnitLost { get; set; } = new BattleResultRowViewModel();
        public BattleResultRowViewModel UnitsLostWood { get; set; } = new BattleResultRowViewModel();
        public BattleResultRowViewModel UnitsLostClay { get; set; } = new BattleResultRowViewModel();
        public BattleResultRowViewModel UnitsLostIron { get; set; } = new BattleResultRowViewModel();
        #endregion



        public BattleResultRowViewModel WallResult { get; set; } = new BattleResultRowViewModel();



        #endregion Properties
    }
}