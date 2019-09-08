using System.ComponentModel;
using System.Runtime.CompilerServices;
using Caliburn.Micro;
using TribalWars2_CalculationTools.Annotations;
using TribalWars2_CalculationTools.Class.Units;

namespace TribalWars2_CalculationTools.Models
{
    public class CalculatorData : PropertyChangedBase
    {
        private bool _inputDefChurch = true;
        private bool _inputAtkChurch = true;
        private decimal _inputMorale = 1;
        private int _inputLuck = 0;
        private int _inputWall = 0;
        private bool _inputNightBonus = true;

        public bool InputAtkChurch
        {
            get => _inputAtkChurch;
            set
            {
                _inputAtkChurch = value;
                NotifyOfPropertyChange(() => InputAtkChurch);
            }
        }
        public bool InputDefChurch
        {
            get => _inputDefChurch;
            set
            {
                _inputDefChurch = value;
                NotifyOfPropertyChange(() => InputDefChurch);
            }
        }

        public int InputWall
        {
            get => _inputWall;
            set
            {
                _inputWall = value;
                NotifyOfPropertyChange(() => InputWall);
            }
        }


        public bool InputNightBonus
        {
            get => _inputNightBonus;
            set
            {
                _inputNightBonus = value;
                NotifyOfPropertyChange(() => InputNightBonus);
            }
        }

        public decimal InputMorale
        {
            get => _inputMorale;
            set
            {
                _inputMorale = value;
                NotifyOfPropertyChange(() => InputMorale);
            }
        }

        public int InputLuck
        {
            get => _inputLuck;
            set
            {
                _inputLuck = value;
                NotifyOfPropertyChange(() => InputLuck);
            }
        }
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


    }
}
