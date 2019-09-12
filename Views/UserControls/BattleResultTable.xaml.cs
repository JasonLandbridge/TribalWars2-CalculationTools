using System;
using System.Collections.Generic;
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
using TribalWars2_CalculationTools.ViewModels;

namespace TribalWars2_CalculationTools.Views.UserControls
{

    public partial class BattleResultTable : UserControl
    {

        #region Fields

        public static readonly DependencyProperty BattleModifierProperty =
            DependencyProperty.Register("BattleModifier",
                typeof(int),
                typeof(BattleResultTable),
                new PropertyMetadata(0));

        public static readonly DependencyProperty BattleResultTableViewModelProperty =
            DependencyProperty.Register("BattleResultTableViewModel",
                typeof(BattleResultTableViewModel),
                typeof(BattleResultTable),
                new PropertyMetadata(SetProperty));

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header",
                typeof(string),
                typeof(BattleResultTable),
                new PropertyMetadata("No Title"));

        #endregion Fields

        #region Constructors

        public BattleResultTable()
        {
            InitializeComponent();
            UnitImageRow.ItemsSource = GameData.UnitImageList;
        }

        #endregion Constructors

        #region Properties

        public int BattleModifier
        {
            get => (int)GetValue(BattleModifierProperty);
            set => SetValue(BattleModifierProperty, value);
        }

        public BattleResultTableViewModel BattleResultTableViewModel
        {
            get => (BattleResultTableViewModel)GetValue(BattleResultTableViewModelProperty);
            set => SetValue(BattleResultTableViewModelProperty, value);
        }

        public string Header
        {
            get => (string)GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        #endregion Properties

        #region Methods

        private static void SetProperty(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BattleResultTable resultTable && e.NewValue is BattleResultTableViewModel battleResultTableViewModel)
            {
                resultTable.UnitsAmount.BattleResultValues = battleResultTableViewModel.UnitAmount;
                resultTable.UnitsLost.BattleResultValues = battleResultTableViewModel.UnitLost;
            }

        }

        #endregion Methods
    }
}
