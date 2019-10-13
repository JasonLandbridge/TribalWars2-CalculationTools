namespace CalculationTools.Core
{
    public static class ApplicationCore
    {
        public static void OnStartUp(IDialogService dialogService)
        {
            IoC.Setup(dialogService);
        }
    }
}
