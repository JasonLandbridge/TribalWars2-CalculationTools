using System;
using System.Collections.Generic;

namespace CalculationTools.Common
{
    public interface IDataManager
    {
        #region Events

        event EventHandler LoginDataIsUpdated;

        #endregion Events

        #region Properties

        ISettings Settings { get; }

        #endregion Properties

        #region Methods

        Account AddAccount();

        bool DeleteAccount(int accountId);

        bool DeleteAccount(Account account);

        Account GetAccount(int accountId, bool tw2AccountId = false);

        List<Account> GetAccounts(bool onlyConfirmed = false);

        List<Server> GetServers();
        void SetActiveCharacterId(int characterId);

        void SetLoginData(ILoginData loginData);
        void SetCharacterData(ICharacterData characterData);

        void SetupSettings();
        void UpdateAccount(Account account);

        #endregion Methods
    }
}