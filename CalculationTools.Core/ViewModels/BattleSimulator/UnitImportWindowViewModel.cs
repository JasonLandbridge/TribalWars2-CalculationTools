using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using CalculationTools.Core.Base;

namespace CalculationTools.Core.BattleSimulator
{
    public class UnitImportWindowViewModel : BaseViewModel
    {
        private string _unitImportText;

        public string UnitImportText
        {
            get => _unitImportText;
            set
            {
                _unitImportText = value;
                OnPropertyChanged();
                ParseInput();
            }
        }

        public bool IsError { get; set; }

        public Visibility IsErrorVisible => IsError ? Visibility.Visible : Visibility.Hidden;
        public string ErrorMessage { get; set; }

        public BattleUnitPreviewViewModel BattleUnitPreviewViewModel { get; set; } = new BattleUnitPreviewViewModel();

        public UnitImportWindowViewModel()
        {

        }


        public void ParseInput()
        {
            // Filter out the decimal character from the string
            string untImportText = UnitImportText.Replace(".", "").Replace(",", "");

            // using the method 
            List<string> entries = untImportText.Split("\r\n").ToList();
            if (entries.Count != 39)
            {
                IsError = true;
                ErrorMessage = "The text input is invalid, make sure you selected all units in the scout report!";
                return;
            }

            // Convert string to int list
            List<int> rowData = new List<int>();
            foreach (string entry in entries)
            {
                if (int.TryParse(entry, out int result))
                {
                    rowData.Add(result);
                    continue;
                }
                rowData.Add(0);
            }


            ObservableCollection<BattleResultValueViewModel> unitAmountRow = new ObservableCollection<BattleResultValueViewModel>();
            ObservableCollection<BattleResultValueViewModel> unitLostRow = new ObservableCollection<BattleResultValueViewModel>();
            ObservableCollection<BattleResultValueViewModel> unitLeftRow = new ObservableCollection<BattleResultValueViewModel>();

            for (int i = 0; i < rowData.Count; i += 3)
            {
                unitAmountRow.Add(new BattleResultValueViewModel(rowData[i]));
                unitLostRow.Add(new BattleResultValueViewModel(rowData[i + 1]));
                unitLeftRow.Add(new BattleResultValueViewModel(rowData[i + 2]));
            }


            BattleUnitPreviewViewModel.UnitAmount.BattleResultValues = unitAmountRow;
            BattleUnitPreviewViewModel.UnitLost.BattleResultValues = unitLostRow;
            BattleUnitPreviewViewModel.UnitsLeft.BattleResultValues = unitLeftRow;

        }
    }
}
