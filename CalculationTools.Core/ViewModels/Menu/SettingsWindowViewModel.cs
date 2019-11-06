using CalculationTools.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            AddAccountCommand = new RelayCommand(AddNewAccount);
            DeleteAccountCommand = new RelayCommand(DeleteAccount);

            _playerData.LoginDataIsUpdated += (sender, args) =>
            {
                WorldList = _playerData.CharacterWorlds;
                if (WorldList != null && WorldList.Count > 0)
                {
                    SelectedWorld = WorldList[0].WorldId;
                }
            };

        }

        private void DeleteAccount()
        {
            _settings.DeleteAccount(SelectedAccount);
            SyncAccounts();
        }

        private void AddNewAccount()
        {
            _settings.AddAccount();

            SyncAccounts();
        }

        #endregion Constructors
        #region Events

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        #endregion Events

        #region Properties


        #region Account

        public ObservableCollection<Account> Accounts { get; set; } = new ObservableCollection<Account>();

        public bool CheckLoginButtonEnabled { get; set; } = true;
        public string CheckLoginButtonText { get; set; } = "Check account";
        public string CheckLoginMessage { get; set; }
        public string Password
        {
            get => SelectedAccount?.Password;
            set => SelectedAccount.Password = value;
        }

        public Account SelectedAccount { get; set; } = new Account();

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

        public string SelectedWorld
        {
            get => SelectedAccount?.DefaultWorld;
            set => SelectedAccount.DefaultWorld = value;
        }

        public List<CharacterWorld> WorldList
        {
            get => SelectedAccount?.WorldList;
            set => SelectedAccount.WorldList = value;
        }

        #endregion


        #region Commands

        public ICommand CheckAccountCommand { get; set; }

        /// <summary>
        /// Close the dialog window without changing anything
        /// </summary>
        public ICommand CloseCommand { get; set; }
        public ICommand AddAccountCommand { get; set; }
        public ICommand DeleteAccountCommand { get; set; }


        #endregion

        #region ErrorHandeling

        public string ErrorMessage { get; set; }
        public bool IsError { get; set; }
        public Visibility IsErrorVisible => IsError ? Visibility.Visible : Visibility.Hidden;

        #endregion

        #endregion Properties
        #region Methods

        public void OnDialogOpen()
        {
            SyncAccounts();
        }

        private void SyncAccounts()
        {
            // Retrieve list of stored accounts from the settings file. 
            Accounts = new ObservableCollection<Account>(_settings.GetAccounts());

            if (Accounts.Count > 0)
            {
                LoadAccount(Accounts.Last());
            }
        }

        private async void CheckAccountCredentials()
        {
            CheckLoginButtonText = "Checking credentials";
            CheckLoginButtonEnabled = false;

            ConnectData connectData = new ConnectData
            {
                Username = Username,
                Password = Password,
                ServerCountryCode = ServerCountryCode
            };

            ConnectResult result = new ConnectResult();
            try
            {
                result = await _socketManager.TestConnection(connectData);
                CheckLoginMessage = result.IsConnected ?
                    "The credentials were successful!" :
                    "The credentials failed! Please try again.";
                SelectedAccount.IsConfirmed = result.IsConnected;

            }
            catch (Exception e)
            {
                string error = "Oops, the developer fucked something up :(";
                error += Environment.NewLine;
                error += $"show him this: {e}";

                CheckLoginMessage = error;
                Console.WriteLine(e);
            }

            CheckLoginButtonText = "Check account";
            CheckLoginButtonEnabled = true;
        }

        #endregion Methods

        public void LoadAccount(Account account)
        {
            SelectedAccount = account;

            SelectedAccount.PropertyChanged += (sender, args) =>
            {
                _settings.SetAccount(SelectedAccount);
            };
        }
    }
}
