using CalculationTools.Core;

namespace CalculationTools.App
{
    /// <summary>
    /// Interaction logic for UnitImportWidow.xaml
    /// </summary>
    public partial class SettingsWindow : IDialog
    {
        public SettingsWindow(SettingsWindowViewModel settingsWindowViewModel)
        {
            InitializeComponent();
            DataContext = settingsWindowViewModel;
        }

    }
}
