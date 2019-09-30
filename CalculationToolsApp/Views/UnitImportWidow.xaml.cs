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
using System.Windows.Shapes;

namespace TribalWars2_CalculationTools.Views
{
    /// <summary>
    /// Interaction logic for UnitImportWidow.xaml
    /// </summary>
    public partial class UnitImportWidow : Window
    {
        public UnitImportWidow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
