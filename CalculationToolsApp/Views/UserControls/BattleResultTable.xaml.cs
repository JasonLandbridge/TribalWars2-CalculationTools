using System.Windows;
using System.Windows.Controls;
using ClassLibrary.Class;
using TribalWars2_CalculationTools.ViewModels;

namespace TribalWars2_CalculationTools.Views.UserControls
{

    public partial class BattleResultTable : UserControl
    {

        #region Fields

        public static readonly DependencyProperty BattleModifierProperty =
            DependencyProperty.Register("BattleModifier",
                typeof(int),
                typeof(BattleResultTable),
                new PropertyMetadata(0));

        public static readonly DependencyProperty BattleResultTableViewModelProperty =
            DependencyProperty.Register("BattleResultTableViewModel",
                typeof(BattleResultTableViewModel),
                typeof(BattleResultTable),
                new PropertyMetadata(SetProperty));

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header",
                typeof(string),
                typeof(BattleResultTable),
                new PropertyMetadata("No Title"));

        #endregion Fields

        #region Constructors

        public BattleResultTable()
        {
            InitializeComponent();
            UnitImageRow.ItemsSource = GameData.UnitImageList;
        }

        #endregion Constructors

        #region Properties

        public int BattleModifier
        {
            get => (int)GetValue(BattleModifierProperty);
            set => SetValue(BattleModifierProperty, value);
        }

        public BattleResultTableViewModel BattleResultTableViewModel
        {
            get => (BattleResultTableViewModel)GetValue(BattleResultTableViewModelProperty);
            set => SetValue(BattleResultTableViewModelProperty, value);
        }

        public string Header
        {
            get => (string)GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }



        #endregion Properties

        #region Methods

        private static void SetProperty(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BattleResultTable resultTable && e.NewValue is BattleResultTableViewModel viewModel)
            {
                resultTable.UnitsAmount.BattleResultValues = viewModel.UnitAmount;
                resultTable.UnitsLost.BattleResultValues = viewModel.UnitLost;
                resultTable.UnitsLeft.BattleResultValues = viewModel.UnitsLeft;

                resultTable.WallResult.Visibility = (viewModel.ShowWallResult ? Visibility.Visible : Visibility.Hidden);
                resultTable.WallResult.WallResult.Content = viewModel.WallResult;
            }

        }

        #endregion Methods
    }
}
