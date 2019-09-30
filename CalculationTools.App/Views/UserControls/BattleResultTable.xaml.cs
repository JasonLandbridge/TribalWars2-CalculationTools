using System.Windows;
using System.Windows.Controls;
using ClassLibrary.Class;
using TribalWars2_CalculationTools.ViewModels;

namespace TribalWars2_CalculationTools.Views.UserControls
{

    public partial class BattleResultTable : UserControl
    {
        #region Constructors

        public BattleResultTable()
        {
            InitializeComponent();
            UnitImageRow.ItemsSource = GameData.UnitImageList;
        }

        #endregion Constructors

    }
}
