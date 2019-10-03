using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using CalculationTools.Core;

namespace CalculationTools.App
{
    /// <summary>
    /// Interaction logic for PaladinWeaponSelect.xaml
    /// </summary>
    public partial class PaladinWeaponSelect : UserControl
    {
        public int WeaponLevel
        {
            get => (int)GetValue(WeaponLevelProperty);
            set => SetValue(WeaponLevelProperty, value);
        }

        // Using a DependencyProperty as the backing store for WeaponLevel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WeaponLevelProperty =
            DependencyProperty.Register("WeaponLevel", typeof(int), typeof(PaladinWeaponSelect), new PropertyMetadata(0));



        public BaseWeapon WeaponType
        {
            get => (BaseWeapon)GetValue(WeaponTypeProperty);
            set => SetValue(WeaponTypeProperty, value);
        }

        // Using a DependencyProperty as the backing store for WeaponType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WeaponTypeProperty =
            DependencyProperty.Register("WeaponType", typeof(BaseWeapon), typeof(PaladinWeaponSelect), new PropertyMetadata(SetWeaponType));

        private static void SetWeaponType(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PaladinWeaponSelect weaponSelect && e.NewValue is BaseWeapon baseWeapon)
            {
                weaponSelect.InputWeaponType.SelectedValue = baseWeapon.UnitId;
            }
        }

        #region Constructors

        public PaladinWeaponSelect()
        {
            InitializeComponent();
            InputWeaponType.ItemsSource = GameData.WeaponOptions;
            InputWeaponLevel.ItemsSource = new List<int> { 1, 2, 3 };
        }

        #endregion Constructors


        #region Properties


        #endregion Properties
        public event EventHandler ValueChanged;

        private void WeaponLevel_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }

        private void WeaponName_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ValueChanged?.Invoke(this, EventArgs.Empty);

        }
    }
}
