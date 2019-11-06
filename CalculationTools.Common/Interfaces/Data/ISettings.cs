using System.Collections.Generic;

namespace CalculationTools.Common
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
        Account AddAccount(Account account = null);

        #endregion

        void DeleteAccount(Account account);
    }
}