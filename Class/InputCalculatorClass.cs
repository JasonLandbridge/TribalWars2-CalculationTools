using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using TribalWars2_CalculationTools.Annotations;

namespace TribalWars2_CalculationTools.Class
{
    public class InputCalculatorClass : INotifyPropertyChanged
    {
        private ObservableCollection<InputUnitRow> _units = new ObservableCollection<InputUnitRow>();

        private InputUnitRow _spearman;
        private InputUnitRow _swordsman;
        private InputUnitRow _axeFighter;
        private InputUnitRow _archer;
        private InputUnitRow _lightCavalry;
        private InputUnitRow _mountedArcher;
        private InputUnitRow _heavyCavalry;
        private InputUnitRow _ram;
        private InputUnitRow _catapult;
        private InputUnitRow _berserker;
        private InputUnitRow _trebuchet;
        private InputUnitRow _nobleman;
        private InputUnitRow _paladin;

        public InputUnitRow Spearman
        {
            get => _spearman;
            set
            {
                _spearman = value;
                this.OnPropertyChanged();
            }
        }

        public InputUnitRow Swordsman
        {
            get => _swordsman;
            set
            {
                _swordsman = value;
                this.OnPropertyChanged();
            }
        }

        public InputUnitRow AxeFighter
        {
            get => _axeFighter;
            set
            {
                _axeFighter = value;
                this.OnPropertyChanged();
            }
        }
        public InputUnitRow Archer
        {
            get => _archer;
            set
            {
                _archer = value;
                this.OnPropertyChanged();
            }
        }
        public InputUnitRow LightCavalry
        {
            get => _lightCavalry;
            set
            {
                _lightCavalry = value;
                this.OnPropertyChanged();
            }
        }
        public InputUnitRow MountedArcher
        {
            get => _mountedArcher;
            set
            {
                _mountedArcher = value;
                this.OnPropertyChanged();
            }
        }
        public InputUnitRow HeavyCavalry
        {
            get => _heavyCavalry;
            set
            {
                _heavyCavalry = value;
                this.OnPropertyChanged();
            }
        }

        public InputUnitRow Ram
        {
            get => _ram;
            set
            {
                _ram = value;
                this.OnPropertyChanged();
            }
        }

        public InputUnitRow Catapult
        {
            get => _catapult;
            set
            {
                _catapult = value;
                this.OnPropertyChanged();
            }
        }

        public InputUnitRow Berserker
        {
            get => _berserker;
            set
            {
                _berserker = value;
                this.OnPropertyChanged();
            }
        }

        public InputUnitRow Trebuchet
        {
            get => _trebuchet;
            set
            {
                _trebuchet = value;
                this.OnPropertyChanged();
            }
        }

        public InputUnitRow Nobleman
        {
            get => _nobleman;
            set
            {
                _nobleman = value;
                this.OnPropertyChanged();
            }
        }

        public InputUnitRow Paladin
        {
            get => _paladin;
            set
            {
                _paladin = value;
                this.OnPropertyChanged();
            }
        }





        public ObservableCollection<InputUnitRow> Units
        {
            get => _units;
            set
            {
                _units = value;
                this.OnPropertyChanged();
            }
        }

        public InputCalculatorClass()
        {
            Units.Add(Spearman = new InputUnitRow
            {
                ImagePath = GameData.Spearman.ImagePath,
                Name = GameData.Spearman.Name,
            });

            Units.Add(Swordsman = new InputUnitRow
            {
                ImagePath = GameData.Swordsman.ImagePath,
                Name = GameData.Swordsman.Name,
            });

            Units.Add(AxeFighter = new InputUnitRow
            {
                ImagePath = GameData.AxeFighter.ImagePath,
                Name = GameData.AxeFighter.Name,
            });

            Units.Add(Archer = new InputUnitRow
            {
                ImagePath = GameData.Archer.ImagePath,
                Name = GameData.Archer.Name,
            });

            Units.Add(LightCavalry = new InputUnitRow
            {
                ImagePath = GameData.LightCavalry.ImagePath,
                Name = GameData.LightCavalry.Name,
            });

            Units.Add(MountedArcher = new InputUnitRow
            {
                ImagePath = GameData.MountedArcher.ImagePath,
                Name = GameData.MountedArcher.Name,
            });

            Units.Add(HeavyCavalry = new InputUnitRow
            {
                ImagePath = GameData.HeavyCavalry.ImagePath,
                Name = GameData.HeavyCavalry.Name,
            });

            Units.Add(Ram = new InputUnitRow
            {
                ImagePath = GameData.Ram.ImagePath,
                Name = GameData.Ram.Name,
            });

            Units.Add(Catapult = new InputUnitRow
            {
                ImagePath = GameData.Catapult.ImagePath,
                Name = GameData.Catapult.Name,
            });

            Units.Add(Berserker = new InputUnitRow
            {
                ImagePath = GameData.Berserker.ImagePath,
                Name = GameData.Berserker.Name,
            });

            Units.Add(Trebuchet = new InputUnitRow
            {
                ImagePath = GameData.Trebuchet.ImagePath,
                Name = GameData.Trebuchet.Name,
            });

            Units.Add(Nobleman = new InputUnitRow
            {
                ImagePath = GameData.Nobleman.ImagePath,
                Name = GameData.Nobleman.Name,
            });

            Units.Add(Paladin = new InputUnitRow
            {
                ImagePath = GameData.Paladin.ImagePath,
                Name = GameData.Paladin.Name,
            });

        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
