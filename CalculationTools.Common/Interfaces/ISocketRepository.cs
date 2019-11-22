using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CalculationTools.Common
{
    public interface ISocketRepository
    {
        Task<DateTime> GetSystemTimeAsync();
        Task<List<IVillage>> GetVillagesByAutocomplete(string nameToSearch);
        Task<bool> EstablishConnection(Account account);

        /// <summary>
        /// Will attempt to login with the account provided, if in test-mode then the connection will be closed immediately after.
        /// </summary>
        /// <param name="account">The account with which to login</param>
        /// <param name="testMode">Will close the connection immediately after if true</param>
        /// <returns>The connection result</returns>
        Task<ConnectResult> LoginWithAccount(Account account, bool testMode = false);
    }
}