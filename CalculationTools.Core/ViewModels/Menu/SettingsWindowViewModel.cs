using CalculationTools.Common;
using NLog;
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


        private readonly IGameDataRepository _gameDataRepository;
        private readonly IPlayerData _playerData;
        private readonly ISettings _settings;
        private readonly ISocketManager _socketManager;

        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        #endregion Fields

        #region Constructors

        public SettingsWindowViewModel(
            IDataManager dataManager,
            IPlayerData playerData,
            ISocketManager socketManager,
            IGameDataRepository gameDataRepository)
        {
            _gameDataRepository = gameDataRepository;
            _settings = dataManager.Settings;
            _playerData = playerData;
            _socketManager = socketManager;


            SetupEvents();
        }
        #endregion Constructors
        #region Events

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        #endregion Events

        #region Properties


        #region Accounts

        public ObservableCollection<Account> Accounts { get; set; } = new ObservableCollection<Account>();

        public string Username
        {
            get => SelectedAccount?.Username;
            set => SelectedAccount.Username = value;
        }

        public string Password
        {
            get => SelectedAccount?.Password;
            set => SelectedAccount.Password = value;
        }

        #region Checks

        public bool CheckLoginButtonEnabled { get; set; } = true;
        public string CheckLoginButtonText { get; set; } = "Check account";
        public string CheckLoginMessage { get; set; }

        #endregion

        public Account SelectedAccount { get; set; } = new Account();

        public List<Character> CharacterList
        {
            get => SelectedAccount?.CharacterList?.ToList();
            set => SelectedAccount.CharacterList = value;
        }
        public Character DefaultCharacter
        {
            get => SelectedAccount?.DefaultCharacter;
            set => SelectedAccount.DefaultCharacter = value;
        }

        public Server OnServer
        {
            get => SelectedAccount?.OnServer;
            set => SelectedAccount.OnServer = value;
        }

        public List<Server> ServerList => _gameDataRepository.GetServers();

        #endregion


        #region Commands

        public ICommand AddAccountCommand { get; set; }
        public ICommand CheckAccountCommand { get; set; }

        /// <summary>
        /// Close the dialog window without changing anything
        /// </summary>
        public ICommand CloseCommand { get; set; }
        public ICommand DeleteAccountCommand { get; set; }


        #endregion

        #region ErrorHandeling

        public string ErrorMessage { get; set; }
        public bool IsError { get; set; }
        public Visibility IsErrorVisible => IsError ? Visibility.Visible : Visibility.Hidden;

        #endregion

        #endregion Properties
        #region Methods

        #region Accounts
        private void DeleteAccount()
        {
            _gameDataRepository.DeleteAccount(SelectedAccount);
            SyncAccounts();
        }

        private void AddNewAccount()
        {
            Account account = _gameDataRepository.AddAccount();
            SyncAccounts();
        }

        private void SyncAccounts()
        {
            // Retrieve list of stored accounts from the settings file. 
            Accounts = new ObservableCollection<Account>(_gameDataRepository.GetAccounts());

            if (Accounts.Count > 0)
            {
                Account account = Accounts.FirstOrDefault(a => a.Id == _settings.LastLoadedAccountId);

                LoadAccount(account ?? Accounts.Last());
            }
        }

        public void LoadAccount(Account account)
        {
            SelectedAccount = account;

            _settings.LastLoadedAccountId = SelectedAccount.Id;

            SelectedAccount.PropertyChanged += (sender, args) =>
            {
                Account newAccount = sender as Account;

                // If any credentials are changed then reset IsConfirmed.
                if (SelectedAccount.Username != newAccount.Username ||
                SelectedAccount.Password != newAccount.Password ||
                SelectedAccount.ServerCountryCode != newAccount.ServerCountryCode)
                {
                    SelectedAccount.IsConfirmed = false;
                    CheckLoginMessage = "Please revalidate the credentials.";
                }

                _gameDataRepository.UpdateAccount(SelectedAccount);
            };
        }

        private async void CheckAccountCredentials()
        {
            CheckLoginButtonText = "Checking credentials";
            CheckLoginButtonEnabled = false;

            ConnectData connectData = new ConnectData
            {
                Username = Username,
                Password = Password,
                ServerCountryCode = OnServer.Id
            };

            ConnectResult result = new ConnectResult();
            try
            {
                result = await _socketManager.TestConnection(connectData);
                CheckLoginMessage = result.IsConnected ?
                    "The credentials were successful!" :
                    "The credentials failed! Please try again.";
                SelectedAccount.IsConfirmed = result.IsConnected;
                SelectedAccount.TW2AccountID = result.TW2AccountId;

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




        #endregion
        #region Setup
        public void OnDialogOpen()
        {
            SyncAccounts();
        }

        public void SetupEvents()
        {
            CloseCommand = new RelayCommand(
                () => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs()));
            CheckAccountCommand = new RelayCommand(CheckAccountCredentials);
            AddAccountCommand = new RelayCommand(AddNewAccount);
            DeleteAccountCommand = new RelayCommand(DeleteAccount);

            _playerData.LoginDataIsUpdated += (sender, args) =>
            {
                // Refresh the account from the database
                SelectedAccount = _gameDataRepository.GetAccount(SelectedAccount.Id);
                CharacterList = SelectedAccount.CharacterList.ToList();

                if (CharacterList.Count > 0)
                {
                    DefaultCharacter = CharacterList[0];
                }
                else
                {
                    ErrorMessage = "No worlds with characters available!";
                }
            };
        }

        #endregion
        #endregion Methods
    }
}
