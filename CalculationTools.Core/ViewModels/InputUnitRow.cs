namespace CalculationTools.Core
{
    public class InputUnitRow : UnitRow
    {
        private int _numberOnDefense = 0;
        private int _numberOnAttack = 0;

        public int NumberOnAttack
        {
            get => _numberOnAttack;
            set
            {
                _numberOnAttack = value;
                OnPropertyChanged("Units");
            }
        }

        public int NumberOnDefense
        {
            get => _numberOnDefense;
            set
            {
                _numberOnDefense = value;
                OnPropertyChanged("Units");
            }
        }

    }
}
