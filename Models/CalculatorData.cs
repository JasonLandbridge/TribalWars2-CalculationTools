using System.ComponentModel;
using System.Runtime.CompilerServices;
using Caliburn.Micro;
using TribalWars2_CalculationTools.Annotations;
using TribalWars2_CalculationTools.Class.Units;

namespace TribalWars2_CalculationTools.Models
{
    public class CalculatorData : INotifyPropertyChanged
    {

        public BindableCollection<BaseUnit> Units { get; set; } = new BindableCollection<BaseUnit>();

        public CalculatorData()
        {
            Units.Add(new Spearman());
            Units.Add(new Swordsman());
            Units.Add(new AxeFighter());
            Units.Add(new Archer());
            Units.Add(new LightCavalry());
            Units.Add(new MountedArcher());
            Units.Add(new HeavyCavalry());
            Units.Add(new Ram());
            Units.Add(new Catapult());
            Units.Add(new Berserker());
            Units.Add(new Trebuchet());
            Units.Add(new Nobleman());
            Units.Add(new Paladin());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
