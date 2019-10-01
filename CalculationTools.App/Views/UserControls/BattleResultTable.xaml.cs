using System.Windows.Controls;
using CalculationTools.Core;

namespace CalculationTools.App
{

    public partial class BattleResultTable : UserControl
    {
        #region Constructors

        public BattleResultTable()
        {
            InitializeComponent();
            UnitImageRow.ItemsSource = GameData.UnitImageList; //TODO fix this to a binding
        }

        #endregion Constructors

    }
}
