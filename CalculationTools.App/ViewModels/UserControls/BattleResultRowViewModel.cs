using System.Collections.ObjectModel;
using ClassLibrary.Class;
using ClassLibrary.ViewModels;

namespace TribalWars2_CalculationTools.ViewModels.UserControls
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
