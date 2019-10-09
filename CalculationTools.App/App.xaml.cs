using AutoUpdaterDotNET;
using CalculationTools.Core;
using CalculationTools.Core.BattleSimulator;
using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Reflection;
using System.Windows;

namespace CalculationTools.App
{

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(App));

        /// <summary>
        /// Custom startup so we load our IoC immediately before anything else.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            // Let the base application do what it needs
            base.OnStartup(e);

            // Setup Log4Net
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            Log.Info("        =============  Started Logging  =============        ");

            IDialogService dialogService = new DialogService(MainWindow);
            dialogService.Register<UnitImportWindowViewModel, UnitImportWidow>();

            // Setup the IoC
            IoC.Setup(dialogService);


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
