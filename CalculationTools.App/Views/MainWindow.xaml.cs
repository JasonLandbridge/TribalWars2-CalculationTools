using System.Windows;

namespace CalculationTools.App
{
    public partial class MainWindow : Window
    {
        #region Constructors

        public MainWindowViewModel MainWindowViewModel { get; set; } = new MainWindowViewModel();
        public MainWindow()
        {
            this.DataContext = MainWindowViewModel;

            InitializeComponent();
        }

        #endregion Constructors

    }
}