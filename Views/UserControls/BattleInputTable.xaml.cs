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

namespace TribalWars2_CalculationTools.Views.UserControls
{
    /// <summary>
    /// Interaction logic for InputTable.xaml
    /// </summary>
    public partial class BattleInputTable : UserControl
    {
        public InputCalculatorClass InputCalculator
        {
            get { return (InputCalculatorClass)GetValue(InputCalculatorProperty); }
            set { SetValue(InputCalculatorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InputCalculatorProperty =
            DependencyProperty.Register("InputCalculator",
                typeof(InputCalculatorClass),
                typeof(BattleInputTable),
                new PropertyMetadata(0, SetValues));

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BattleInputTable inputTable = d as BattleInputTable;
            InputCalculatorClass inputCalculator = e.NewValue as InputCalculatorClass;

            if (inputTable != null)
            {
                inputTable.UnitsInputRows.ItemsSource = inputCalculator.Units;
                inputTable.InputChurchHeader.Title = inputCalculator.InputChurchLabel;
                inputTable.InputChurchHeader.ImagePath = inputCalculator.InputChurchImagePath;
            }
        }


        public BattleInputTable()
        {
            InitializeComponent();
        }

        private void InputChangedCheckBox(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void InputChangedInteger(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
