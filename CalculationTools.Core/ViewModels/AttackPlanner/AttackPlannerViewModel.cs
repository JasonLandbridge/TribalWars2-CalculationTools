
using CalculationTools.Common;

namespace CalculationTools.Core
{
    public class AttackPlannerViewModel : BaseViewModel
    {
        private IDataManager _dataManager;
        private IDialogService _dialogService;

        public AttackPlannerViewModel(IDialogService dialogService,
            IDataManager dataManager)
        {
            _dialogService = dialogService;
            _dataManager = dataManager;
        }
    }
}
