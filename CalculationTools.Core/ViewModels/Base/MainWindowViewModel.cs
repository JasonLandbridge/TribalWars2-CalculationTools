using CalculationTools.Common;
using System;
using System.Reactive.Linq;
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

            SetupReactions();
            SetConnectionStatus(false);
        }

        #endregion Constructors

        #region Properties

        public string ConnectionStatusImage { get; set; }
        public string ConnectionStatusMessage { get; set; }


        #region Commands

        public ICommand OpenSettingsCommand { get; set; }
        public ICommand ConnectToTW2Command { get; set; }

        #endregion Commands
        #endregion Properties

        #region Methods

        public void SetConnectionStatus(bool status)
        {
            string statusString;
            if (status)
            {
                statusString = "online";
            }
            else
            {
                statusString = "offline";
            }

            ConnectionStatusImage = $"/Resources/Img/connection_status/{statusString}.png";
            ConnectionStatusMessage = statusString.ToUpper();
        }
        private void SetupReactions()
        {
            //Change the status bar online/offline icon when the connection changes
            Observable
                .FromEventPattern<bool>(
                          ev => DataEvents.ConnectionStatus += ev,
                          ev => DataEvents.ConnectionStatus -= ev)
                .Subscribe(x => SetConnectionStatus(x.EventArgs));
        }

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
