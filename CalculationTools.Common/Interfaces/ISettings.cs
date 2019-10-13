using System.Collections.Generic;

namespace CalculationTools.Common
{
    public interface ISettings
    {
        #region Properties

        List<Account> Accounts { get; set; }
        bool IsAttackStrengthShown { get; set; }
        bool IsDefenseStrengthShown { get; set; }
        bool IsResourcesLostShown { get; set; }

        #endregion Properties

        #region Methods
        void Save();

        #endregion Methods
    }
}