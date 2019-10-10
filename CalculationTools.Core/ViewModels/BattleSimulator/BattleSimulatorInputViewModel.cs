using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CalculationTools.Core
{
    public class BattleSimulatorInputViewModel : BaseViewModel
    {
        #region Constructors

        public BattleSimulatorInputViewModel()
        {
            SetupValues();
            DefaultValues();
        }

        #endregion Constructors

        #region Properties

        #region Units

        #region UnitInput

        public static InputUnitRow Archer { get; set; } = new InputUnitRow
        {
            ImagePath = GameData.Archer.ImagePath,
            Name = GameData.Archer.Name,
        };

        public static InputUnitRow AxeFighter { get; set; } = new InputUnitRow
        {
            ImagePath = GameData.AxeFighter.ImagePath,
            Name = GameData.AxeFighter.Name,
        };

        public static InputUnitRow Berserker { get; set; } = new InputUnitRow
        {
            ImagePath = GameData.Berserker.ImagePath,
            Name = GameData.Berserker.Name,
        };

        public static InputUnitRow Catapult { get; set; } = new InputUnitRow
        {
            ImagePath = GameData.Catapult.ImagePath,
            Name = GameData.Catapult.Name,
        };

        public static InputUnitRow HeavyCavalry { get; set; } = new InputUnitRow
        {
            ImagePath = GameData.HeavyCavalry.ImagePath,
            Name = GameData.HeavyCavalry.Name,
        };

        public static InputUnitRow LightCavalry { get; set; } = new InputUnitRow
        {
            ImagePath = GameData.LightCavalry.ImagePath,
            Name = GameData.LightCavalry.Name,
        };

        public static InputUnitRow MountedArcher { get; set; } = new InputUnitRow
        {
            ImagePath = GameData.MountedArcher.ImagePath,
            Name = GameData.MountedArcher.Name,
        };

        public static InputUnitRow Nobleman { get; set; } = new InputUnitRow
        {
            ImagePath = GameData.Nobleman.ImagePath,
            Name = GameData.Nobleman.Name,
        };

        public static InputUnitRow Paladin { get; set; } = new InputUnitRow
        {
            ImagePath = GameData.Paladin.ImagePath,
            Name = GameData.Paladin.Name,
        };

        public static InputUnitRow Ram { get; set; } = new InputUnitRow
        {
            ImagePath = GameData.Ram.ImagePath,
            Name = GameData.Ram.Name,
        };

        public static InputUnitRow Spearman { get; set; } = new InputUnitRow
        {
            ImagePath = GameData.Spearman.ImagePath,
            Name = GameData.Spearman.Name,
        };

        public static InputUnitRow Swordsman { get; set; } = new InputUnitRow
        {
            ImagePath = GameData.Swordsman.ImagePath,
            Name = GameData.Swordsman.Name,
        };

        public static InputUnitRow Trebuchet { get; set; } = new InputUnitRow
        {
            ImagePath = GameData.Trebuchet.ImagePath,
            Name = GameData.Trebuchet.Name,
        };

        #endregion UnitInput

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

        public static ObservableCollection<InputUnitRow> Units { get; set; } = new ObservableCollection<InputUnitRow>
        {
            Spearman,
            Swordsman,
            AxeFighter,
            Archer,
            LightCavalry,
            MountedArcher,
            HeavyCavalry,
            Ram,
            Catapult,
            Berserker,
            Trebuchet,
            Nobleman,
            Paladin,
        };

        #region Officers

        public bool InputGrandmasterBonus { get; set; }
        public string InputGrandmasterBonusImagePath { get; } = "/Resources/Img/units/unit_grandmaster.png";
        public string InputGrandmasterBonusLabel { get; } = "Grand Master";

        #endregion Officers

        #endregion Units

        #region Buildings

        public FaithLevel InputAtkChurch { get; set; }
        public string InputChurchImagePath { get; } = "/Resources/Img/buildings/buildings_church.png";

        public string InputChurchLabel { get; } = "Church";

        public FaithLevel InputDefChurch { get; set; }
        public int InputWall { get; set; }

        public string InputWallImagePath { get; } = "/Resources/Img/buildings/buildings_wall.png";

        public string InputWallLabel { get; } = "Wall";

        #endregion Buildings

        #region Weapons

        public BaseWeapon InputAtkWeapon { get; set; }

        public int InputAtkWeaponLevel { get; set; }
        public BaseWeapon InputDefWeapon { get; set; }

        public int InputDefWeaponLevel { get; set; }

        public string InputPaladinWeaponImagePath { get; } = "/Resources/Img/weapons/paladin_weapon.png";

        public string InputPaladinWeaponLabel { get; } = "Paladin Weapon";

        #endregion Weapons

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
        public int InputWeaponMastery { get; set; }
        public string InputWeaponMasteryImagePath { get; } = "/Resources/Img/info/info_weapon_mastery.png";
        public string InputWeaponMasteryLabel { get; } = "Weapon Mastery";

        #endregion Meta

        #region Commands

        public ICommand OpenImportUnitsCommand { get; set; }

        #endregion Commands

        public bool IsValid => TotalUnits != 0;

        #endregion Properties

        #region Methods

        #region Loading

        public void LoadAtkUnits(UnitSet atkUnits)
        {
            Spearman.NumberOnAttack = atkUnits.Spearman;
            Swordsman.NumberOnAttack = atkUnits.Swordsman;
            AxeFighter.NumberOnAttack = atkUnits.AxeFighter;
            Archer.NumberOnAttack = atkUnits.Archer;
            LightCavalry.NumberOnAttack = atkUnits.LightCavalry;
            MountedArcher.NumberOnAttack = atkUnits.MountedArcher;
            HeavyCavalry.NumberOnAttack = atkUnits.HeavyCavalry;
            Ram.NumberOnAttack = atkUnits.Ram;
            Catapult.NumberOnAttack = atkUnits.Catapult;
            Berserker.NumberOnAttack = atkUnits.Berserker;
            Trebuchet.NumberOnAttack = atkUnits.Trebuchet;
            Nobleman.NumberOnAttack = atkUnits.Nobleman;
            Paladin.NumberOnAttack = atkUnits.Paladin;
        }

        public void LoadBattleConfig(BattleConfig battleConfig)
        {
            LoadUnits(battleConfig.AtkUnits, battleConfig.DefUnits);
            LoadBattleMeta(battleConfig.BattleMeta);
        }

        public void LoadBattleMeta(BattleMeta battleMeta)
        {
            InputGrandmasterBonus = battleMeta.GrandmasterBonus;
            InputAtkWeaponLevel = battleMeta.AtkWeaponLevel;
            InputAtkWeapon = battleMeta.AtkWeapon;
            InputDefWeaponLevel = battleMeta.DefWeaponLevel;
            InputDefWeapon = battleMeta.DefWeapon;
            InputAtkChurch = battleMeta.AtkChurch;
            InputDefChurch = battleMeta.DefChurch;
            InputLuck = battleMeta.Luck;
            InputMorale = battleMeta.Morale;
            InputWeaponMastery = battleMeta.WeaponMastery;
            InputNightBonus = battleMeta.NightBonus;
            InputWall = battleMeta.WallLevel;
        }

        public void LoadDefUnits(UnitSet defUnits)
        {
            Spearman.NumberOnDefense = defUnits.Spearman;
            Swordsman.NumberOnDefense = defUnits.Swordsman;
            AxeFighter.NumberOnDefense = defUnits.AxeFighter;
            Archer.NumberOnDefense = defUnits.Archer;
            LightCavalry.NumberOnDefense = defUnits.LightCavalry;
            MountedArcher.NumberOnDefense = defUnits.MountedArcher;
            HeavyCavalry.NumberOnDefense = defUnits.HeavyCavalry;
            Ram.NumberOnDefense = defUnits.Ram;
            Catapult.NumberOnDefense = defUnits.Catapult;
            Berserker.NumberOnDefense = defUnits.Berserker;
            Trebuchet.NumberOnDefense = defUnits.Trebuchet;
            Nobleman.NumberOnDefense = defUnits.Nobleman;
            Paladin.NumberOnDefense = defUnits.Paladin;
        }

        /// <summary>
        /// Set all the attacking and defending units in the input fields
        /// </summary>
        /// <param name="atkUnits">The Attacking UnitSet</param>
        /// <param name="defUnits">The Defending UnitSet</param>
        public void LoadUnits(UnitSet atkUnits, UnitSet defUnits)
        {
            LoadAtkUnits(atkUnits);
            LoadDefUnits(defUnits);
        }

        #endregion Loading

        #region Export

        public BattleConfig ToBattleConfig()
        {
            return new BattleConfig(GetAtkUnitSet(), GetDefUnitSet(), GetBattleMeta());
        }

        public UnitSet GetAtkUnitSet()
        {
            return new UnitSet
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
        }

        public UnitSet GetDefUnitSet()
        {
            return new UnitSet
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
        }

        public BattleMeta GetBattleMeta()
        {
            return new BattleMeta
            {
                GrandmasterBonus = InputGrandmasterBonus,
                AtkWeaponLevel = InputAtkWeaponLevel,
                AtkWeapon = InputAtkWeapon,
                DefWeaponLevel = InputDefWeaponLevel,
                DefWeapon = InputDefWeapon,
                AtkChurch = InputAtkChurch,
                DefChurch = InputDefChurch,
                WallLevel = InputWall,
                Luck = InputLuck,
                Morale = InputMorale,
                NightBonus = InputNightBonus,
                WeaponMastery = InputWeaponMastery
            };
        }

        #endregion Export

        public void DefaultValues()
        {
            BattleConfig defaultConfig = new BattleConfig
            {
                AtkUnits = new UnitSet
                {
                    Archer = 0,
                    AxeFighter = 0,
                    Berserker = 0,
                    Catapult = 0,
                    HeavyCavalry = 0,
                    LightCavalry = 0,
                    MountedArcher = 0,
                    Nobleman = 0,
                    Paladin = 0,
                    Ram = 0,
                    Spearman = 0,
                    Swordsman = 0,
                    Trebuchet = 0,
                },

                DefUnits = new UnitSet
                {
                    Archer = 0,
                    AxeFighter = 0,
                    Berserker = 0,
                    Catapult = 0,
                    HeavyCavalry = 0,
                    LightCavalry = 0,
                    MountedArcher = 0,
                    Nobleman = 0,
                    Paladin = 0,
                    Ram = 0,
                    Spearman = 0,
                    Swordsman = 0,
                    Trebuchet = 0,
                },

                BattleMeta = new BattleMeta
                {
                    GrandmasterBonus = false,
                    AtkWeaponLevel = 0,
                    AtkWeapon = GameData.WeaponOptions[0],
                    DefWeaponLevel = 0,
                    DefWeapon = GameData.WeaponOptions[0],
                    AtkChurch = GameData.FaithOptions[1],
                    DefChurch = GameData.FaithOptions[1],
                    Luck = 0,
                    Morale = 100,
                    NightBonus = false,
                    WallLevel = 0,
                    WeaponMastery = 0
                }
            };

            LoadBattleConfig(defaultConfig);
        }

        private void SetupValues()
        {
            //Add property notification to nested properties
            foreach (InputUnitRow inputUnitRow in Units)
            {
                inputUnitRow.PropertyChanged += (sender, args) => OnPropertyChanged();
            }
        }

        #endregion Methods
    }
}