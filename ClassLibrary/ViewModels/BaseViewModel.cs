using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ClassLibrary.ViewModels
{
    /// <summary>
    /// A base view model that fires Property Changed Events as needed
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
