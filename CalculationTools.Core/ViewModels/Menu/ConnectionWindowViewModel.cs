using System;
using System.Windows;
using System.Windows.Input;

namespace CalculationTools.Core
{
    public class ConnectionWindowViewModel : BaseViewModel, IDialogViewModel
    {
        #region Constructors

        public ConnectionWindowViewModel()
        {
        }

        #endregion Constructors

        #region Events

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
        public void OnDialogOpen()
        {

        }

        #endregion Events

        #region Properties

        public UnitSet UnitResultSet { get; set; } = new UnitSet();

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

        #endregion Properties

        #region Methods

        #endregion Methods

        #region Fields

        private string _unitImportText;

        #endregion Fields
    }
}
