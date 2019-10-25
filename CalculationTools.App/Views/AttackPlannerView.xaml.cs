using CalculationTools.Core;
using System.Windows.Controls;

namespace CalculationTools.App.Views
{
    /// <summary>
    /// Interaction logic for AttackPlannerView.xaml
    /// </summary>
    public partial class AttackPlannerView : UserControl
    {
        public AttackPlannerView(AttackPlannerViewModel attackPlannerViewModel)
        {
            InitializeComponent();
            DataContext = attackPlannerViewModel;
        }
    }
}
