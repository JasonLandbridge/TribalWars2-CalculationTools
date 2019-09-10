using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using TribalWars2_CalculationTools.Annotations;
using TribalWars2_CalculationTools.Class;
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

            CalculatorData.Add(new CalculatorData());
            SelectedCalculatorData = CalculatorData[0];

            this.InputChurch.Title = "Church";
            this.InputChurch.ImagePath = "/Resources/Img/buildings/buildings_church.png";
            //InputMorale = new InputRowHeaderViewModel("Morale", "/Resources/Img/info/info_morale.png");
            //InputLuck = new InputRowHeaderViewModel("Luck", "/Resources/Img/info/info_luck.png");
            //InputWall = new InputRowHeaderViewModel("Wall", "/Resources/Img/buildings/buildings_wall.png");
            //InputNightBonus = new InputRowHeaderViewModel("Night bonus", "/Resources/Img/info/info_nightbonus.png");


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
    }
}
