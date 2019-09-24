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

        public InputCalculatorData InputCalculatorData
        {
            get => (InputCalculatorData)GetValue(InputCalculatorDataProperty);
            set => SetValue(InputCalculatorDataProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InputCalculatorDataProperty =
            DependencyProperty.Register("InputCalculatorData",
                typeof(InputCalculatorData),
                typeof(BattleInputTable),
                new PropertyMetadata(new InputCalculatorData(), SetValues));

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BattleInputTable inputTable = d as BattleInputTable;
            InputCalculatorData inputCalculator = e.NewValue as InputCalculatorData;

            if (inputTable != null && inputCalculator != null)
            {
                inputTable.UnitsInputRows.ItemsSource = inputCalculator.Units;

                inputTable.InputGrandMasterHeader.Title = inputCalculator.InputGrandmasterBonusLabel;
                inputTable.InputGrandMasterHeader.ImagePath = inputCalculator.InputGrandmasterBonusImagePath;

                inputTable.InputChurchHeader.Title = inputCalculator.InputChurchLabel;
                inputTable.InputChurchHeader.ImagePath = inputCalculator.InputChurchImagePath;

                inputTable.InputMoraleHeader.Title = inputCalculator.InputMoraleLabel;
                inputTable.InputMoraleHeader.ImagePath = inputCalculator.InputMoraleImagePath;

                inputTable.InputLuckHeader.Title = inputCalculator.InputLuckLabel;
                inputTable.InputLuckHeader.ImagePath = inputCalculator.InputLuckImagePath;

                inputTable.InputWallHeader.Title = inputCalculator.InputWallLabel;
                inputTable.InputWallHeader.ImagePath = inputCalculator.InputWallImagePath;

                inputTable.InputNightBonusHeader.Title = inputCalculator.InputNightBonusLabel;
                inputTable.InputNightBonusHeader.ImagePath = inputCalculator.InputNightBonusImagePath;

            }
        }

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

        private void InputComboChurchDef_DragLeave(object sender, DragEventArgs e)
        {
            UpdateValueChanged(this, EventArgs.Empty);
        }

        private void InputComboChurchAtk_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdateValueChanged(this, EventArgs.Empty);
        }

        private void InputMoralChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            InputCalculatorData.InputMorale = Convert.ToInt32(e.NewValue);
            UpdateValueChanged(this, EventArgs.Empty);

        }

        private void InputComboChurchAtk_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e != null && e.AddedItems.Count > 0)
            {
                InputCalculatorData.InputAtkChurch = e.AddedItems[0] as FaithLevel;
                UpdateValueChanged(this, EventArgs.Empty);
            }
        }

        private void InputComboChurchDef_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e != null && e.AddedItems.Count > 0)
            {
                InputCalculatorData.InputDefChurch = e.AddedItems[0] as FaithLevel;
                UpdateValueChanged(this, EventArgs.Empty);
            }
        }
    }
}
