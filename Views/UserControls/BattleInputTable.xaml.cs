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
using TribalWars2_CalculationTools.ViewModels;
using Syncfusion.Windows.Tools.Controls;
using TribalWars2_CalculationTools.Class;

namespace TribalWars2_CalculationTools.Views.UserControls
{
    /// <summary>
    /// Interaction logic for InputTable.xaml
    /// </summary>
    public partial class BattleInputTable : UserControl
    {

        public int ComponentWidth { get; } = 120;


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

        public BattleInputTable()
        {
            InitializeComponent();
            InputComboChurchAtk.ItemsSource = GameData.FaithOptions;
            InputComboChurchDef.ItemsSource = GameData.FaithOptions;
        }

        private void InputChangedCheckBox(object sender, RoutedEventArgs e)
        {
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }

        private void InputChangedInteger(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
