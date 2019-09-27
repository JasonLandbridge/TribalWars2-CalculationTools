using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using ClassLibrary.ViewModels;
using TribalWars2_CalculationTools.Models;
using TribalWars2_CalculationTools.ViewModels.UserControls;

namespace TribalWars2_CalculationTools.ViewModels
{
    /// <summary>
    /// The viewmodel for view MainWindow
    /// </summary>
    public class MainWindowViewModel : BaseViewModel
    {
        #region Properties

        /// <summary>
        /// The viewmodel for the input fields of the battle simulator
        /// </summary>
        public BattleInputTableViewModel BattleCalculatorInputViewModel { get; set; } = new BattleInputTableViewModel();

        /// <summary>
        /// The result is shown with this ViewModel
        /// </summary>
        public BattleResultViewModel BattleResultViewModel { get; set; } = new BattleResultViewModel();

        public ObservableCollection<CalculatorData> CalculatorData { get; set; } = new ObservableCollection<CalculatorData>();
        public CalculatorData SelectedCalculatorData { get; set; }
        #endregion Properties


        #region Constructors

        public MainWindowViewModel()
        {
            // TODO create a list of different battle calculations that can be loaded.
            CalculatorData.Add(new CalculatorData(BattleResultViewModel));
            SelectedCalculatorData = CalculatorData[0];

            BattleCalculatorInputViewModel.PropertyChanged += UpdateBattleCalculator;



            UpdateBattleCalculator(null, null);

        }


        #endregion Constructors

        #region Methods
        private void UpdateBattleCalculator(object sender, PropertyChangedEventArgs e)
        {
            Debug.WriteLine("Battle Calculator updated!");
            SelectedCalculatorData.SimulateBattle(BattleCalculatorInputViewModel);
        }
        #endregion Methods
    }
}
