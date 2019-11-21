using System.Collections.Generic;

namespace CalculationTools.Common
{
    public interface IDataManager
    {

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

        List<Village> GetVillages(int characterId = 0);

        void SetCharacterData(ICharacterData characterData);

        void SetConnectionResult(ConnectResult connectResult);

        void SetupSettings();
        void UpdateAccount(Account account);

        #endregion Methods
    }
}