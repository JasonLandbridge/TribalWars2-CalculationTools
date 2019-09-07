using Caliburn.Micro;
using TribalWars2_CalculationTools.Models;

namespace TribalWars2_CalculationTools.ViewModels
{
    public class ShellViewModel : Screen
    {
        private CalculatorData _selectedCalculatorData;
        private BindableCollection<CalculatorData> _calculatorData = new BindableCollection<CalculatorData>();

        public ShellViewModel()
        {
            CalculatorData.Add(new CalculatorData
            {
                AtkSpear = 999,
                DefSpear = 888
            });
            SelectedCalculatorData = CalculatorData[0];
        }

        public BindableCollection<CalculatorData> CalculatorData
        {
            get => _calculatorData;
            set => _calculatorData = value;
        }

        public CalculatorData SelectedCalculatorData
        {
            get => _selectedCalculatorData;
            set
            {
                _selectedCalculatorData = value;
                NotifyOfPropertyChange(() => SelectedCalculatorData);
            }
        }
    }
}