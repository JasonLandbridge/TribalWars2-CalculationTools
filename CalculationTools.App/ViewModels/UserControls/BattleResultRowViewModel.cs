using System.Collections.ObjectModel;
using CalculationTools.Core;

namespace CalculationTools.App
{
    public class BattleResultRowViewModel : BaseViewModel
    {
        #region Properties

        public ObservableCollection<BattleResultValue> BattleResultValues { get; set; } =
            BattleResultValue.GetEmptyObservableCollection();
        public string Content { get; set; }
        public string Header { get; set; }
        public bool Show { get; set; }

        #endregion Properties
    }
}
