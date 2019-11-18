using System.Windows.Input;
using System.Windows.Media;
using CalculationTools.Common;

namespace CalculationTools.Core
{
    public class BattleSimulatorViewModel : BaseViewModel
    {
        #region Fields

        private readonly BattleInputViewModel _battleInputViewModel;
        private readonly UnitImportWindowViewModel _unitImportWindowViewModel;
        private readonly IDialogService _dialogService;
        private readonly IDataManager _dataManager;
        private readonly ISettings _settings;

        private bool _isAttackStrengthShown;
        private bool _isDefenseStrengthShown;
        private bool _isResourcesLostShown;

        #endregion Fields

        #region Constructors

        public BattleSimulatorViewModel(
            BattleInputViewModel battleInputViewModel,
            UnitImportWindowViewModel unitImportWindowViewModel,
            IDialogService dialogService,
            IDataManager dataManager)
        {
            _battleInputViewModel = battleInputViewModel;
            _unitImportWindowViewModel = unitImportWindowViewModel;
            _dialogService = dialogService;
            _dataManager = dataManager;
            _settings = dataManager.Settings;

            ImportUnitCommand = new RelayCommand(SetImportedUnits);

            SetupBattleResult();

            BattleConfig battleConfig = new BattleConfig
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
                    GrandmasterBonus = true,
                    AtkWeaponLevel = 0,
                    AtkWeapon = GameData.WeaponOptions[0],
                    DefWeaponLevel = 0,
                    DefWeapon = GameData.WeaponOptions[0],
                    AtkChurch = GameData.FaithOptions[1],
                    DefChurch = GameData.FaithOptions[1],
                    Luck = 0,
                    Morale = 100,
                    NightBonus = false,
                    WeaponMastery = 3,
                    WallLevel = 0
                }
            };

            _battleInputViewModel.LoadBattleConfig(battleConfig);

            //Only bind to the property changed after all values have been loaded for the first time to prevent unnecessary recalculations
            _battleInputViewModel.PropertyChanged += (sender, args) =>
            {
                RunBattleSimulator();
            };
            RunBattleSimulator();

        }

        #endregion Constructors

        #region Methods

        private void SetupBattleResult()
        {
            AttackBattleResultTable.Header = "Attacking Units";
            DefenseBattleResultTable.Header = "Defending Units";
            AttackBattleResultTable.IsAttackTable = true;
            DefenseBattleResultTable.IsAttackTable = false;

            AttackBattleResultTable.ShowWallResult = false;

            DefenseBattleResultTable.WallResult.BattleResultValues = null;
            DefenseBattleResultTable.ShowWallResult = true;

            // Load settings
            IsAttackStrengthShown = _settings.IsAttackStrengthShown;
            IsDefenseStrengthShown = _settings.IsDefenseStrengthShown;
            IsResourcesLostShown = _settings.IsResourcesLostShown;
        }

        private void UpdateBattleResult(BattleResult battleResult)
        {
            AttackBattleResultTable.BattleModifier = battleResult.AtkBattleModifier.ToString();
            DefenseBattleResultTable.BattleModifier = battleResult.DefModifierBeforeBattle.ToString();
            DefenseBattleResultTable.WallResult.Content = battleResult.WallResult;

            // Set values for the AttackResultTable
            AttackBattleResultTable.UnitAmount.BattleResultValues = battleResult.ListOfAtkNumbers.ValuesWithTotal;

            AttackBattleResultTable.AttackFromInfantry.BattleResultValues = battleResult.ListOfAtkFromInfantry.ValuesWithTotal;
            AttackBattleResultTable.AttackFromCavalry.BattleResultValues = battleResult.ListOfAtkFromCavalry.ValuesWithTotal;
            AttackBattleResultTable.AttackFromArchers.BattleResultValues = battleResult.ListOfAtkFromArchers.ValuesWithTotal;

            AttackBattleResultTable.UnitLost.BattleResultValues = battleResult.ListOfAtkLostNumbers.ValuesWithTotal.SetColor(Colors.Red);
            AttackBattleResultTable.UnitsLostWood.BattleResultValues = battleResult.AtkWoodLost.ValuesWithTotal;
            AttackBattleResultTable.UnitsLostClay.BattleResultValues = battleResult.AtkClayLost.ValuesWithTotal;
            AttackBattleResultTable.UnitsLostIron.BattleResultValues = battleResult.AtkIronLost.ValuesWithTotal;
            AttackBattleResultTable.UnitsLostResourceTotal.BattleResultValues = battleResult.AtkTotalResourcesLost.ValuesWithTotal;

            AttackBattleResultTable.UnitsLeft.BattleResultValues = battleResult.ListOfAtkLeftNumbers.ValuesWithTotal;

            // Set values for the DefenseResultTable
            DefenseBattleResultTable.UnitAmount.BattleResultValues = battleResult.ListOfDefNumbers.ValuesWithTotal;

            DefenseBattleResultTable.DefenseFromInfantry.BattleResultValues = battleResult.ListOfDefFromInfantry.ValuesWithTotal;
            DefenseBattleResultTable.DefenseFromCavalry.BattleResultValues = battleResult.ListOfDefFromCavalry.ValuesWithTotal;
            DefenseBattleResultTable.DefenseFromArchers.BattleResultValues = battleResult.ListOfDefFromArchers.ValuesWithTotal;

            DefenseBattleResultTable.UnitLost.BattleResultValues = battleResult.ListOfDefLostNumbers.ValuesWithTotal.SetColor(Colors.Red);
            DefenseBattleResultTable.UnitsLostWood.BattleResultValues = battleResult.DefWoodLost.ValuesWithTotal;
            DefenseBattleResultTable.UnitsLostClay.BattleResultValues = battleResult.DefClayLost.ValuesWithTotal;
            DefenseBattleResultTable.UnitsLostIron.BattleResultValues = battleResult.DefIronLost.ValuesWithTotal;
            DefenseBattleResultTable.UnitsLostResourceTotal.BattleResultValues = battleResult.DefTotalResourcesLost.ValuesWithTotal;

            DefenseBattleResultTable.UnitsLeft.BattleResultValues = battleResult.ListOfDefLeftNumbers.ValuesWithTotal;
        }

        private void SetImportedUnits()
        {
            _dialogService.ShowDialog(_unitImportWindowViewModel);
        }

        /// <summary>
        /// Will run the battle simulator based on the inputs and will then show the result in the BattleResult table.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunBattleSimulator()
        {
            BattleResult battleResult = GameData.SimulateBattle(_battleInputViewModel.ToBattleConfig());
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
        /// The viewmodel that displays the battle results for the defending side.
        /// </summary>
        public BattleResultTableViewModel DefenseBattleResultTable { get; set; } = new BattleResultTableViewModel();

        #endregion ViewModels

        #region Filters

        public bool IsAttackStrengthShown
        {
            get => _isAttackStrengthShown;
            set
            {
                _isAttackStrengthShown = value;
                AttackBattleResultTable.IsAttackStrengthShown = value;
                _settings.IsAttackStrengthShown = value;
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
                _settings.IsDefenseStrengthShown = value;
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
                _settings.IsResourcesLostShown = value;
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