using CalculationTools.Common;
using System.Collections.Generic;

namespace CalculationTools.Data
{
    /// <summary>
    /// This contains the real accounts used for mostly testing purposes
    /// </summary>
    public static class SecretData
    {
        public static List<Account> GetAccounts()
        {
            return new List<Account>{
                new Account
                {
                    Id = 1,
                    Username = "",
                    Password = "",
                    OnServerId = "",
                    IsConfirmed = false
                },
                new Account
                {
                    Id = 2,
                    Username = "",
                    Password = "",
                    OnServerId = "",
                    IsConfirmed = false
                }
            };
        }
    }
}
