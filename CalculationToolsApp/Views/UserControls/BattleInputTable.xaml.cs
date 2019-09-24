using System;
using System.Windows;
using System.Windows.Controls;
using ClassLibrary.Class;
using ClassLibrary.Utilities;
using TribalWars2_CalculationTools.ViewModels;

namespace TribalWars2_CalculationTools.Views.UserControls
{
    /// <summary>
    /// Interaction logic for InputTable.xaml
    /// </summary>
    public partial class BattleInputTable : UserControl
    {

        public event EventHandler ValueChanged;

        public void UpdateValueChanged(object sender, EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }

        public BattleInputTable()
        {
            InitializeComponent();
            UnitsInputImageRows.ItemsSource = GameData.UnitImageList;
            InputComboChurchAtk.ItemsSource = GameData.FaithOptions;
            InputComboChurchDef.ItemsSource = GameData.FaithOptions;
        }

        private void InputChangedCheckBox(object sender, RoutedEventArgs e)
        {
            UpdateValueChanged(this, EventArgs.Empty);
        }

        private void InputChangedInteger(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpdateValueChanged(this, EventArgs.Empty);
        }

        private void InputComboChurchAtk_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e != null && e.AddedItems.Count > 0)
            {
                //BattleCalculatorInputViewModel.InputAtkChurch = e.AddedItems[0] as FaithLevel;
                UpdateValueChanged(this, EventArgs.Empty);
            }
        }

        private void InputComboChurchDef_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e != null && e.AddedItems.Count > 0)
            {
                //BattleCalculatorInputViewModel.InputDefChurch = e.AddedItems[0] as FaithLevel;
                UpdateValueChanged(this, EventArgs.Empty);
            }
        }
    }
}
