﻿using CalculationTools.Common;
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
            DisconnectCommand = new RelayCommand(StopConnection);

            ClearCommand = new RelayCommand(ClearConnectionLog);
            CloseCommand = new RelayCommand(() => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs()));
        }

        private void ClearConnectionLog()
        {
            _socketManager.ClearConnectionLog();
            ConnectionLog = string.Empty;
        }

        private async void StartConnection()
        {
            if (SelectedAccount != null)
            {
                IsConnecting = true;
                _socketManager.ConnectionLogUpdated += UpdateConnectionLog;
                await _socketManager.StartConnection(SelectedAccount.ToConnectData());
            }
        }

        private void UpdateConnectionLog(object sender, EventArgs e)
        {
            ConnectionLog = _socketManager.ConnectionLog?.ToString();
        }

        private async void StopConnection()
        {
            if (SelectedAccount != null)
            {
                IsConnecting = false;

                await _socketManager.StopConnection();
                _socketManager.ConnectionLogUpdated -= UpdateConnectionLog;
            }
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

        public bool IsAccountSelectionEnabled { get; set; } = true;
        public List<Account> Accounts { get; set; } = new List<Account>();
        public Account SelectedAccount { get; set; } = new Account();

        public string ConnectionLog { get; set; }

        #region Commands
        public ICommand ConnectCommand { get; }
        public ICommand DisconnectCommand { get; }

        /// <summary>
        /// Clear the connection log
        /// </summary>
        public ICommand ClearCommand { get; }

        /// <summary>
        /// Close the dialog window without changing anything
        /// </summary>
        public ICommand CloseCommand { get; }

        #endregion

        #region ErrorHandeling

        public string ErrorMessage { get; set; }
        public bool IsError { get; set; }
        public Visibility IsErrorVisible => IsError ? Visibility.Visible : Visibility.Hidden;

        #endregion

        #endregion Properties

        #region Methods

        #endregion Methods

    }
}