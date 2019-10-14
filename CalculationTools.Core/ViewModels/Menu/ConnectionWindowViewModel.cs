using CalculationTools.Common;
using CalculationTools.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CalculationTools.Core
{
    public class ConnectionWindowViewModel : BaseViewModel, IDialogViewModel
    {
        private readonly IDataManager _dataManager;
        private readonly ISocketManager _socketManager;
        private readonly ISettings _settings;

        #region Constructors

        public ConnectionWindowViewModel(
            IDataManager dataManager,
            ISocketManager socketManager)
        {
            _dataManager = dataManager;
            _socketManager = socketManager;
            _settings = dataManager.Settings;

            ConnectCommand = new RelayCommand(StartConnection);
        }

        private async void StartConnection()
        {
            if (SelectedAccount != null)
            {
                IsConnecting = true;
                await _socketManager.StartConnection(SelectedAccount.ToConnectData());
            }
        }

        private async void StopConnection()
        {
            await _socketManager.StopConnection();
        }

        #endregion Constructors

        #region Events

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
        public void OnDialogOpen()
        {
            Accounts = _settings.GetAccounts(true);

            if (Accounts.Count > 0)
            {
                SelectedAccount = Accounts.Last();

                SelectedAccount.PropertyChanged += (sender, args) =>
                {
                    _settings.SetAccount(SelectedAccount);
                };
            }
        }

        #endregion Events

        #region Properties

        public bool IsConnecting
        {
            set => IsAccountSelectionEnabled = !value;
        }

        public bool IsAccountSelectionEnabled { get; set; }
        public List<Account> Accounts { get; set; }
        public Account SelectedAccount { get; set; }

        #region Commands

        /// <summary>
        /// Close the dialog window without changing anything
        /// </summary>
        public ICommand CloseCommand { get; }
        public ICommand ConnectCommand { get; }
        public ICommand DisconnectCommand { get; }

        /// <summary>
        /// Reset the input fields
        /// </summary>
        public ICommand ResetCommand { get; }

        /// <summary>
        /// Confirm and send the UnitSet to the BattleSimulator
        /// </summary>
        public ICommand SaveCommand { get; }

        #endregion

        #region ErrorHandeling

        public string ErrorMessage { get; set; }
        public bool IsError { get; set; }
        public Visibility IsErrorVisible => IsError ? Visibility.Visible : Visibility.Hidden;

        #endregion

        #endregion Properties

        #region Methods

        #endregion Methods

        #region Fields

        private string _unitImportText;

        #endregion Fields
    }
}
