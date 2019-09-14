using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TribalWars2_CalculationTools.Class;

namespace TribalWars2_CalculationTools.Views.UserControls
{
    /// <summary>
    /// Interaction logic for BattleResultRow_UC.xaml
    /// </summary>
    public partial class BattleResultRow : UserControl
    {
        #region Fields

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(BattleResultRow), new PropertyMetadata("No Title"));

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BattleResultValuesProperty =
            DependencyProperty.Register("BattleResultValues", typeof(ObservableCollection<BattleResultValue>), typeof(BattleResultRow), new PropertyMetadata(SetSource));

        #endregion Fields

        #region Constructors

        public BattleResultRow()
        {
            InitializeComponent();

        }

        #endregion Constructors




        #region Properties

        public string Header
        {
            get => (string)GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public ObservableCollection<BattleResultValue> BattleResultValues
        {
            get => (ObservableCollection<BattleResultValue>)GetValue(BattleResultValuesProperty);
            set => SetValue(BattleResultValuesProperty, value);
        }



        #endregion Properties

        #region Methods

        private static void SetSource(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BattleResultRow battleResult = d as BattleResultRow;
            ObservableCollection<BattleResultValue> battleResultValues = e.NewValue as ObservableCollection<BattleResultValue>;

            if (battleResult != null && battleResultValues != null)
            {
                battleResult.BattleResultValues = battleResultValues;
            }

        }

        #endregion Methods
    }
}
