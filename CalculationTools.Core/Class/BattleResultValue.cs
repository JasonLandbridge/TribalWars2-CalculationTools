using System.Collections.Generic;
using System.Collections.ObjectModel;
using CalculationTools.Core.Base;

namespace CalculationTools.Core
{
    public class BattleResultValue : BaseViewModel
    {
        public int Value { get; set; }

        public BattleResultValue(int value)
        {
            Value = value;
        }

        /// <summary>
        /// Return en List with 0 values equal to the number of registered units.
        /// </summary>
        /// <returns></returns>
        public static List<BattleResultValue> GetEmptyList()
        {
            List<BattleResultValue> list = new List<BattleResultValue>();

            for (int i = 0; i < GameData.UnitList.Count; i++)
            {
                list.Add(new BattleResultValue(0));
            }

            return list;
        }

        /// <summary>
        /// Return en ObservableCollection with 0 values equal to the number of registered units.
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<BattleResultValue> GetEmptyObservableCollection()
        {
            return new ObservableCollection<BattleResultValue>(GetEmptyList());
        }
    }
}
