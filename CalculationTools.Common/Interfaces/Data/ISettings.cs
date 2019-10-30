using System.Collections.Generic;
using CalculationTools.Common.Entities.World;

namespace CalculationTools.Common.Data
{
    public interface ISettings
    {
        #region Properties

        bool IsAttackStrengthShown { get; set; }
        bool IsDefenseStrengthShown { get; set; }
        bool IsResourcesLostShown { get; set; }
        string FileName { get; set; }

        #endregion Properties

        #region Methods
        void Save();

        #endregion Methods

        void SetDefaultValues();

        #region Accounts

        List<Account> GetAccounts(bool onlyConfirmed = false);
        void SetAccount(Account account);
        Account AddAccount(Account account);

        #endregion

    }
}