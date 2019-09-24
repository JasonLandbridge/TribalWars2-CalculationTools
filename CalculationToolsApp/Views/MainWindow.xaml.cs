using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using TribalWars2_CalculationTools.Models;
using TribalWars2_CalculationTools.ViewModels;

namespace TribalWars2_CalculationTools.Views
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region Fields

        private BattleCalculatorInputViewModel _battleCalculatorInputViewModel = new BattleCalculatorInputViewModel();
        private CalculatorData _selectedCalculatorData;

        #endregion Fields

        #region Constructors

        public MainWindow()
        {
            CalculatorData.Add(new CalculatorData(BattleResultViewModel));
            SelectedCalculatorData = CalculatorData[0];

            InitializeComponent();

            this.DataContext = this;
            BattleInputTable.DataContext = BattleCalculatorInputViewModel;

            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
            }
            else
            {
                AttackBattleResultTable.DataContext = BattleResultViewModel;
                DefenseBattleResultTable.DataContext = BattleResultViewModel;
            }

            UpdateBattleCalculator(null, null);
        }

        #endregion Constructors

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        private BattleResultViewModel _battleResultViewModel = new BattleResultViewModel();
        public BattleResultViewModel BattleResultViewModel
        {
            get => _battleResultViewModel;
            set
            {
                _battleResultViewModel = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<CalculatorData> CalculatorData { get; set; } = new ObservableCollection<CalculatorData>();

        public BattleCalculatorInputViewModel BattleCalculatorInputViewModel
        {
            get => _battleCalculatorInputViewModel;
            set
            {
                _battleCalculatorInputViewModel = value;
                OnPropertyChanged();
            }
        }

        public CalculatorData SelectedCalculatorData
        {
            get => _selectedCalculatorData;
            set
            {
                _selectedCalculatorData = value;
                OnPropertyChanged();
            }
        }
        #endregion Properties

        #region Methods

        public void UpdateBattleCalculator(object sender, EventArgs e)
        {
            Debug.WriteLine("Battle Calculator updated!");
            SelectedCalculatorData.SimulateBattle(BattleCalculatorInputViewModel);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void InputTable_Loaded(object sender, RoutedEventArgs e)
        {
        }

        #endregion Methods
    }
}