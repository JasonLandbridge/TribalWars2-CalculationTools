using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CalculationTools.Common
{
    /// <summary>
    /// A base view model that fires Property Changed Events as needed
    /// </summary>
    public class BasePropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
