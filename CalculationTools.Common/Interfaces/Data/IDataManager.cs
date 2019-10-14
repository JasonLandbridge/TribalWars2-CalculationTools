namespace CalculationTools.Common.Data
{
    public interface IDataManager
    {
        void SetupSettings();

        ISettings Settings { get; }
    }
}