using CalculationTools.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CalculationTools.Data
{
    public interface IGameDataRepository
    {
        #region Properties

        DbContextOptions<CalculationToolsDBContext> DbContextOptions { get; set; }
        bool IsInUnitTestMode { get; set; }

        #endregion Properties

        #region Methods

        #region Accounts

        Account AddAccount();

        bool DeleteAccount(int accountId);

        bool DeleteAccount(Account account);

        Account GetAccount(int accountId, bool tw2AccountId = false);

        List<Account> GetAccounts(bool onlyConfirmed = false);
        void UpdateAccount(Account account);
        #endregion

        List<Server> GetServers();

        void UpdateGroups(List<IGroup> groupList);

        bool UpdateVillages(List<IVillage> villages);

        void UpdateLoginData(ILoginData loginData);

        Task<List<Village>> GetVillagesByAutocompleteAsync(string villageName);

        #endregion Methods

        List<Village> GetVillages(int characterId = 0);
        void DeleteDB();
    }
}