using System.Windows;
using CalculationTools.Core;

namespace CalculationTools.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructors

        public MainWindow()
        {
            InitializeComponent();
            DataContext = IoC.GetMainWindowViewModel();
        }

        #endregion Constructors
    }
}