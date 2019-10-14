using CalculationTools.Core;

namespace CalculationTools.App
{
    /// <summary>
    /// Interaction logic for UnitImportWidow.xaml
    /// </summary>
    public partial class ConnectionWindow : IDialog
    {
        public ConnectionWindow(ConnectionWindowViewModel connectionWindowViewModel)
        {
            InitializeComponent();
            DataContext = connectionWindowViewModel;
        }

    }
}
