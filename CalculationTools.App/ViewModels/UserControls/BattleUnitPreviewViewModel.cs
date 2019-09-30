using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ClassLibrary.Class;
using ClassLibrary.ViewModels;

namespace TribalWars2_CalculationTools.ViewModels.UserControls
{
    public class BattleUnitPreviewViewModel : BaseViewModel
    {
        public ObservableCollection<UnitRow> UnitImageRows => GameData.UnitImageList;

        public BattleResultRowViewModel UnitAmount { get; set; } = new BattleResultRowViewModel();

        public BattleResultRowViewModel UnitLost { get; set; } = new BattleResultRowViewModel();

        public BattleResultRowViewModel UnitsLeft { get; set; } = new BattleResultRowViewModel();

    }
}
