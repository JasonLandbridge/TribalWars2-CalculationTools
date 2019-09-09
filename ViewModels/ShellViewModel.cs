using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using TribalWars2_CalculationTools.Class;
using Xceed.Wpf.Toolkit;
using TribalWars2_CalculationTools.Models;

namespace TribalWars2_CalculationTools.ViewModels
{
    public class ShellViewModel : Screen, INotifyPropertyChanged
    {
        private CalculatorData _selectedCalculatorData;

        private InputCalculatorClass _inputCalculatorClass = new InputCalculatorClass();
        public ShellViewModel()
        {
            CalculatorData.Add(new CalculatorData());
            SelectedCalculatorData = CalculatorData[0];
        }

        public BindableCollection<CalculatorData> CalculatorData { get; set; } = new BindableCollection<CalculatorData>();

        public CalculatorData SelectedCalculatorData
        {
            get => _selectedCalculatorData;
            set
            {
                _selectedCalculatorData = value;
                NotifyOfPropertyChange(() => SelectedCalculatorData);
            }
        }

        public InputCalculatorClass InputCalculatorData
        {
            get => _inputCalculatorClass;
            set
            {
                _inputCalculatorClass = value;
                Debug.WriteLine("Property updated!");

                NotifyOfPropertyChange(() => InputCalculatorData);
            }
        }

        private void UpdateProperty(object sender, SelectionChangedEventArgs e)
        {
        }

        public InputRowHeaderViewModel InputChurch
        {
            get;
            private set;
        } = new InputRowHeaderViewModel("Church", "/Resources/Img/buildings/buildings_church.png");
        public InputRowHeaderViewModel InputMorale
        {
            get;
            private set;
        } = new InputRowHeaderViewModel("Morale", "/Resources/Img/info/info_morale.png");
        public InputRowHeaderViewModel InputLuck
        {
            get;
            private set;
        } = new InputRowHeaderViewModel("Luck", "/Resources/Img/info/info_luck.png");
        public InputRowHeaderViewModel InputWall
        {
            get;
            private set;
        } = new InputRowHeaderViewModel("Wall", "/Resources/Img/buildings/buildings_wall.png");
        public InputRowHeaderViewModel InputNightBonus
        {
            get;
            private set;
        } = new InputRowHeaderViewModel("Night bonus", "/Resources/Img/info/info_nightbonus.png");


    }
}