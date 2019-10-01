using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using CalculationTools.Core.Base;
using CalculationTools.Core.BattleSimulator;

namespace CalculationTools.Core
{
    public class BattleSimulatorViewModel : BaseViewModel
    {
        #region Properties

        #region BattleResult

        public int AtkBattleModifier { get; set; }
        public int DefBattleModifier { get; set; }

        #endregion

        #region ViewModels

        /// <summary>
        /// The viewmodel for the input fields of the battle simulator.
        /// </summary>
        public BattleSimulatorInputViewModel BattleSimulatorInputViewModel { get; set; } = new BattleSimulatorInputViewModel();

        /// <summary>
        /// The viewmodel that displays the battle results for the attacking side.
        /// </summary>
        public BattleResultTableViewModel AttackBattleResultTable { get; set; } = new BattleResultTableViewModel();
        /// <summary>
        /// The viewmodel that displays the battle results for the defending side.
        /// </summary>
        public BattleResultTableViewModel DefenseBattleResultTable { get; set; } = new BattleResultTableViewModel();


        #endregion

        #endregion Properties


        #region Constructors

        public BattleSimulatorViewModel()
        {
            SetupBattleResult();
            BattleSimulatorInputViewModel.PropertyChanged += RunBattleSimulator;

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

            BattleSimulatorInputViewModel.LoadUnits(atkUnits, defUnits);
        }

        /// <summary>
        /// Will run the battle simulator based on the inputs and will then show the result in the BattleResult table.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunBattleSimulator(object sender, PropertyChangedEventArgs e)
        {
            Debug.WriteLine("Battle Calculator updated!");
            BattleResult battleResult = GameData.SimulateBattle(BattleSimulatorInputViewModel);
            UpdateBattleResult(battleResult);
        }

        #endregion Constructors


        #region Methods

        public void SetupBattleResult()
        {
            AttackBattleResultTable.Header = "Attacking Units";
            DefenseBattleResultTable.Header = "Defending Units";

            AttackBattleResultTable.UnitAmount.Header = "Amount";
            AttackBattleResultTable.UnitLost.Header = "Losses";
            AttackBattleResultTable.UnitsLeft.Header = "Survivors";
            AttackBattleResultTable.ShowWallResult = false;

            DefenseBattleResultTable.UnitAmount.Header = "Amount";
            DefenseBattleResultTable.UnitLost.Header = "Losses";
            DefenseBattleResultTable.UnitsLeft.Header = "Survivors";
            DefenseBattleResultTable.WallResult.Header = "Wall";
            DefenseBattleResultTable.WallResult.BattleResultValues = null;
            DefenseBattleResultTable.ShowWallResult = true;
        }

        public void UpdateBattleResult(BattleResult battleResult)
        {

            AttackBattleResultTable.BattleModifier = battleResult.AtkBattleModifier.ToString();
            DefenseBattleResultTable.BattleModifier = battleResult.DefBattleModifier.ToString();

            DefenseBattleResultTable.WallResult.Content = battleResult.WallResult;

            AttackBattleResultTable.UnitAmount.BattleResultValues = new ObservableCollection<BattleResultValueViewModel>(battleResult.ListOfAtkNumbers);
            AttackBattleResultTable.UnitLost.BattleResultValues = new ObservableCollection<BattleResultValueViewModel>(battleResult.ListOfAtkLostNumbers);
            AttackBattleResultTable.UnitsLeft.BattleResultValues = new ObservableCollection<BattleResultValueViewModel>(battleResult.ListOfAtkLeftNumbers);

            DefenseBattleResultTable.UnitAmount.BattleResultValues = new ObservableCollection<BattleResultValueViewModel>(battleResult.ListOfDefNumbers);
            DefenseBattleResultTable.UnitLost.BattleResultValues = new ObservableCollection<BattleResultValueViewModel>(battleResult.ListOfDefLostNumbers);
            DefenseBattleResultTable.UnitsLeft.BattleResultValues = new ObservableCollection<BattleResultValueViewModel>(battleResult.ListOfDefLeftNumbers);

        }

        #endregion Methods
    }
}
