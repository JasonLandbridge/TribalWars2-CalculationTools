using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TribalWars2_CalculationTools.Class;

namespace TribalWars2_CalculationTools.ViewModels
{
    public class BattleResultTableViewModel
    {

        public ObservableCollection<BattleResultValue> UnitAmount { get; set; } = new ObservableCollection<BattleResultValue>();

        public ObservableCollection<BattleResultValue> UnitLost { get; set; } = new ObservableCollection<BattleResultValue>();


    }
}
