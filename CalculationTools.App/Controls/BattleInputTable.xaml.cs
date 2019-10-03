using System;
using System.Windows;
using System.Windows.Controls;
using CalculationTools.Core;

namespace CalculationTools.App
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
            //TODO fix to static resource
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
                UpdateValueChanged(this, EventArgs.Empty);
            }
        }

        private void InputComboChurchDef_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e != null && e.AddedItems.Count > 0)
            {
                UpdateValueChanged(this, EventArgs.Empty);
            }
        }
    }
}
