using AutoUpdaterDotNET;
using CalculationTools.Core;
using SimpleInjector;
using System;
using System.Diagnostics;
using System.Windows;

namespace CalculationTools.App
{

    /// <summary>
    /// Interaction logic for ApplicationStart.xaml
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

            RegisterDependencies();

            try
            {
                // Show the main window
                Current.MainWindow = IoC.Container.GetInstance<MainWindow>();
                Current.MainWindow.Show();
            }
            catch (Exception ex)
            {
                //Log the exception and exit
                Debug.WriteLine(ex.Message);
            }

            //Check for updates
            //MessageBox.Show(typeof(ApplicationStart).Assembly.GetName().Version.ToString());
            AutoUpdate();
        }


        private void RegisterDependencies()
        {
            // Register all dialog windows
            DialogService dialogService = new DialogService(MainWindow);

            dialogService.Register<UnitImportWindowViewModel, UnitImportWidow>();
            dialogService.Register<ConnectionWindowViewModel, ConnectionWindow>();
            dialogService.Register<SettingsWindowViewModel, SettingsWindow>();

            // Register dependencies
            IoC.Container.Register<IDialogService>(() => dialogService, Lifestyle.Singleton);

            IoC.Container.Register<ApplicationViewModel>(Lifestyle.Singleton);

            IoC.Container.Register<MainWindow>(Lifestyle.Singleton);
            IoC.Container.Register<MainWindowViewModel>(Lifestyle.Singleton);

            IoC.Container.Register<SettingsWindow>();
            IoC.Container.Register<SettingsWindowViewModel>(Lifestyle.Singleton);

            IoC.Container.Register<UnitImportWidow>();
            IoC.Container.Register<UnitImportWindowViewModel>(Lifestyle.Singleton);

            IoC.Container.Register<ConnectionWindow>();
            IoC.Container.Register<ConnectionWindowViewModel>(Lifestyle.Singleton);

            IoC.Container.Register<BattleSimulatorView>(Lifestyle.Singleton);
            IoC.Container.Register<BattleSimulatorViewModel>(Lifestyle.Singleton);

            IoC.Container.Register<BattleInput>(Lifestyle.Singleton);
            IoC.Container.Register<BattleInputViewModel>(Lifestyle.Singleton);

            ApplicationCore.OnStartUp();

            IoC.Container.Verify(VerificationOption.VerifyAndDiagnose);
        }

        public void AutoUpdate()
        {
            AutoUpdater.DownloadPath = Environment.CurrentDirectory;
            AutoUpdater.Start("https://raw.githubusercontent.com/JasonLandbridge/TribalWars2-CalculationTools/master/CalculationTools.ApplicationStart/Resources/Updates/AutoUpdater.xml");

        }


    }
}
