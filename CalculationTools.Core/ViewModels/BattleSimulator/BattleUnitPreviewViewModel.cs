using System.Collections.ObjectModel;

namespace CalculationTools.Core
{
    public class BattleUnitPreviewViewModel : BaseViewModel
    {
        public string Header { get; set; }

        public ObservableCollection<UnitRow> UnitImageRows => GameData.UnitImageList;

        public BattleResultRowViewModel UnitAmount { get; set; } = new BattleResultRowViewModel();

        public BattleResultRowViewModel UnitLost { get; set; } = new BattleResultRowViewModel();

        public BattleResultRowViewModel UnitsLeft { get; set; } = new BattleResultRowViewModel();

        public bool UnitAmountVisibility { get; set; } = true;
        public bool UnitLostVisibility { get; set; } = true;
        public bool UnitsLeftVisibility { get; set; } = true;

        public BattleUnitPreviewViewModel()
        {
            UnitAmount.Header = "In village";
            UnitLost.Header = "Traveling";
            UnitsLeft.Header = "Total";
        }

        public void Reset()
        {
            UnitAmount.BattleResultValues = BattleResultRow.Empty.Values;
            UnitLost.BattleResultValues = BattleResultRow.Empty.Values;
            UnitsLeft.BattleResultValues = BattleResultRow.Empty.Values;
        }
    }
}
