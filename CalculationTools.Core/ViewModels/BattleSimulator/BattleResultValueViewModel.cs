using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Media;

namespace CalculationTools.Core
{
    public class BattleResultValueViewModel : BaseViewModel
    {
        #region Constructors

        public BattleResultValueViewModel()
        {

        }

        public BattleResultValueViewModel(int value)
        {
            Value = value;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Sets if this value should be displayed in shorthand
        /// <para>Displays 1,234,567,890</para>
        /// <para>Displays 1,234,568K</para>
        /// <para>Displays 1,235M</para>
        /// <para>Displays 1B</para>
        /// </summary>
        public bool IsAbbreviated { get; set; }

        /// <summary>
        /// The base value 
        /// </summary>
        public int Value { get; set; }

        public string ValueFormat { get; } = "s{0}";

        public bool IsBold { get; set; }

        public string IsBoldString => IsBold ? "Bold" : "Normal";

        public string ValueFormatted
        {
            get
            {
                if (IsAbbreviated)
                {
                    switch (Value)
                    {
                        //100.000.000 - 100.000.000.000
                        case int n when (n >= 100000000):
                            return Value.ToString("###,##0,,M", CultureInfo.InvariantCulture);

                        //100.000 - 999.999
                        case int n when (n >= 10000):
                            return Value.ToString("#,##0,K", CultureInfo.InvariantCulture);

                        //10.000 - 99.999
                        case int n when (n >= 1000):
                            return Value.ToString("#,#", CultureInfo.InvariantCulture);
                    }
                }
                return Value.ToString();
            }
        }

        public Color ValueColor { get; set; } = Colors.Black;

        public SolidColorBrush ValueColorBrush => new SolidColorBrush(ValueColor);

        #endregion Properties

        #region Methods

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

        #endregion Methods
    }
}
