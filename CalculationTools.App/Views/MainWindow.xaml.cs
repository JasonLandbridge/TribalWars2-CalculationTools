using System;
using CalculationTools.App.Views;
using CalculationTools.Core;

namespace CalculationTools.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        #region Constructors

        public MainWindow(MainWindowViewModel mainWindowViewModel, BattleSimulatorView battleSimulatorView, AttackPlannerView attackPlannerView)
        {
            InitializeComponent();
            DataContext = mainWindowViewModel;
            this.BattleSimulator.Content = battleSimulatorView;
            this.AttackPlanner.Content = attackPlannerView;
        }

        #endregion Constructors

        private void Window_Closed(object sender, EventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}