namespace CalculationTools.Core
{
    public class AttackPlannerRowViewModel : BaseViewModel
    {
        public bool IsChecked { get; set; }

        public VillageSelectorViewModel VillageOrigin { get; set; }


        public AttackPlannerRowViewModel()
        {
            VillageOrigin = new VillageSelectorViewModel();
            VillageOrigin.PropertyChanged += (sender, args) => OnPropertyChanged();
        }

    }
}
