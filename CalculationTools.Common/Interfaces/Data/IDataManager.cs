namespace CalculationTools.Common
{
    public interface IDataManager
    {
        void SetupSettings();

        ISettings Settings { get; }
    }
}