using System.Collections.ObjectModel;
using CalculationTools.Core.Base;

namespace CalculationTools.Core.BattleSimulator
{
    public class BattleResultRowViewModel : BaseViewModel
    {
        #region Properties

        public ObservableCollection<BattleResultValueViewModel> BattleResultValues { get; set; } = BattleResultValueViewModel.GetEmptyObservableCollection();
        public string Content { get; set; }
        public string Header { get; set; }
        public bool Show { get; set; }

        #endregion Properties
    }
}
