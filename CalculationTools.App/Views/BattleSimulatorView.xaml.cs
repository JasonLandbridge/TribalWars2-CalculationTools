using CalculationTools.Core;
using System.Windows.Controls;

namespace CalculationTools.App
{
    /// <summary>
    /// Interaction logic for BattleSimulatorView.xaml
    /// </summary>
    public partial class BattleSimulatorView : UserControl
    {
        public BattleSimulatorView(BattleSimulatorViewModel battleSimulatorViewModel, BattleInput battleInput)
        {
            InitializeComponent();
            DataContext = battleSimulatorViewModel;
            this.BattleInput.Content = battleInput;

        }
    }
}
