using System.Windows.Input;

namespace CalculationTools.Core
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Fields

        private readonly IDialogService _dialogService;

        private readonly SettingsWindowViewModel _settingsWindowViewModel;
        private readonly ConnectionWindowViewModel _connectionWindowViewModel;

        #endregion Fields

        #region Constructors

        public MainWindowViewModel(
            SettingsWindowViewModel settingsWindowViewModel,
            ConnectionWindowViewModel connectionWindowViewModel,
            IDialogService dialogService)
        {
            _dialogService = dialogService;
            _settingsWindowViewModel = settingsWindowViewModel;
            _connectionWindowViewModel = connectionWindowViewModel;


            ConnectToTW2Command = new RelayCommand(ConnectToTW2);
            OpenSettingsCommand = new RelayCommand(OpenSettings);
        }

        #endregion Constructors

        #region Properties

        #region Commands

        public ICommand OpenSettingsCommand { get; set; }
        public ICommand ConnectToTW2Command { get; set; }

        #endregion Commands
        #endregion Properties

        #region Methods

        public void OpenSettings()
        {
            _dialogService.ShowDialog(_settingsWindowViewModel);
        }
        public void ConnectToTW2()
        {
            _dialogService.ShowDialog(_connectionWindowViewModel);
        }

        #endregion

    }
}
