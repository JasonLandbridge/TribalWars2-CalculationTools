using Ninject;
using nucs.JsonSettings;
using nucs.JsonSettings.Autosave;

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

        /// <summary>
        /// All settings are stored here and auto saved on change.
        /// </summary>
        public static Settings Settings { get; } = JsonSettings.Load<Settings>("Settings.json").EnableAutosave();
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
            // Bind all view models
            BindViewModel(dialogService);
            BindModels();
        }

        /// <summary>
        /// Binds all singleton view models
        /// </summary>
        private static void BindViewModel(IDialogService dialogService)
        {

            // Binds to a single instance of Application view model
            Kernel.Bind<ApplicationViewModel>().ToConstant(new ApplicationViewModel());

            Kernel.Bind<BattleSimulatorViewModel>().ToConstant(new BattleSimulatorViewModel(dialogService));
        }

        private static void BindModels()
        {
            Kernel.Bind<Settings>().ToConstant(Settings);
        }


        public static ApplicationViewModel GetApplicationViewModel()
        {
            return Get<ApplicationViewModel>();
        }

        public static BattleSimulatorViewModel GetBattleSimulatorViewModel()
        {
            return Get<BattleSimulatorViewModel>();
        }

        #endregion Methods
    }
}
