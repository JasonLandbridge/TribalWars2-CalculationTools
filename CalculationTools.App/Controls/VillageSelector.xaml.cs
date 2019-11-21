using CalculationTools.Common;
using System.Collections.Generic;
using System.Windows.Controls;

namespace CalculationTools.App.Controls
{
    /// <summary>
    /// Interaction logic for VillageSelector.xaml
    /// </summary>
    public partial class VillageSelector : UserControl
    {

        public VillageSelector()
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
