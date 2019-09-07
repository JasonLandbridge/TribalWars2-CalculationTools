using System.ComponentModel;
using System.Runtime.CompilerServices;
using TribalWars2_CalculationTools.Annotations;

namespace TribalWars2_CalculationTools.Models
{
    public class CalculatorData : INotifyPropertyChanged
    {
        private int _atkSpear;
        public int AtkSpear
        {
            get => _atkSpear;
            set
            {
                if (_atkSpear != value)
                {
                    _atkSpear = value;
                    OnPropertyChanged();
                }
            }
        }

        public CalculatorData()
        {

        }

        public int DefSpear { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
