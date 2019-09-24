using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ClassLibrary.Class
{
    public class InputUnitRow : UnitRow, INotifyPropertyChanged
    {
        private int _numberOnAttack = 0;
        private int _numberOnDefense = 0;

        public int NumberOnAttack
        {
            get => _numberOnAttack;
            set
            {
                _numberOnAttack = value;
                OnPropertyChanged();
            }
        }

        public int NumberOnDefense
        {
            get => _numberOnDefense;
            set
            {
                _numberOnDefense = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
