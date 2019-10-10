using Ninject;
using nucs.JsonSettings;
using nucs.JsonSettings.Autosave;
using System;
using System.IO;

namespace CalculationTools.Core
{
    /// <summary>
    /// The IoC container for the Calculation.App
    /// </summary>
    public static class IoC
    {
        #region Properties

        /// <summary>
        /// The kernel for the IoC container
        /// </summary>
        public static IKernel Kernel { get; } = new StandardKernel();

        public static Settings Settings => Get<Settings>();
        #endregion Properties

        #region Methods

        /// <summary>
        /// Return a service from the IoC, of the specified type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }

        /// <summary>
        /// Sets up the IoC container, binds all the information required and is ready for use
        /// NOTE: Must be called as soon as your application starts up to ensure all
        ///       services can be found
        /// </summary>
        public static void Setup(IDialogService dialogService)
        {
            SetupSettings();

            // Bind all view models
            BindViewModel(dialogService);
            BindModels();
        }

        public static void SetupSettings()
        {
            Settings settings;
            string FileName = "Settings.json";
            string path = $"{AppDomain.CurrentDomain.BaseDirectory}{FileName}";

            if (File.Exists(path))
            {
                settings = JsonSettings.Load<Settings>(FileName).EnableAutosave();
            }
            else
            {
                settings = JsonSettings.Construct<Settings>(FileName).EnableAutosave();
                settings.SetDefaultValues();
            }

            Kernel.Bind<Settings>().ToConstant(settings);

        }

        /// <summary>
        /// Binds all singleton view models
        /// </summary>
        private static void BindViewModel(IDialogService dialogService)
        {

            // Binds to a single instance of Application view model
            Kernel.Bind<ApplicationViewModel>().ToConstant(new ApplicationViewModel());

            Kernel.Bind<MainWindowViewModel>().ToConstant(new MainWindowViewModel(dialogService));

            Kernel.Bind<BattleSimulatorViewModel>().ToConstant(new BattleSimulatorViewModel(dialogService));
        }

        private static void BindModels()
        {

        }


        public static ApplicationViewModel GetApplicationViewModel()
        {
            return Get<ApplicationViewModel>();
        }
        public static MainWindowViewModel GetMainWindowViewModel()
        {
            return Get<MainWindowViewModel>();
        }

        public static BattleSimulatorViewModel GetBattleSimulatorViewModel()
        {
            return Get<BattleSimulatorViewModel>();
        }

        #endregion Methods
    }
}
