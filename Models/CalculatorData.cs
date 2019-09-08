using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Caliburn.Micro;
using TribalWars2_CalculationTools.Annotations;
using TribalWars2_CalculationTools.Class;
using TribalWars2_CalculationTools.Class.Enum;
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
        private BindableCollection<BaseUnit> _units = new BindableCollection<BaseUnit>();
        private BattleResult _lastBattleResult;

        public BattleResult LastBattleResult
        {
            get => _lastBattleResult;
            set
            {
                _lastBattleResult = value;
                NotifyOfPropertyChange(() => LastBattleResult);
            }
        }

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

        public BindableCollection<BaseUnit> Units
        {
            get => _units;
            set
            {
                Debug.WriteLine("SUCCESS!!!");

                _units = value;
            }
        }

        public Spearman Spearman
        {
            get => (Spearman)Units[0];
            set
            {
                Units[0] = value;
                NotifyOfPropertyChange(() => Spearman);
                ValueUpdated();
            }
        }
        public Swordsman Swordsman
        {
            get => (Swordsman)Units[1];
            set
            {
                Units[1] = value;
                NotifyOfPropertyChange(() => Swordsman);
                ValueUpdated();
            }
        }
        public AxeFighter AxeFighter
        {
            get => (AxeFighter)Units[2];
            set
            {
                Units[2] = value;
                NotifyOfPropertyChange(() => AxeFighter);
                ValueUpdated();
            }
        }
        public Archer Archer
        {
            get => (Archer)Units[3];
            set
            {
                Units[3] = value;
                NotifyOfPropertyChange(() => Archer);
                ValueUpdated();
            }
        }
        public LightCavalry LightCavalry
        {
            get => (LightCavalry)Units[4];
            set
            {
                Units[4] = value;
                NotifyOfPropertyChange(() => LightCavalry);
                ValueUpdated();
            }
        }
        public MountedArcher MountedArcher
        {
            get => (MountedArcher)Units[5];
            set
            {
                Units[5] = value;
                NotifyOfPropertyChange(() => MountedArcher);
                ValueUpdated();
            }
        }
        public HeavyCavalry HeavyCavalry
        {
            get => (HeavyCavalry)Units[6];
            set
            {
                Units[6] = value;
                NotifyOfPropertyChange(() => HeavyCavalry);
                ValueUpdated();
            }
        }
        public Ram Ram
        {
            get => (Ram)Units[7];
            set
            {
                Units[7] = value;
                NotifyOfPropertyChange(() => Ram);
                ValueUpdated();
            }
        }

        public Catapult Catapult
        {
            get => (Catapult)Units[8];
            set
            {
                Units[8] = value;
                NotifyOfPropertyChange(() => Catapult);
                ValueUpdated();
            }
        }

        public Berserker Berserker
        {
            get => (Berserker)Units[9];
            set
            {
                Units[9] = value;
                NotifyOfPropertyChange(() => Berserker);
                ValueUpdated();
            }
        }

        public Trebuchet Trebuchet
        {
            get => (Trebuchet)Units[10];
            set
            {
                Units[10] = value;
                NotifyOfPropertyChange(() => Trebuchet);
                ValueUpdated();
            }
        }

        public Nobleman Nobleman
        {
            get => (Nobleman)Units[11];
            set
            {
                Units[11] = value;
                NotifyOfPropertyChange(() => Nobleman);
                ValueUpdated();
            }
        }

        public Paladin Paladin
        {
            get => (Paladin)Units[12];
            set
            {
                Units[12] = value;
                NotifyOfPropertyChange(() => Paladin);
                ValueUpdated();
            }
        }

        public CalculatorData()
        {
            // Do not change the order!
            Units.Add(new Spearman(this));
            Units.Add(new Swordsman(this));
            Units.Add(new AxeFighter(this));
            Units.Add(new Archer(this));

            Units.Add(new LightCavalry(this));
            Units.Add(new MountedArcher(this));
            Units.Add(new HeavyCavalry(this));
            Units.Add(new Ram(this));

            Units.Add(new Catapult(this));
            Units.Add(new Berserker(this));
            Units.Add(new Trebuchet(this));

            Units.Add(new Nobleman(this));
            Units.Add(new Paladin(this));

            LastBattleResult = new BattleResult(Units.ToList());

        }

        public void ValueUpdated()
        {
            this.SimulateBattle();
        }

        public int GetTotalAttackInfantry()
        {
            int totalAttack = 0;
            foreach (BaseUnit unit in Units)
            {
                if (unit.UnitType == UnitType.Infantry)
                {
                    totalAttack += unit.GetTotalInfantryAtkStrength;
                }
            }

            return totalAttack;
        }

        public int GetTotalDefenseInfantry()
        {
            int totalDefense = 0;
            foreach (BaseUnit unit in Units)
            {
                if (unit.UnitType == UnitType.Infantry)
                {
                    totalDefense += unit.GetTotalDefFromInfantry;
                }
            }
            return totalDefense;
        }
        public void SimulateBattle()
        {
            BattleResult result = new BattleResult(Units.ToList());


            Spearman.NumberOnAttackLost = Spearman.NumberOnAttack;
            Swordsman.NumberOnAttackLost = Swordsman.NumberOnAttack;

            LastBattleResult = result;

        }


    }
}
