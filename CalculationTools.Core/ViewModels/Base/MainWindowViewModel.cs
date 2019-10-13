using CalculationTools.Common;
using System.Windows.Input;

namespace CalculationTools.Core
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Fields

        private readonly IDialogService _dialogService;
        private readonly ISettings _settings;
        private SettingsWindowViewModel _settingsWindowViewModel;

        #endregion Fields

        #region Constructors

        public MainWindowViewModel(IDialogService dialogService, IDataManager dataManager)
        {
            _dialogService = dialogService;
            _settings = dataManager.Settings;

            ConnectToTW2Command = new RelayCommand(ConnectToTW2);
            OpenSettingsCommand = new RelayCommand(OpenSettings);
        }

        #endregion Constructors

        #region Properties

        public BattleSimulatorViewModel BattleSimulatorViewModel => IoC.GetBattleSimulatorViewModel();

        #region MenuViewModels

        public ConnectionWindowViewModel ConnectionWindowViewModel { get; set; } = new ConnectionWindowViewModel();

        public SettingsWindowViewModel SettingsWindowViewModel
        {
            get { return _settingsWindowViewModel ??= new SettingsWindowViewModel(_settings); }
        }

        #endregion
        #region Commands

        public ICommand OpenSettingsCommand { get; set; }
        public ICommand ConnectToTW2Command { get; set; }

        #endregion Commands
        #endregion Properties

        #region Methods

        public void OpenSettings()
        {
            _dialogService.ShowDialog(SettingsWindowViewModel);
        }
        public void ConnectToTW2()
        {

            _dialogService.ShowDialog(ConnectionWindowViewModel);
            //  SocketManager.StartConnectionAsync();
        }

        #endregion

    }
}
