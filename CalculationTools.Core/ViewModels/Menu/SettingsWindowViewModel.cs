using CalculationTools.Common;
using CalculationTools.Common.Data;
using CalculationTools.Common.DataModels.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CalculationTools.Core
{
    public class SettingsWindowViewModel : BaseViewModel, IDialogViewModel
    {
        #region Fields


        private readonly IPlayerData _playerData;
        private readonly ISettings _settings;
        private readonly ISocketManager _socketManager;
        private string _unitImportText;

        #endregion Fields

        #region Constructors

        public SettingsWindowViewModel(IDataManager dataManager,
            IPlayerData playerData, ISocketManager socketManager)
        {
            _settings = dataManager.Settings;
            _playerData = playerData;
            _socketManager = socketManager;

            CloseCommand = new RelayCommand(() => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs()));
            CheckAccountCommand = new RelayCommand(CheckAccountCredentials);

            _playerData.LoginDataIsUpdated += (sender, args) =>
            {
                WorldList = _playerData.CharacterWorlds;
            };

        }
        #endregion Constructors
        #region Properties


        #region Account

        public List<Account> Accounts { get; set; }

        public bool CheckLoginButtonEnabled { get; set; } = true;
        public string CheckLoginButtonText { get; set; } = "Check account";
        public string CheckLoginMessage { get; set; }
        public string Password
        {
            get => SelectedAccount?.Password;
            set => SelectedAccount.Password = value;
        }

        public Account SelectedAccount { get; set; }

        public string ServerCountryCode
        {
            get => SelectedAccount?.ServerCountryCode;
            set => SelectedAccount.ServerCountryCode = value;
        }

        public List<Server> ServerList => GameData.ServerList;

        public string Username
        {
            get => SelectedAccount?.Username;
            set => SelectedAccount.Username = value;
        }

        public List<CharacterWorld> WorldList { get; set; }
        #endregion


        #region Commands

        public ICommand CheckAccountCommand { get; set; }

        /// <summary>
        /// Close the dialog window without changing anything
        /// </summary>
        public ICommand CloseCommand { get; set; }
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

        #region Events

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        #endregion Events

        #region Methods

        public void OnDialogOpen()
        {
            Accounts = _settings.GetAccounts();

            if (Accounts.Count > 0)
            {
                SelectedAccount = Accounts.Last();

                SelectedAccount.PropertyChanged += (sender, args) =>
                {
                    _settings.SetAccount(SelectedAccount);
                };
            }
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

            bool result = await _socketManager.LoginAsync(connectData);


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

        #endregion Methods
    }
}
