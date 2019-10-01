using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CalculationTools.Core;
using CalculationTools.Core.Base;

namespace CalculationTools.App
{
    public class BattleInputTableViewModel : BaseViewModel
    {
        #region Fields
        private readonly IWindowFactory _mWindowFactory = new UnitImportWindowFactory();
        private int _inputWall = 20;

        #endregion Fields

        #region Constructors

        public BattleInputTableViewModel()
        {

            OpenImportUnitsCommand = new RelayCommand(OpenUnitImportWindow);

            DefaultValues();

            UnitSet atkUnits = new UnitSet
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

            UnitSet defUnits = new UnitSet
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

            LoadUnits(atkUnits, defUnits);

        }

        public void DefaultValues()
        {
            InputGrandmasterBonus = false;
            InputAtkWeaponLevel = 1;
            InputAtkWeapon = new EmptyWeapon();
            InputDefWeaponLevel = 1;
            InputDefWeapon = new EmptyWeapon();
            InputAtkChurch = GameData.FaithOptions[1];
            InputDefChurch = GameData.FaithOptions[1];
            InputLuck = 0;
            InputMorale = 100;
            InputNightBonus = false;

        }

        public void LoadUnits(UnitSet atkUnits, UnitSet defUnits)
        {
            Units.Add(Spearman = new InputUnitRow
            {
                ImagePath = GameData.Spearman.ImagePath,
                Name = GameData.Spearman.Name,
                NumberOnAttack = atkUnits.Spearman,
                NumberOnDefense = defUnits.Spearman
            });

            Units.Add(Swordsman = new InputUnitRow
            {
                ImagePath = GameData.Swordsman.ImagePath,
                Name = GameData.Swordsman.Name,
                NumberOnAttack = atkUnits.Swordsman,
                NumberOnDefense = defUnits.Swordsman
            });

            Units.Add(AxeFighter = new InputUnitRow
            {
                ImagePath = GameData.AxeFighter.ImagePath,
                Name = GameData.AxeFighter.Name,
                NumberOnAttack = atkUnits.AxeFighter,
                NumberOnDefense = defUnits.AxeFighter
            });

            Units.Add(Archer = new InputUnitRow
            {
                ImagePath = GameData.Archer.ImagePath,
                Name = GameData.Archer.Name,
                NumberOnAttack = atkUnits.Archer,
                NumberOnDefense = defUnits.Archer

            });

            Units.Add(LightCavalry = new InputUnitRow
            {
                ImagePath = GameData.LightCavalry.ImagePath,
                Name = GameData.LightCavalry.Name,
                NumberOnAttack = atkUnits.LightCavalry,
                NumberOnDefense = defUnits.LightCavalry

            });

            Units.Add(MountedArcher = new InputUnitRow
            {
                ImagePath = GameData.MountedArcher.ImagePath,
                Name = GameData.MountedArcher.Name,
                NumberOnAttack = atkUnits.MountedArcher,
                NumberOnDefense = defUnits.MountedArcher

            });

            Units.Add(HeavyCavalry = new InputUnitRow
            {
                ImagePath = GameData.HeavyCavalry.ImagePath,
                Name = GameData.HeavyCavalry.Name,
                NumberOnAttack = atkUnits.HeavyCavalry,
                NumberOnDefense = defUnits.HeavyCavalry

            });

            Units.Add(Ram = new InputUnitRow
            {
                ImagePath = GameData.Ram.ImagePath,
                Name = GameData.Ram.Name,
                NumberOnAttack = atkUnits.Ram,
                NumberOnDefense = defUnits.Ram
            });

            Units.Add(Catapult = new InputUnitRow
            {
                ImagePath = GameData.Catapult.ImagePath,
                Name = GameData.Catapult.Name,
                NumberOnAttack = atkUnits.Catapult,
                NumberOnDefense = defUnits.Catapult

            });

            Units.Add(Berserker = new InputUnitRow
            {
                ImagePath = GameData.Berserker.ImagePath,
                Name = GameData.Berserker.Name,
                NumberOnAttack = atkUnits.Berserker,
                NumberOnDefense = defUnits.Berserker

            });

            Units.Add(Trebuchet = new InputUnitRow
            {
                ImagePath = GameData.Trebuchet.ImagePath,
                Name = GameData.Trebuchet.Name,
                NumberOnAttack = atkUnits.Trebuchet,
                NumberOnDefense = defUnits.Trebuchet
            });

            Units.Add(Nobleman = new InputUnitRow
            {
                ImagePath = GameData.Nobleman.ImagePath,
                Name = GameData.Nobleman.Name,
                NumberOnAttack = atkUnits.Nobleman,
                NumberOnDefense = defUnits.Nobleman

            });

            Units.Add(Paladin = new InputUnitRow
            {
                ImagePath = GameData.Paladin.ImagePath,
                Name = GameData.Paladin.Name,
                NumberOnAttack = atkUnits.Paladin,
                NumberOnDefense = defUnits.Paladin

            });

            //Add property notification to nested properties
            foreach (InputUnitRow inputUnitRow in Units)
            {
                inputUnitRow.PropertyChanged += (sender, args) => OnPropertyChanged();
            }
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

        private void OpenUnitImportWindow()
        {
            _mWindowFactory.CreateNewWindow();

        }

        #endregion Constructors

        #region Properties

        #region Units

        public InputUnitRow Archer { get; set; }
        public InputUnitRow AxeFighter { get; set; }
        public InputUnitRow Berserker { get; set; }
        public InputUnitRow Catapult { get; set; }
        public InputUnitRow HeavyCavalry { get; set; }
        public InputUnitRow LightCavalry { get; set; }
        public InputUnitRow MountedArcher { get; set; }
        public InputUnitRow Nobleman { get; set; }
        public InputUnitRow Paladin { get; set; }
        public InputUnitRow Ram { get; set; }
        public InputUnitRow Spearman { get; set; }
        public InputUnitRow Swordsman { get; set; }
        public InputUnitRow Trebuchet { get; set; }

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

        public ObservableCollection<InputUnitRow> Units { get; set; } = new ObservableCollection<InputUnitRow>();

        #region Officers

        public bool InputGrandmasterBonus { get; set; }
        public string InputGrandmasterBonusImagePath { get; } = "/Resources/Img/units/unit_grandmaster.png";
        public string InputGrandmasterBonusLabel { get; } = "Grand Master";

        #endregion


        #endregion

        #region Buildings

        public FaithLevel InputAtkChurch { get; set; }
        public string InputChurchImagePath { get; } = "/Resources/Img/buildings/buildings_church.png";

        public string InputChurchLabel { get; } = "Church";

        public FaithLevel InputDefChurch { get; set; }
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

        public BaseWeapon InputAtkWeapon { get; set; }

        public int InputAtkWeaponLevel { get; set; }
        public BaseWeapon InputDefWeapon { get; set; }

        public int InputDefWeaponLevel { get; set; }

        public string InputPaladinWeaponImagePath { get; } = "/Resources/Img/weapons/paladin_weapon.png";

        public string InputPaladinWeaponLabel { get; } = "Paladin Weapon";
        #endregion

        #region Meta

        public int InputLuck { get; set; }

        public string InputLuckImagePath { get; } = "/Resources/Img/info/info_luck.png";

        public string InputLuckLabel { get; } = "Luck";

        public int InputMorale { get; set; }

        public string InputMoraleImagePath { get; } = "/Resources/Img/info/info_morale.png";

        public string InputMoraleLabel { get; } = "Morale";

        public bool InputNightBonus { get; set; }

        public string InputNightBonusImagePath { get; } = "/Resources/Img/info/info_nightbonus.png";

        public string InputNightBonusLabel { get; } = "Night bonus";


        #endregion

        #region Commands

        public ICommand OpenImportUnitsCommand { get; set; }

        #endregion

        public bool IsValid => TotalUnits != 0;
        #endregion Properties

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
