using System.Collections.Generic;

namespace CalculationTools.Common
{
    public interface IGameDataRepository
    {
        #region Methods

        #region Accounts

        Account AddAccount();

        bool DeleteAccount(int accountId);

        bool DeleteAccount(Account account);

        Account GetAccount(int accountId, bool tw2AccountId = false);

        List<Account> GetAccounts(bool onlyConfirmed = false);
        void UpdateAccount(Account account);
        #endregion

        List<World> GetCharacterWorlds(int characterId);

        List<Server> GetServers();

        void UpdateGroups(List<IGroup> groupList);

        void UpdateVillages(List<IVillage> villages);


        #endregion Methods

        void UpdateAccountData(ILoginData loginData);
    }
}