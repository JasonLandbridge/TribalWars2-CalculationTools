using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using TribalWars2_CalculationTools.Annotations;

namespace TribalWars2_CalculationTools.Views.UserControls
{
    /// <summary>
    /// Interaction logic for InputRowHeader.xaml
    /// </summary>
    public partial class InputRowHeader : UserControl, INotifyPropertyChanged
    {
        #region Fields

        public static readonly DependencyProperty ImagePathProperty =
            DependencyProperty.Register("ImagePath", typeof(string), typeof(InputRowHeader), new PropertyMetadata(""));

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(InputRowHeader), new PropertyMetadata(""));

        private string _imagePath = "";

        private string _title = "Default ViewModel first caption";

        #endregion Fields

        #region Constructors

        public InputRowHeader()
        {
            InitializeComponent();
        }
        public InputRowHeader(string title, string imagePath)
        {
            this._title = title;
            this._imagePath = imagePath;
        }

        #endregion Constructors

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        public string ImagePath
        {
            get => (string)GetValue(ImagePathProperty);
            set
            {
                SetValue(ImagePathProperty, value);
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set
            {
                SetValue(TitleProperty, value);
                OnPropertyChanged();
            }
        }

        #endregion Properties

        #region Methods

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion Methods
    }
}
