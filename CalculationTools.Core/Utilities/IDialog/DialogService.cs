using System;
using System.Collections.Generic;
using System.Windows;

namespace CalculationTools.Core
{
    public class DialogService : IDialogService
    {
        private readonly Window owner;

        public DialogService(Window owner)
        {
            this.owner = owner;
            Mappings = new Dictionary<Type, Type>();
        }

        public IDictionary<Type, Type> Mappings { get; }

        public void Register<TViewModel, TView>() where TViewModel : IDialogViewModel
                                                  where TView : IDialog
        {
            if (Mappings.ContainsKey(typeof(TViewModel)))
            {
                throw new ArgumentException($"Type {typeof(TViewModel)} is already mapped to type {typeof(TView)}");
            }

            Mappings.Add(typeof(TViewModel), typeof(TView));
        }

        public event EventHandler OnDialogOpen;

        public bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : IDialogViewModel
        {
            Type viewType = Mappings[typeof(TViewModel)];

            // Instead of re-creating a new view every time, take the one from the IoC container.
            IDialog dialog = (IDialog)IoC.Container.GetInstance(viewType);

            EventHandler<DialogCloseRequestedEventArgs> handler = null;

            handler = (sender, e) =>
            {
                viewModel.CloseRequested -= handler;

                if (e.DialogResult.HasValue)
                {
                    try
                    {
                        dialog.DialogResult = e.DialogResult;

                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                    }
                }
                else
                {
                    dialog.Close();
                }

            };

            viewModel.CloseRequested += handler;

            dialog.DataContext = viewModel;
            dialog.Owner = owner;

            // Run the OnDialogOpen method on the viewmodel
            viewModel.OnDialogOpen();

            return dialog.ShowDialog();
        }


    }
}