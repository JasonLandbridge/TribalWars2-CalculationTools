using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using CalculationTools.Common;

namespace CalculationTools.App.Controls
{
    /// <summary>
    /// Interaction logic for CitySelector.xaml
    /// </summary>
    public partial class CitySelector : UserControl
    {

        public CitySelector()
        {
            InitializeComponent();
            //SuggestBox.DataContext = this;
        }



        public List<string> SearchResults { get; set; } = new List<string>
        {
            "Test1",
            "Test2",
            "Test3",
            "Test4",
            "Test5",
            "Test6",
        };

        public int CurrentId { get; set; }
        public Village CurrentVillage { get; set; }


    }
}
