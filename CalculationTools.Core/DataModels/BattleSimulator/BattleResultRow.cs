using System;
using System.Collections.Generic;
using System.Linq;

namespace CalculationTools.Core
{


    public class BattleResultRow
    {
        public List<BattleResultValueViewModel> Values { get; set; } = new List<BattleResultValueViewModel>();

        public List<BattleResultValueViewModel> ValuesWithTotal
        {
            get
            {
                List<BattleResultValueViewModel> result = Values;
                result.Add(new BattleResultValueViewModel(Total));
                result.Last().IsBold = true;
                return result;
            }
        }

        public int Total => Values.Sum(x => x.Value);


        public BattleResultRow()
        {

        }

        public BattleResultRow(List<BattleResultValueViewModel> values)
        {
            Values = values;
        }

        public void Add(int value)
        {
            Values.Add(new BattleResultValueViewModel(value));
        }

        public static BattleResultRow operator +(BattleResultRow a, BattleResultRow b)
        {
            List<BattleResultValueViewModel> result = new List<BattleResultValueViewModel>();

            for (int i = 0; i < Math.Max(a.Values.Count, b.Values.Count); i++)
            {
                int value = 0;

                if (i < a.Values.Count)
                {
                    value += a.Values[i].Value;
                }

                if (i < b.Values.Count)
                {
                    value += b.Values[i].Value;
                }

                result.Add(new BattleResultValueViewModel(value));
            }

            return new BattleResultRow(result);
        }

        public static BattleResultRow Empty
        {
            get
            {
                List<BattleResultValueViewModel> list = new List<BattleResultValueViewModel>();
                for (int i = 0; i < 13; i++)
                {
                    list.Add(new BattleResultValueViewModel(0));
                }
                return new BattleResultRow(list);
            }
        }
    }
}
