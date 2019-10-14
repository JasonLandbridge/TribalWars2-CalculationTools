using CalculationTools.Core;

namespace CalculationTools.App
{
    /// <summary>
    /// Interaction logic for UnitImportWidow.xaml
    /// </summary>
    public partial class UnitImportWidow : IDialog
    {
        public UnitImportWidow(UnitImportWindowViewModel importWindowViewModel)
        {
            InitializeComponent();
            DataContext = importWindowViewModel;
        }

    }
}
