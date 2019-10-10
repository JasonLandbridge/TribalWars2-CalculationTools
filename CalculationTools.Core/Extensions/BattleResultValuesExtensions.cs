using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace CalculationTools.Core.Extensions
{
    public static class BattleResultValuesExtensions
    {
        /// <summary>
        /// Gets the sum of all values and adds this at the end of the list.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<BattleResultValueViewModel> AddTotal(this List<BattleResultValueViewModel> source)
        {
            source.Add(new BattleResultValueViewModel
            {
                Value = source.Sum(value => value.Value),
                IsBold = true
            });
            return source;
        }


        /// <summary>
        /// Set the color for all values.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="valueColor"></param>
        /// <returns></returns>
        public static List<BattleResultValueViewModel> SetColor(this List<BattleResultValueViewModel> source, Color valueColor)
        {
            source.ForEach(x => x.ValueColor = valueColor);
            return source;
        }

        /// <summary>
        /// Should the numbers be displayed abbreviated.
        /// <para>E.g. 1K, 100M, 10B etc</para>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="abbreviateValue"></param>
        /// <returns></returns>
        public static List<BattleResultValueViewModel> SetAbbreviation(this List<BattleResultValueViewModel> source, bool abbreviateValue)
        {
            source.ForEach(x => x.IsAbbreviated = abbreviateValue);
            return source;
        }
    }
}
