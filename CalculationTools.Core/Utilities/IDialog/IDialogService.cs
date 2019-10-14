namespace CalculationTools.Core
{
    public interface IDialogService
    {
        void Register<TViewModel, TView>() where TViewModel : IDialogViewModel
                                           where TView : IDialog;

        bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : IDialogViewModel;
    }
}