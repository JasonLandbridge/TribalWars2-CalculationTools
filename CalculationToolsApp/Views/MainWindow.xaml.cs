using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using TribalWars2_CalculationTools.Annotations;
using TribalWars2_CalculationTools.Class;
using TribalWars2_CalculationTools.Class.Units;
using TribalWars2_CalculationTools.Models;
using TribalWars2_CalculationTools.ViewModels;

namespace TribalWars2_CalculationTools.Views
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region Fields

        private InputCalculatorData _inputCalculatorData = new InputCalculatorData();
        private CalculatorData _selectedCalculatorData;

        #endregion Fields

        #region Constructors

        public MainWindow()
        {
            // TODO make a list


            CalculatorData.Add(new CalculatorData(BattleResultViewModel));
            SelectedCalculatorData = CalculatorData[0];

            InitializeComponent();
            this.DataContext = this;

            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
            }
            else
            {
                InputTable.DataContext = this;
                InputTable.InputCalculatorData = InputCalculatorData;

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

        public InputCalculatorData InputCalculatorData
        {
            get => _inputCalculatorData;
            set
            {
                _inputCalculatorData = value;
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
            SelectedCalculatorData.SimulateBattle(InputCalculatorData);
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