﻿using CalculationTools.Core;
using CalculationTools.Core.Base;

namespace CalculationTools.App
{
    public class BattleResultTableViewModel : BaseViewModel
    {
        #region Properties

        public string Header { get; set; }
        public bool ShowWallResult { get; set; }
        public string BattleModifier { get; set; }
        public BattleResultRowViewModel UnitAmount { get; set; } = new BattleResultRowViewModel();

        public BattleResultRowViewModel UnitLost { get; set; } = new BattleResultRowViewModel();

        public BattleResultRowViewModel UnitsLeft { get; set; } = new BattleResultRowViewModel();

        public BattleResultRowViewModel WallResult { get; set; } = new BattleResultRowViewModel();

        #endregion Properties
    }
}