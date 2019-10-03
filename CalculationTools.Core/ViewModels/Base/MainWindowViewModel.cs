using System.Windows.Input;
using CalculationTools.Core.Base;

namespace CalculationTools.Core
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly IDialogService dialogService;

        public MainWindowViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
        }

    }
}
