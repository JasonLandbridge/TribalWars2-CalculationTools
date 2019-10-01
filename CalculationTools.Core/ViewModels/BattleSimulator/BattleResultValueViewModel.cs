using System.Collections.Generic;
using System.Collections.ObjectModel;
using CalculationTools.Core.Base;

namespace CalculationTools.Core.BattleSimulator
{
    public class BattleResultValueViewModel : BaseViewModel
    {
        public int Value { get; set; }

        public BattleResultValueViewModel(int value)
        {
            Value = value;
        }

        /// <summary>
        /// Return en List with 0 values equal to the number of registered units.
        /// </summary>
        /// <returns></returns>
        public static List<BattleResultValueViewModel> GetEmptyList()
        {
            List<BattleResultValueViewModel> list = new List<BattleResultValueViewModel>();

            for (int i = 0; i < GameData.UnitList.Count; i++)
            {
                list.Add(new BattleResultValueViewModel(0));
            }

            return list;
        }

        /// <summary>
        /// Returns an ObservableCollection with 0 values equal to the number of registered units.
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<BattleResultValueViewModel> GetEmptyObservableCollection()
        {
            return new ObservableCollection<BattleResultValueViewModel>(GetEmptyList());
        }
    }
}
