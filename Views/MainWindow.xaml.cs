using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using TribalWars2_CalculationTools.Annotations;
using TribalWars2_CalculationTools.Class;
using TribalWars2_CalculationTools.Models;

namespace TribalWars2_CalculationTools.Views
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private CalculatorData _selectedCalculatorData;

        private InputCalculatorData _inputCalculatorData = new InputCalculatorData();

        public MainWindow()
        {
            // TODO make a list
            CalculatorData.Add(new CalculatorData());
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
            }

            //PresentationTraceSources.TraceLevel=High
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public ObservableCollection<CalculatorData> CalculatorData { get; set; } = new ObservableCollection<CalculatorData>();

        public CalculatorData SelectedCalculatorData
        {
            get => _selectedCalculatorData;
            set
            {
                _selectedCalculatorData = value;
                OnPropertyChanged();
            }
        }

        public InputCalculatorData InputCalculatorData
        {
            get => _inputCalculatorData;
            set
            {
                _inputCalculatorData = value;
                Debug.WriteLine("Property updated!");
                OnPropertyChanged();

            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        public void UpdateBattleCalculator(object sender, EventArgs e)
        {
            Debug.WriteLine("Battle Calculator updated!");
            SelectedCalculatorData.SimulateBattle(InputCalculatorData);
        }

        private void InputTable_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
