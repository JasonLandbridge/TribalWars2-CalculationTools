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

        public string Header
        {
            get => (string)GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header",
                typeof(string),
                typeof(BattleResultTable),
                new PropertyMetadata("No Title"));


        public BattleResultTableViewModel BattleResultTableViewModel
        {
            get => (BattleResultTableViewModel)GetValue(BattleResultTableViewModelProperty);
            set => SetValue(BattleResultTableViewModelProperty, value);
        }

        public static readonly DependencyProperty BattleResultTableViewModelProperty =
            DependencyProperty.Register("BattleResultTableViewModel",
                typeof(BattleResultTableViewModel),
                typeof(BattleResultTable),
                new PropertyMetadata(new BattleResultTableViewModel(), SetProperty));

        private static void SetProperty(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BattleResultTable resultTable = d as BattleResultTable;
            BattleResultTableViewModel battleResultTableViewModel = e.NewValue as BattleResultTableViewModel;

            if (resultTable != null && battleResultTableViewModel != null)
            {
                resultTable.UnitsAmount.BattleResultValues = battleResultTableViewModel.UnitAmount;
                resultTable.UnitsLost.BattleResultValues = battleResultTableViewModel.UnitLost;
            }

        }


        public BattleResultTable()
        {
            InitializeComponent();
            UnitImageRow.ItemsSource = GameData.UnitImageList;
        }
    }
}
