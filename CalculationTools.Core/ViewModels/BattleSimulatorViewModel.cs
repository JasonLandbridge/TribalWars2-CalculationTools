using CalculationTools.Core.Base;
using CalculationTools.Core.BattleSimulator;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Input;
using CalculationTools.Core.Extensions;

namespace CalculationTools.Core
{
    public class BattleSimulatorViewModel : BaseViewModel
    {
        #region Fields

        private readonly IDialogService dialogService;
        private bool _isDefenseStrengthShown;
        private bool _isResourcesLostShown;
        private bool isAttackStrengthShown;

        #endregion Fields

        #region Constructors

        public BattleSimulatorViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;

            ImportUnitCommand = new RelayCommand(SetImportedUnits);

            SetupBattleResult();

            BattleConfig battleConfig = new BattleConfig
            {
                AtkUnits = new UnitSet
                {
                    Spearman = 0,
                    Swordsman = 0,
                    AxeFighter = 13469,
                    Archer = 0,
                    LightCavalry = 1645,
                    MountedArcher = 1021,
                    HeavyCavalry = 0,
                    Ram = 0,
                    Catapult = 0,
                    Berserker = 0,
                    Trebuchet = 0,
                    Nobleman = 0,
                    Paladin = 1,
                },

                DefUnits = new UnitSet
                {
                    Spearman = 2000,
                    Swordsman = 2000,
                    AxeFighter = 4300,
                    Archer = 2000,
                    LightCavalry = 45,
                    MountedArcher = 1,
                    HeavyCavalry = 0,
                    Ram = 300,
                    Catapult = 10,
                    Berserker = 0,
                    Trebuchet = 0,
                    Nobleman = 0,
                    Paladin = 1,
                },

                BattleMeta = new BattleMeta
                {
                    GrandmasterBonus = true,
                    AtkWeaponLevel = 2,
                    AtkWeapon = GameData.AxeFighterWeapon,
                    DefWeaponLevel = 0,
                    DefWeapon = GameData.WeaponOptions[0],
                    AtkChurch = GameData.FaithOptions[4],
                    DefChurch = GameData.FaithOptions[1],
                    Luck = 13,
                    Morale = 62,
                    NightBonus = false,
                    WeaponMastery = 2,
                    WallLevel = 20
                }
            };

            BattleSimulatorInputViewModel.LoadBattleConfig(battleConfig);

            //Only bind to the property changed after all values have been loaded for the first time to prevent unnecessary recalculations
            BattleSimulatorInputViewModel.PropertyChanged += (sender, args) => RunBattleSimulator();
            RunBattleSimulator();
        }

        #endregion Constructors

        #region Methods

        public void SetupBattleResult()
        {
            AttackBattleResultTable.Header = "Attacking Units";
            DefenseBattleResultTable.Header = "Defending Units";
            AttackBattleResultTable.IsAttackTable = true;
            DefenseBattleResultTable.IsAttackTable = false;

            AttackBattleResultTable.ShowWallResult = false;

            DefenseBattleResultTable.WallResult.BattleResultValues = null;
            DefenseBattleResultTable.ShowWallResult = true;

            IsAttackStrengthShown = true;
            IsDefenseStrengthShown = true;
            IsResourcesLostShown = true;
        }

        public void UpdateBattleResult(BattleResult battleResult)
        {
            AttackBattleResultTable.BattleModifier = battleResult.AtkBattleModifier.ToString();
            DefenseBattleResultTable.BattleModifier = battleResult.DefModifierBeforeBattle.ToString();

            DefenseBattleResultTable.WallResult.Content = battleResult.WallResult;

            // Set values for the AttackResultTable
            AttackBattleResultTable.UnitAmount.BattleResultValues = battleResult.ListOfAtkNumbers.AddTotal();

            AttackBattleResultTable.AttackFromInfantry.BattleResultValues = battleResult.ListOfAtkFromInfantry.AddTotal().SetAbbreviation(true);
            AttackBattleResultTable.AttackFromCavalry.BattleResultValues = battleResult.ListOfAtkFromCavalry.AddTotal().SetAbbreviation(true);
            AttackBattleResultTable.AttackFromArchers.BattleResultValues = battleResult.ListOfAtkFromArchers.AddTotal().SetAbbreviation(true);

            AttackBattleResultTable.UnitLost.BattleResultValues = battleResult.ListOfAtkLostNumbers.AddTotal().SetColor(Colors.Red);
            AttackBattleResultTable.UnitsLostWood.BattleResultValues = battleResult.ListOfAtkLostWood.AddTotal().SetAbbreviation(true);
            AttackBattleResultTable.UnitsLostClay.BattleResultValues = battleResult.ListOfAtkLostClay.AddTotal().SetAbbreviation(true);
            AttackBattleResultTable.UnitsLostIron.BattleResultValues = battleResult.ListOfAtkLostIron.AddTotal().SetAbbreviation(true);
            AttackBattleResultTable.UnitsLeft.BattleResultValues = battleResult.ListOfAtkLeftNumbers.AddTotal();

            // Set values for the DefenseResultTable
            DefenseBattleResultTable.UnitAmount.BattleResultValues = battleResult.ListOfDefNumbers.AddTotal();

            DefenseBattleResultTable.DefenseFromInfantry.BattleResultValues = battleResult.ListOfDefFromInfantry.AddTotal().SetAbbreviation(true);
            DefenseBattleResultTable.DefenseFromCavalry.BattleResultValues = battleResult.ListOfDefFromCavalry.AddTotal().SetAbbreviation(true);
            DefenseBattleResultTable.DefenseFromArchers.BattleResultValues = battleResult.ListOfDefFromArchers.AddTotal().SetAbbreviation(true);

            DefenseBattleResultTable.UnitLost.BattleResultValues = battleResult.ListOfDefLostNumbers.AddTotal().SetColor(Colors.Red);
            DefenseBattleResultTable.UnitsLostWood.BattleResultValues = battleResult.ListOfDefLostWood.AddTotal().SetAbbreviation(true);
            DefenseBattleResultTable.UnitsLostClay.BattleResultValues = battleResult.ListOfDefLostClay.AddTotal().SetAbbreviation(true);
            DefenseBattleResultTable.UnitsLostIron.BattleResultValues = battleResult.ListOfDefLostIron.AddTotal().SetAbbreviation(true);
            DefenseBattleResultTable.UnitsLeft.BattleResultValues = battleResult.ListOfDefLeftNumbers.AddTotal();
        }

        public void SetImportedUnits()
        {
            var viewModel = new UnitImportWindowViewModel();

            dialogService.ShowDialog(viewModel);
        }

        /// <summary>
        /// Will run the battle simulator based on the inputs and will then show the result in the BattleResult table.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunBattleSimulator()
        {
            Debug.WriteLine("Battle Calculator updated!");
            BattleResult battleResult = GameData.SimulateBattle(BattleSimulatorInputViewModel.ToBattleConfig());
            UpdateBattleResult(battleResult);
        }

        #endregion Methods

        #region Properties

        #region BattleResult

        public int AtkBattleModifier { get; set; }
        public int DefBattleModifier { get; set; }

        #endregion BattleResult

        #region ViewModels

        /// <summary>
        /// The viewmodel that displays the battle results for the attacking side.
        /// </summary>
        public BattleResultTableViewModel AttackBattleResultTable { get; set; } = new BattleResultTableViewModel();

        /// <summary>
        /// The viewmodel for the input fields of the battle simulator.
        /// </summary>
        public BattleSimulatorInputViewModel BattleSimulatorInputViewModel { get; set; } = new BattleSimulatorInputViewModel();

        /// <summary>
        /// The viewmodel that displays the battle results for the defending side.
        /// </summary>
        public BattleResultTableViewModel DefenseBattleResultTable { get; set; } = new BattleResultTableViewModel();

        #endregion ViewModels

        #region Filters

        public bool IsAttackStrengthShown
        {
            get => isAttackStrengthShown;
            set
            {
                isAttackStrengthShown = value;
                AttackBattleResultTable.IsAttackStrengthShown = value;
                OnPropertyChanged();
            }
        }

        public bool IsDefenseStrengthShown
        {
            get => _isDefenseStrengthShown;
            set
            {
                _isDefenseStrengthShown = value;
                DefenseBattleResultTable.IsDefenseStrengthShown = value;
                OnPropertyChanged();
            }
        }

        public bool IsResourcesLostShown
        {
            get => _isResourcesLostShown;
            set
            {
                _isResourcesLostShown = value;
                AttackBattleResultTable.IsResourcesLostShown = value;
                DefenseBattleResultTable.IsResourcesLostShown = value;
                OnPropertyChanged();
            }
        }

        #endregion Filters

        #region Commands

        public ICommand ImportUnitCommand { get; set; }

        #endregion Commands

        #endregion Properties
    }
}