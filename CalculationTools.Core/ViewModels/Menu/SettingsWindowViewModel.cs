using CalculationTools.Common;
using CalculationTools.Common.DataModels.World;
using CalculationTools.WebSocket;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace CalculationTools.Core
{
    public class SettingsWindowViewModel : BaseViewModel, IDialogRequestClose
    {
        private readonly ISettings _settings;

        #region Constructors

        public SettingsWindowViewModel(ISettings settings)
        {
            _settings = settings;

            CloseCommand = new RelayCommand(() => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs()));
            CheckAccountCommand = new RelayCommand(CheckAccountCredentials);
            SelectedAccount = Accounts[0];
        }

        private async void CheckAccountCredentials()
        {
            CheckLoginButtonText = "Checking credentials";
            CheckLoginButtonEnabled = false;

            ConnectData connectData = new ConnectData
            {
                Username = SelectedAccount.Username,
                Password = SelectedAccount.Password,
                ServerCountryCode = SelectedAccount.ServerCountryCode
            };

            bool result = await SocketManager.LoginAsync(connectData);

            if (result)
            {
                CheckLoginMessage = "The credentials were successful!";
            }
            else
            {
                CheckLoginMessage = "The credentials failed! Please try again.";
            }

            CheckLoginButtonText = "Check account";
            CheckLoginButtonEnabled = true;

        }

        #endregion Constructors

        #region Events

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        #endregion Events

        #region Properties


        #region Account

        public List<Account> Accounts
        {
            get => _settings.Accounts;
            set => _settings.Accounts = value;
        }

        public Account SelectedAccount { get; set; }

        public List<Server> ServerList => GameData.ServerList;

        public string CheckLoginButtonText { get; set; } = "Check account";

        public bool CheckLoginButtonEnabled { get; set; } = true;

        public string CheckLoginMessage { get; set; }

        public string Username
        {
            get => SelectedAccount.Username;
            set
            {
                SelectedAccount.Username = value;
                Accounts[0] = SelectedAccount;
            }
        }
        public string Password
        {
            get => SelectedAccount.Password;
            set
            {
                SelectedAccount.Password = value;
                Accounts[0] = SelectedAccount;
            }
        }
        public string ServerCountryCode
        {
            get => SelectedAccount.ServerCountryCode;
            set
            {
                SelectedAccount.ServerCountryCode = value;
                Accounts[0] = SelectedAccount;
            }
        }

        #endregion


        #region Commands

        /// <summary>
        /// Close the dialog window without changing anything
        /// </summary>
        public ICommand CloseCommand { get; set; }
        public ICommand CheckAccountCommand { get; set; }

        #endregion

        #region ErrorHandeling

        public string ErrorMessage { get; set; }
        public bool IsError { get; set; }
        public Visibility IsErrorVisible => IsError ? Visibility.Visible : Visibility.Hidden;

        #endregion
        public string UnitImportText
        {
            get => _unitImportText;
            set
            {
                _unitImportText = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties

        #region Fields

        private string _unitImportText;

        #endregion Fields
    }
}
