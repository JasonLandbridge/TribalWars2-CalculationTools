using System.Collections.Generic;

namespace CalculationTools.Core
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
