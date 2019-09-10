using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using TribalWars2_CalculationTools.Annotations;
using TribalWars2_CalculationTools.Class;
using TribalWars2_CalculationTools.Class.Utilities;
using TribalWars2_CalculationTools.Models;

namespace TribalWars2_CalculationTools.Views
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private CalculatorData _selectedCalculatorData;

        private InputCalculatorClass _inputCalculatorClass = new InputCalculatorClass();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            // TODO make a list
            CalculatorData.Add(new CalculatorData());
            SelectedCalculatorData = CalculatorData[0];

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

        public InputCalculatorClass InputCalculatorData
        {
            get => _inputCalculatorClass;
            set
            {
                _inputCalculatorClass = value;
                Debug.WriteLine("Property updated!");
                OnPropertyChanged();

            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        public void UpdateBattleCalculator()
        {
            Debug.WriteLine("Battle Calculator updated!");
            SelectedCalculatorData.SimulateBattle(InputCalculatorData);
        }

        private void InputChangedInteger(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            UpdateBattleCalculator();
        }

        private void InputChangedCheckBox(object sender, RoutedEventArgs e)
        {
            UpdateBattleCalculator();
        }


    }
}
