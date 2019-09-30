using ClassLibrary.Utilities.WindowFactory;
using TribalWars2_CalculationTools.Views;

namespace TribalWars2_CalculationTools.ViewModels.WindowFactory
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
