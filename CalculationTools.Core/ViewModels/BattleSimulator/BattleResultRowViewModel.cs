using System.Collections.Generic;
using System.Collections.ObjectModel;
using CalculationTools.Core.Base;

namespace CalculationTools.Core.BattleSimulator
{
    public class BattleResultRowViewModel : BaseViewModel
    {
        #region Properties

        public List<BattleResultValueViewModel> BattleResultValues { get; set; } = BattleResultValueViewModel.GetEmptyList();
        public string Content { get; set; }
        public string Header { get; set; }

        public bool Show { get; set; }

        #endregion Properties
    }
}
