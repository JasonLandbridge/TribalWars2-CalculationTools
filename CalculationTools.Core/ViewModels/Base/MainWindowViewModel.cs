using System.Windows.Input;

namespace CalculationTools.Core
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Fields

        private readonly IDialogService _dialogService;

        private readonly SettingsWindowViewModel _settingsWindowViewModel;
        private readonly BattleSimulatorViewModel _battleSimulatorViewModel;

        #endregion Fields

        #region Constructors

        public MainWindowViewModel(
            SettingsWindowViewModel settingsWindowViewModel,
            BattleSimulatorViewModel battleSimulatorViewModel,
            IDialogService dialogService)
        {
            _dialogService = dialogService;
            _settingsWindowViewModel = settingsWindowViewModel;
            _battleSimulatorViewModel = battleSimulatorViewModel;

            ConnectToTW2Command = new RelayCommand(ConnectToTW2);
            OpenSettingsCommand = new RelayCommand(OpenSettings);
        }

        #endregion Constructors

        #region Properties

        #region MenuViewModels

        public ConnectionWindowViewModel ConnectionWindowViewModel { get; set; } = new ConnectionWindowViewModel();

        #endregion
        #region Commands

        public ICommand OpenSettingsCommand { get; set; }
        public ICommand ConnectToTW2Command { get; set; }

        #endregion Commands
        #endregion Properties

        #region Methods

        public void OpenSettings()
        {
            _settingsWindowViewModel.OnDialogOpen();
            _dialogService.ShowDialog(_settingsWindowViewModel);
        }
        public void ConnectToTW2()
        {

            _dialogService.ShowDialog(ConnectionWindowViewModel);
            //  SocketManager.StartConnectionAsync();
        }

        #endregion

    }
}
