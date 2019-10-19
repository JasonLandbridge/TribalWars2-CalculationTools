using System;
using CalculationTools.Core;

namespace CalculationTools.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        #region Constructors

        public MainWindow(MainWindowViewModel mainWindowViewModel, BattleSimulatorView battleSimulatorView)
        {
            InitializeComponent();
            DataContext = mainWindowViewModel;
            BattleSimulator.Content = battleSimulatorView;
        }

        #endregion Constructors

        private void Window_Closed(object sender, EventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}