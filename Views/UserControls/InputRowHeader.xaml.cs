using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TribalWars2_CalculationTools.Annotations;

namespace TribalWars2_CalculationTools.Views.UserControls
{
    /// <summary>
    /// Interaction logic for InputRowHeader.xaml
    /// </summary>
    public partial class InputRowHeader : UserControl
    {
        #region Fields

        public static readonly DependencyProperty ImagePathProperty =
            DependencyProperty.Register("ImagePath", typeof(string), typeof(InputRowHeader), new PropertyMetadata(SetImage));

        private static void SetImage(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is InputRowHeader inputRow && e.NewValue is string imagePath)
            {
                inputRow.HeaderImage.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
            }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(InputRowHeader), new PropertyMetadata(""));

        #endregion Fields

        #region Constructors

        public InputRowHeader()
        {
            InitializeComponent();

        }

        #endregion Constructors


        #region Properties

        public string ImagePath
        {
            get => (string)GetValue(ImagePathProperty);
            set => SetValue(ImagePathProperty, value);
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        #endregion Properties

    }
}
