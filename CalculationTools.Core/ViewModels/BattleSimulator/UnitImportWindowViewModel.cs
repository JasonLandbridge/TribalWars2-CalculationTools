using Castle.Core.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CalculationTools.Core
{
    public class UnitImportWindowViewModel : BaseViewModel, IDialogViewModel
    {
        private readonly BattleInputViewModel _battleInputViewModel;

        #region Constructors

        public UnitImportWindowViewModel(BattleInputViewModel battleInputViewModel)
        {
            _battleInputViewModel = battleInputViewModel;
            ResetCommand = new RelayCommand(ResetWindow);
            SaveCommand = new RelayCommand(SendToBattleInput);
            CloseCommand = new RelayCommand(() => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)));
        }

        private void ResetWindow()
        {
            _unitImportText = string.Empty;
            BattleUnitPreviewViewModel.Reset();
        }

        #endregion Constructors

        #region Events

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
        public void OnDialogOpen()
        {
            ResetCommand.Execute(null);
        }

        #endregion Events

        #region Properties

        public UnitSet UnitResultSet { get; set; } = new UnitSet();

        public bool IsAttackingImport { get; set; }

        public string HelpOneImagePath => $"/Resources/Img/help/village_units_selected.png";
        public string HelpTwoImagePath => $"/Resources/Img/help/scout_report_units_selected.png";


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
            if (UnitImportText.IsNullOrEmpty())
            {
                IsError = false;
                ResetWindow();
                return;
            }

            // Filter out the decimal character from the string
            string untImportText = UnitImportText.Replace(".", "").Replace(",", "");
            if (untImportText.EndsWith("\n"))
            {
                untImportText = untImportText.Remove(untImportText.Length - 2, 2);
            }



            List<string> entries = untImportText.Split("\r\n").ToList();


            if (entries.Count == 0 || entries.Count > 39)
            {
                IsError = true;
                ErrorMessage = "The text input is invalid, make sure you selected all units in the scout report!";
                return;
            }

            // If the entries are less than 13 than it is most likely an attacking units import
            if (entries.Count >= 1 && entries.Count <= 13)
            {
                IsAttackingImport = true;
                BattleUnitPreviewViewModel.Header = "Attacking Units";
                BattleUnitPreviewViewModel.UnitLostVisibility = false;
                BattleUnitPreviewViewModel.UnitsLeftVisibility = false;
            }

            if (entries.Count >= 14 && entries.Count <= 39)
            {
                IsAttackingImport = false;
                BattleUnitPreviewViewModel.Header = "Defending Units";
                BattleUnitPreviewViewModel.UnitLostVisibility = true;
                BattleUnitPreviewViewModel.UnitsLeftVisibility = true;

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

            if (IsAttackingImport)
            {
                for (int i = 0; i < GameData.UnitCount; i++)
                {
                    int value = 0;
                    if (i < rowData.Count)
                    {
                        value = rowData[i];
                    }

                    unitValueList.Add(value);
                    unitAmountRow.Add(new BattleResultValueViewModel(value));

                }

                BattleUnitPreviewViewModel.UnitAmount.BattleResultValues = unitAmountRow;

            }
            else
            {
                // Values are ordered top to bottom, and left to right when copied in
                for (int i = 0; i < rowData.Count; i += 3)
                {
                    int value = 0;
                    if (i < rowData.Count)
                    {
                        value = rowData[i];
                    }

                    unitValueList.Add(value);
                    unitAmountRow.Add(new BattleResultValueViewModel(value));

                    value = 0;
                    if (i + 1 < rowData.Count)
                    {
                        value = rowData[i + 1];
                    }
                    unitLostRow.Add(new BattleResultValueViewModel(value));


                    value = 0;
                    if (i + 2 < rowData.Count)
                    {
                        value = rowData[i + 2];
                    }
                    unitLeftRow.Add(new BattleResultValueViewModel(value));
                }

                BattleUnitPreviewViewModel.UnitAmount.BattleResultValues = unitAmountRow;
                BattleUnitPreviewViewModel.UnitLost.BattleResultValues = unitLostRow;
                BattleUnitPreviewViewModel.UnitsLeft.BattleResultValues = unitLeftRow;

            }

            IsError = false;

            UnitResultSet = new UnitSet(unitValueList);
        }

        public void SendToBattleInput()
        {
            ParseInput();
            if (IsAttackingImport)
            {
                _battleInputViewModel.LoadAtkUnits(UnitResultSet);
            }
            else
            {
                _battleInputViewModel.LoadDefUnits(UnitResultSet);
            }
            CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true));
        }

        #endregion Methods

        #region Fields

        private string _unitImportText;

        #endregion Fields
    }
}
