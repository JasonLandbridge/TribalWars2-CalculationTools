using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using TribalWars2_CalculationTools.Models;
using TribalWars2_CalculationTools.ViewModels;

namespace TribalWars2_CalculationTools.Views
{
    public partial class MainWindow : Window
    {
        #region Constructors

        public MainWindowViewModel MainWindowViewModel { get; set; } = new MainWindowViewModel();
        public MainWindow()
        {
            this.DataContext = MainWindowViewModel;

            InitializeComponent();
        }

        #endregion Constructors

    }
}