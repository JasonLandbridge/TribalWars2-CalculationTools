using System;

namespace CalculationTools.Core
{
    public interface IDialogViewModel
    {
        event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
        void OnDialogOpen();
    }
}