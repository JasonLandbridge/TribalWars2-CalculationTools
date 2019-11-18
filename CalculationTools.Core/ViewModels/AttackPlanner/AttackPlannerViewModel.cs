
using CalculationTools.Common;
using CalculationTools.Data;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;

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

            SetupReactions();
        }

        public List<Village> VillageList { get; set; } = new List<Village>
        {
            new Village
            {
                Name = "TEST TEST TEST"
            }
        };

        private void SetupReactions()
        {
            var villageUpdatedObservable = Observable.FromEventPattern(
                ev => DataEvents.VillagesUpdated += ev,
                ev => DataEvents.VillagesUpdated -= ev);

            villageUpdatedObservable.Subscribe(x =>
            {
                VillageList = _dataManager.GetVillages(1);
            });
        }
    }
}

