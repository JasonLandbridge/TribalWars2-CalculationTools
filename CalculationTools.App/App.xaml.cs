using AutoUpdaterDotNET;
using CalculationTools.Core;
using System;
using System.Windows;
using SimpleInjector;

namespace CalculationTools.App
{

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        /// <summary>
        /// Custom startup so we load our IoC immediately before anything else.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            // Let the base application do what it needs
            base.OnStartup(e);

            // Register all dialog windows
            DialogService dialogService = new DialogService(MainWindow);

            dialogService.Register<UnitImportWindowViewModel, UnitImportWidow>();
            dialogService.Register<ConnectionWindowViewModel, ConnectionWindow>();
            dialogService.Register<SettingsWindowViewModel, SettingsWindow>();

            IoC.Container.Register<IDialogService>(() => dialogService, Lifestyle.Singleton);
            ApplicationCore.OnStartUp(dialogService);

            // Show the main window
            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();

            //Check for updates
            //MessageBox.Show(typeof(App).Assembly.GetName().Version.ToString());
            AutoUpdate();
        }

        public void AutoUpdate()
        {
            AutoUpdater.DownloadPath = Environment.CurrentDirectory;
            AutoUpdater.Start("https://raw.githubusercontent.com/JasonLandbridge/TribalWars2-CalculationTools/master/CalculationTools.App/Resources/Updates/AutoUpdater.xml");

        }


    }
}
