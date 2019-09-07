using System.ComponentModel;
using System.Runtime.CompilerServices;
using Caliburn.Micro;
using TribalWars2_CalculationTools.Annotations;
using TribalWars2_CalculationTools.Class.Units;

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
        
        public Spearman Spearman { get; set; }
        public Swordsman Swordsman { get; set; }

        public BindableCollection<BaseUnit> Units { get; set; } = new BindableCollection<BaseUnit>();

        public CalculatorData()
        {
            Units.Add(new Spearman());
            Units.Add(new Swordsman());
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
