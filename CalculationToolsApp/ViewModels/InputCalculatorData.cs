using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using ClassLibrary.Class;
using ClassLibrary.Class.Weapons;
using ClassLibrary.Enums;
using ClassLibrary.Structs;
using ClassLibrary.Utilities;

namespace TribalWars2_CalculationTools.ViewModels
{
    public class InputCalculatorData : INotifyPropertyChanged
    {
        #region Fields

        private InputUnitRow _archer;
        private InputUnitRow _axeFighter;
        private InputUnitRow _berserker;
        private InputUnitRow _catapult;
        private bool _grandmasterBonus = false;
        private InputUnitRow _heavyCavalry;
        private FaithLevel _inputAtkChurch = GameData.FaithOptions[1];
        private int _inputAtkWeaponLevel = 1;
        private FaithLevel _inputDefChurch = GameData.FaithOptions[1];
        private int _inputDefWeaponLevel = 1;
        private int _inputLuck = 0;
        private int _inputMorale = 100;
        private bool _inputNightBonus = false;
        private int _inputWall = 20;
        private InputUnitRow _lightCavalry;
        private InputUnitRow _mountedArcher;
        private InputUnitRow _nobleman;
        private InputUnitRow _paladin;
        private InputUnitRow _ram;
        private InputUnitRow _spearman;
        private InputUnitRow _swordsman;
        private InputUnitRow _trebuchet;
        private ObservableCollection<InputUnitRow> _units = new ObservableCollection<InputUnitRow>();
        private BaseWeapon _inputAtkWeapon;
        private BaseWeapon _inputDefWeapon;

        #endregion Fields

        #region Constructors

        public InputCalculatorData()
        {

            UnitSet AtkUnits = new UnitSet
            {
                Spearman = 0,
                Swordsman = 0,
                AxeFighter = 10000,
                Archer = 0,
                LightCavalry = 0,
                MountedArcher = 0,
                HeavyCavalry = 0,
                Ram = 90000,
                Catapult = 0,
                Berserker = 0,
                Trebuchet = 0,
                Nobleman = 0,
                Paladin = 0,

            };

            UnitSet DefUnits = new UnitSet
            {
                Spearman = 1000,
                Swordsman = 1000,
                AxeFighter = 0,
                Archer = 0,
                LightCavalry = 0,
                MountedArcher = 0,
                HeavyCavalry = 0,
                Ram = 0,
                Catapult = 0,
                Berserker = 0,
                Trebuchet = 100,
                Nobleman = 0,
                Paladin = 0,

            };


            Units.Add(Spearman = new InputUnitRow
            {
                ImagePath = GameData.Spearman.ImagePath,
                Name = GameData.Spearman.Name,
                NumberOnAttack = AtkUnits.Spearman,
                NumberOnDefense = DefUnits.Spearman
            });

            Units.Add(Swordsman = new InputUnitRow
            {
                ImagePath = GameData.Swordsman.ImagePath,
                Name = GameData.Swordsman.Name,
                NumberOnAttack = AtkUnits.Swordsman,
                NumberOnDefense = DefUnits.Swordsman
            });

            Units.Add(AxeFighter = new InputUnitRow
            {
                ImagePath = GameData.AxeFighter.ImagePath,
                Name = GameData.AxeFighter.Name,
                NumberOnAttack = AtkUnits.AxeFighter,
                NumberOnDefense = DefUnits.AxeFighter
            });

            Units.Add(Archer = new InputUnitRow
            {
                ImagePath = GameData.Archer.ImagePath,
                Name = GameData.Archer.Name,
                NumberOnAttack = AtkUnits.Archer,
                NumberOnDefense = DefUnits.Archer

            });

            Units.Add(LightCavalry = new InputUnitRow
            {
                ImagePath = GameData.LightCavalry.ImagePath,
                Name = GameData.LightCavalry.Name,
                NumberOnAttack = AtkUnits.LightCavalry,
                NumberOnDefense = DefUnits.LightCavalry

            });

            Units.Add(MountedArcher = new InputUnitRow
            {
                ImagePath = GameData.MountedArcher.ImagePath,
                Name = GameData.MountedArcher.Name,
                NumberOnAttack = AtkUnits.MountedArcher,
                NumberOnDefense = DefUnits.MountedArcher

            });

            Units.Add(HeavyCavalry = new InputUnitRow
            {
                ImagePath = GameData.HeavyCavalry.ImagePath,
                Name = GameData.HeavyCavalry.Name,
                NumberOnAttack = AtkUnits.HeavyCavalry,
                NumberOnDefense = DefUnits.HeavyCavalry

            });

            Units.Add(Ram = new InputUnitRow
            {
                ImagePath = GameData.Ram.ImagePath,
                Name = GameData.Ram.Name,
                NumberOnAttack = AtkUnits.Ram,
                NumberOnDefense = DefUnits.Ram
            });

            Units.Add(Catapult = new InputUnitRow
            {
                ImagePath = GameData.Catapult.ImagePath,
                Name = GameData.Catapult.Name,
                NumberOnAttack = AtkUnits.Catapult,
                NumberOnDefense = DefUnits.Catapult

            });

            Units.Add(Berserker = new InputUnitRow
            {
                ImagePath = GameData.Berserker.ImagePath,
                Name = GameData.Berserker.Name,
                NumberOnAttack = AtkUnits.Berserker,
                NumberOnDefense = DefUnits.Berserker

            });

            Units.Add(Trebuchet = new InputUnitRow
            {
                ImagePath = GameData.Trebuchet.ImagePath,
                Name = GameData.Trebuchet.Name,
                NumberOnAttack = AtkUnits.Trebuchet,
                NumberOnDefense = DefUnits.Trebuchet
            });

            Units.Add(Nobleman = new InputUnitRow
            {
                ImagePath = GameData.Nobleman.ImagePath,
                Name = GameData.Nobleman.Name,
                NumberOnAttack = AtkUnits.Nobleman,
                NumberOnDefense = DefUnits.Nobleman

            });

            Units.Add(Paladin = new InputUnitRow
            {
                ImagePath = GameData.Paladin.ImagePath,
                Name = GameData.Paladin.Name,
                NumberOnAttack = AtkUnits.Paladin,
                NumberOnDefense = DefUnits.Paladin

            });


        }

        public WeaponSet GetAtkWeapon()
        {
            if (InputAtkWeapon == null || Paladin.NumberOnAttack <= 0)
            {
                return new WeaponSet();
            }

            UnitType unitType = InputAtkWeapon.BelongsToUnitType;
            decimal atkModifier = GameData.GetAtkModifierFromWeapon(unitType, InputAtkWeaponLevel);
            decimal defModifier = GameData.GetDefModifierFromWeapon(unitType, InputAtkWeaponLevel);
            return new WeaponSet(unitType, atkModifier, defModifier);
        }
        public WeaponSet GetDefWeapon()
        {
            if (InputDefWeapon == null || Paladin.NumberOnDefense <= 0)
            {
                return new WeaponSet();
            }

            UnitType unitType = InputDefWeapon.BelongsToUnitType;
            decimal atkModifier = GameData.GetAtkModifierFromWeapon(unitType, InputDefWeaponLevel);
            decimal defModifier = GameData.GetDefModifierFromWeapon(unitType, InputDefWeaponLevel);
            return new WeaponSet(unitType, atkModifier, defModifier);
        }

        #endregion Constructors

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        #region Units

        public InputUnitRow Archer
        {
            get => _archer;
            set
            {
                _archer = value;
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

        public InputUnitRow Berserker
        {
            get => _berserker;
            set
            {
                _berserker = value;
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

        public InputUnitRow HeavyCavalry
        {
            get => _heavyCavalry;
            set
            {
                _heavyCavalry = value;
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

        public InputUnitRow Ram
        {
            get => _ram;
            set
            {
                _ram = value;
                this.OnPropertyChanged();
            }
        }

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

        public InputUnitRow Trebuchet
        {
            get => _trebuchet;
            set
            {
                _trebuchet = value;
                this.OnPropertyChanged();
            }
        }

        public int TotalAtkUnits
        {
            get
            {
                return Units.Sum(unit => unit.NumberOnAttack);
            }
        }

        public int TotalDefUnits
        {
            get
            {
                return Units.Sum(unit => unit.NumberOnDefense);
            }
        }

        public int TotalUnits => TotalAtkUnits + TotalDefUnits;

        public ObservableCollection<InputUnitRow> Units
        {
            get => _units;
            set
            {
                _units = value;
                this.OnPropertyChanged();
            }
        }

        #region Officers

        public bool InputGrandmasterBonus
        {
            get => _grandmasterBonus;
            set
            {
                _grandmasterBonus = value;
                OnPropertyChanged();
            }
        }

        public string InputGrandmasterBonusImagePath { get; } = "/Resources/Img/units/unit_grandmaster.png";
        public string InputGrandmasterBonusLabel { get; } = "Grand Master";

        #endregion


        #endregion

        #region Buildings

        public FaithLevel InputAtkChurch
        {
            get => _inputAtkChurch;
            set
            {
                _inputAtkChurch = value;
                OnPropertyChanged();
            }
        }
        public string InputChurchImagePath { get; } = "/Resources/Img/buildings/buildings_church.png";

        public string InputChurchLabel { get; } = "Church";

        public FaithLevel InputDefChurch
        {
            get => _inputDefChurch;
            set
            {
                _inputDefChurch = value;
                OnPropertyChanged();
            }
        }

        public int InputWall
        {
            get => Math.Clamp(_inputWall, 0, 20);
            set
            {
                _inputWall = Math.Clamp(value, 0, 20);
                OnPropertyChanged();
            }
        }

        public string InputWallImagePath { get; } = "/Resources/Img/buildings/buildings_wall.png";

        public string InputWallLabel { get; } = "Wall";



        #endregion

        #region Weapons

        public BaseWeapon InputAtkWeapon
        {
            get => _inputAtkWeapon;
            set
            {
                _inputAtkWeapon = value;
                OnPropertyChanged();
            }
        }

        public int InputAtkWeaponLevel
        {
            get => _inputAtkWeaponLevel;
            set
            {
                _inputAtkWeaponLevel = value;
                OnPropertyChanged();
            }
        }
        public BaseWeapon InputDefWeapon
        {
            get => _inputDefWeapon;
            set
            {
                _inputDefWeapon = value;
                OnPropertyChanged();
            }
        }

        public int InputDefWeaponLevel
        {
            get => _inputDefWeaponLevel;
            set
            {
                _inputDefWeaponLevel = value;
                OnPropertyChanged();
            }
        }

        public string InputPaladinWeaponImagePath { get; } = "/Resources/Img/weapons/paladin_weapon.png";

        public string InputPaladinWeaponLabel { get; } = "Paladin Weapon";
        #endregion



        #region Meta

        public int InputLuck
        {
            get => _inputLuck;
            set
            {
                _inputLuck = value;
                OnPropertyChanged();
            }
        }

        public string InputLuckImagePath { get; } = "/Resources/Img/info/info_luck.png";

        public string InputLuckLabel { get; } = "Luck";

        public int InputMorale
        {
            get => _inputMorale;
            set
            {
                _inputMorale = value;
                OnPropertyChanged();
            }
        }

        public string InputMoraleImagePath { get; } = "/Resources/Img/info/info_morale.png";

        public string InputMoraleLabel { get; } = "Morale";

        public bool InputNightBonus
        {
            get => _inputNightBonus;
            set
            {
                _inputNightBonus = value;
                OnPropertyChanged();
            }
        }

        public string InputNightBonusImagePath { get; } = "/Resources/Img/info/info_nightbonus.png";

        public string InputNightBonusLabel { get; } = "Night bonus";


        #endregion

        public bool IsValid => (TotalUnits != 0);
        #endregion Properties

        #region Methods

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion Methods

        public BattleResult ToBattleResult()
        {
            UnitSet AtkUnits = new UnitSet
            {
                Spearman = Spearman.NumberOnAttack,
                Swordsman = Swordsman.NumberOnAttack,
                AxeFighter = AxeFighter.NumberOnAttack,
                Archer = Archer.NumberOnAttack,
                LightCavalry = LightCavalry.NumberOnAttack,
                MountedArcher = MountedArcher.NumberOnAttack,
                HeavyCavalry = HeavyCavalry.NumberOnAttack,
                Ram = Ram.NumberOnAttack,
                Catapult = Catapult.NumberOnAttack,
                Berserker = Berserker.NumberOnAttack,
                Trebuchet = Trebuchet.NumberOnAttack,
                Nobleman = Nobleman.NumberOnAttack,
                Paladin = Paladin.NumberOnAttack,
            };

            UnitSet DefUnits = new UnitSet
            {
                Spearman = Spearman.NumberOnDefense,
                Swordsman = Swordsman.NumberOnDefense,
                AxeFighter = AxeFighter.NumberOnDefense,
                Archer = Archer.NumberOnDefense,
                LightCavalry = LightCavalry.NumberOnDefense,
                MountedArcher = MountedArcher.NumberOnDefense,
                HeavyCavalry = HeavyCavalry.NumberOnDefense,
                Ram = Ram.NumberOnDefense,
                Catapult = Catapult.NumberOnDefense,
                Berserker = Berserker.NumberOnDefense,
                Trebuchet = Trebuchet.NumberOnDefense,
                Nobleman = Nobleman.NumberOnDefense,
                Paladin = Paladin.NumberOnDefense,
            };

            return new BattleResult
            {
                WallLevelBefore = InputWall,
                AtkUnits = AtkUnits,
                DefUnits = DefUnits
            };

        }
    }
}
