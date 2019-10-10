using System.Windows.Input;

namespace CalculationTools.Core
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Fields

        private readonly IDialogService dialogService;

        #endregion Fields

        #region Constructors

        public MainWindowViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
            ConnectToTW2Command = new RelayCommand(ConnectToTW2);
            OpenSettingsCommand = new RelayCommand(OpenSettings);
        }

        #endregion Constructors

        #region Properties

        public BattleSimulatorViewModel BattleSimulatorViewModel => IoC.GetBattleSimulatorViewModel();

        #region MenuViewModels

        public ConnectionWindowViewModel ConnectionWindowViewModel { get; set; } = new ConnectionWindowViewModel();
        public SettingsWindowViewModel SettingsWindowViewModel { get; set; } = new SettingsWindowViewModel();

        #endregion
        #region Commands

        public ICommand OpenSettingsCommand { get; set; }
        public ICommand ConnectToTW2Command { get; set; }

        #endregion Commands
        #endregion Properties

        #region Methods

        public void OpenSettings()
        {
            dialogService.ShowDialog(SettingsWindowViewModel);
        }
        public void ConnectToTW2()
        {

            dialogService.ShowDialog(ConnectionWindowViewModel);
            //  WebSocketConnect.StartConnectionAsync();
        }

        #endregion

    }
}
