
using CalculationTools.Common;
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


            AttackPlannerRows.Add(new AttackPlannerRowViewModel
            {
                IsChecked = true,
                VillageOrigin = new VillageSelectorViewModel()
            });

        }

        public List<AttackPlannerRowViewModel> AttackPlannerRows { get; set; } = new List<AttackPlannerRowViewModel>();



        private void SetupReactions()
        {
            Observable
                .FromEventPattern(
                    ev => DataEvents.VillagesUpdated += ev,
                    ev => DataEvents.VillagesUpdated -= ev)
                .Subscribe(x =>
                {
                    // VillageList = _dataManager.GetVillages(1);
                });
        }
    }
}

