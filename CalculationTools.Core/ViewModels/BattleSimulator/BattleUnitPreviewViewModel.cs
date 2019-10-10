using System.Collections.ObjectModel;

namespace CalculationTools.Core.BattleSimulator
{
    public class BattleUnitPreviewViewModel : BaseViewModel
    {
        public ObservableCollection<UnitRow> UnitImageRows => GameData.UnitImageList;

        public BattleResultRowViewModel UnitAmount { get; set; } = new BattleResultRowViewModel();

        public BattleResultRowViewModel UnitLost { get; set; } = new BattleResultRowViewModel();

        public BattleResultRowViewModel UnitsLeft { get; set; } = new BattleResultRowViewModel();

    }
}
