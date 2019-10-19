using CalculationTools.Core;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CalculationTools.App
{
    /// <summary>
    /// Interaction logic for InputTable.xaml
    /// </summary>
    public partial class BattleInput
    {

        #region Constructors

        public BattleInput(BattleInputViewModel battleInputViewModel)
        {
            InitializeComponent();
            DataContext = battleInputViewModel;

            //TODO fix to static resource
            UnitsInputImageRows.ItemsSource = GameData.UnitImageList;
            InputComboChurchAtk.ItemsSource = GameData.FaithOptions;
            InputComboChurchDef.ItemsSource = GameData.FaithOptions;
        }

        #endregion Constructors

        #region Events

        public event EventHandler ValueChanged;

        #endregion Events

        #region Methods

        public void UpdateValueChanged(object sender, EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }
        private void InputChangedCheckBox(object sender, RoutedEventArgs e)
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

        #endregion Methods
    }
}
