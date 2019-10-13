using System;

namespace CalculationTools.Core
{
    public class DialogCloseRequestedEventArgs : EventArgs
    {
        public DialogCloseRequestedEventArgs(bool? dialogResult = null)
        {
            DialogResult = dialogResult;
        }

        public bool? DialogResult { get; }
    }
}