using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CalculationTools.Core.Base;

namespace CalculationTools.Core.BattleSimulator
{
    public class UnitImportWindowViewModel : BaseViewModel, IDialogRequestClose
    {
        #region Constructors

        public UnitImportWindowViewModel()
        {
            ResetCommand = new RelayCommand(() => { UnitImportText = string.Empty; });
            SaveCommand = new RelayCommand(SendToBattleInput);
            CloseCommand = new RelayCommand(() => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)));
        }

        #endregion Constructors

        #region Events

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        #endregion Events

        #region Properties

        public UnitSet UnitResultSet { get; set; }

        #region ViewModels
        public BattleUnitPreviewViewModel BattleUnitPreviewViewModel { get; set; } = new BattleUnitPreviewViewModel();

        #endregion


        #region Commands

        /// <summary>
        /// Close the dialog window without changing anything
        /// </summary>
        public ICommand CloseCommand { get; }

        /// <summary>
        /// Reset the input fields
        /// </summary>
        public ICommand ResetCommand { get; }

        /// <summary>
        /// Confirm and send the UnitSet to the BattleSimulator
        /// </summary>
        public ICommand SaveCommand { get; }

        #endregion

        #region ErrorHandeling

        public string ErrorMessage { get; set; }
        public bool IsError { get; set; }
        public Visibility IsErrorVisible => IsError ? Visibility.Visible : Visibility.Hidden;

        #endregion
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

        #endregion Properties

        #region Methods

        public void ParseInput()
        {
            if (UnitImportText == null)
            {
                return;
            }

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


            List<BattleResultValueViewModel> unitAmountRow = new List<BattleResultValueViewModel>();
            List<int> unitValueList = new List<int>();
            List<BattleResultValueViewModel> unitLostRow = new List<BattleResultValueViewModel>();
            List<BattleResultValueViewModel> unitLeftRow = new List<BattleResultValueViewModel>();

            for (int i = 0; i < rowData.Count; i += 3)
            {
                unitValueList.Add(rowData[i]);
                unitAmountRow.Add(new BattleResultValueViewModel(rowData[i]));
                unitLostRow.Add(new BattleResultValueViewModel(rowData[i + 1]));
                unitLeftRow.Add(new BattleResultValueViewModel(rowData[i + 2]));
            }


            BattleUnitPreviewViewModel.UnitAmount.BattleResultValues = unitAmountRow;
            BattleUnitPreviewViewModel.UnitLost.BattleResultValues = unitLostRow;
            BattleUnitPreviewViewModel.UnitsLeft.BattleResultValues = unitLeftRow;

            UnitResultSet = new UnitSet(unitValueList);
        }

        public void SendToBattleInput()
        {
            ParseInput();
            IoC.GetBattleSimulatorViewModel().BattleSimulatorInputViewModel.LoadDefUnits(UnitResultSet);
            CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true));
        }

        #endregion Methods

        #region Fields

        private string _unitImportText;

        #endregion Fields
    }
}
