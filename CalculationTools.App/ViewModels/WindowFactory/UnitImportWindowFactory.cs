using CalculationTools.Core;

namespace CalculationTools.App
{
    public class UnitImportWindowFactory : IWindowFactory
    {

        public void CreateNewWindow()
        {
            UnitImportWidow window = new UnitImportWidow
            {
                DataContext = new UnitImportWindowViewModel()
            };
            window.ShowDialog();


        }

    }
}
