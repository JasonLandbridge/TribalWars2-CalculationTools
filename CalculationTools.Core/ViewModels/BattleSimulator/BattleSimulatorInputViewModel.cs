using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CalculationTools.Core.Base;

namespace CalculationTools.Core.BattleSimulator
{
    public class BattleSimulatorInputViewModel : BaseViewModel
    {
        #region Constructors

        public BattleSimulatorInputViewModel()
        {
            SetupValues();
            DefaultValues();
        }

        private void SetupValues()
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

            //Add property notification to nested properties
            foreach (InputUnitRow inputUnitRow in Units)
            {
                inputUnitRow.PropertyChanged += (sender, args) => OnPropertyChanged();
            }

        }
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

        #region Loading

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

        #endregion



        public WeaponSet GetAtkWeapon()
        {
            if (InputAtkWeapon == null || Paladin.NumberOnAttack <= 0 || InputAtkWeapon.BelongsToUnitType == null)
            {
                return new WeaponSet();
            }

            UnitType unitType = (UnitType)InputAtkWeapon.BelongsToUnitType;
            decimal atkModifier = GameData.GetAtkModifierFromWeapon(unitType, InputAtkWeaponLevel);
            decimal defModifier = GameData.GetDefModifierFromWeapon(unitType, InputAtkWeaponLevel);
            return new WeaponSet(unitType, atkModifier, defModifier);
        }
        public WeaponSet GetDefWeapon()
        {
            if (InputDefWeapon == null || Paladin.NumberOnDefense <= 0 || InputDefWeapon.BelongsToUnitType == null)
            {
                return new WeaponSet();
            }

            UnitType unitType = (UnitType)InputDefWeapon.BelongsToUnitType;
            decimal atkModifier = GameData.GetAtkModifierFromWeapon(unitType, InputDefWeaponLevel);
            decimal defModifier = GameData.GetDefModifierFromWeapon(unitType, InputDefWeaponLevel);
            return new WeaponSet(unitType, atkModifier, defModifier);
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
        public int InputWall { get; set; }

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

        public int InputWeaponMastery { get; set; }
        public string InputWeaponMasteryImagePath { get; } = "/Resources/Img/info/info_weapon_mastery.png";
        public string InputWeaponMasteryLabel { get; } = "Weapon Mastery";

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
                DefUnits = DefUnits,
                AtkWeapon = GetAtkWeapon(),
                DefWeapon = GetDefWeapon()
            };

        }
    }
}
