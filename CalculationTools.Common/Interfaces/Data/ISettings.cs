namespace CalculationTools.Common
{
    public interface ISettings
    {
        #region Properties

        bool IsAttackStrengthShown { get; set; }
        bool IsDefenseStrengthShown { get; set; }
        bool IsResourcesLostShown { get; set; }
        string FileName { get; set; }
        int LastLoadedAccountIndex { get; set; }

        #endregion Properties

        #region Methods
        void Save();

        #endregion Methods

        void SetDefaultValues();

        #region Accounts

        #endregion

    }
}