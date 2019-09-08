using Caliburn.Micro;
using Xceed.Wpf.Toolkit;
using TribalWars2_CalculationTools.Models;

namespace TribalWars2_CalculationTools.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private CalculatorData _selectedCalculatorData;

        public ShellViewModel()
        {
            this.InputRowHeaderViewModel = new InputRowHeaderViewModel("ViewModel First Set Content", "");

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


        /// <summary>Gets the ViewModelFirst test control view model.</summary>
        /// <value>The ViewModelFirst test control view model.</value>
        public InputRowHeaderViewModel InputRowHeaderViewModel
        {
            get;
            private set;
        }
    }
}