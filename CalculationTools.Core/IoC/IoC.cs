using CalculationTools.Common;
using CalculationTools.Data;
using SimpleInjector;

namespace CalculationTools.Core
{
    /// <summary>
    /// The IoC container for the Calculation.App
    /// </summary>
    public static class IoC
    {
        #region Properties

        /// <summary>
        /// The IoC container
        /// </summary>
        public static Container Container { get; } = new Container();
        #endregion Properties

        #region Methods


        /// <summary>
        /// Sets up the IoC container, binds all the information required and is ready for use
        /// NOTE: Must be called as soon as your application starts up to ensure all
        ///       services can be found
        /// </summary>
        public static void Setup(IDialogService dialogService)
        {

            Container.Register<IDataManager, DataManager>(Lifestyle.Singleton);

            Container.Register<ApplicationViewModel>(Lifestyle.Singleton);
            Container.Register<MainWindowViewModel>(Lifestyle.Singleton);
            Container.Register<BattleSimulatorViewModel>(Lifestyle.Singleton);

            Container.Verify(VerificationOption.VerifyAndDiagnose);

        }


        public static ApplicationViewModel GetApplicationViewModel()
        {
            return Container.GetInstance<ApplicationViewModel>();
        }
        public static MainWindowViewModel GetMainWindowViewModel()
        {
            return Container.GetInstance<MainWindowViewModel>();
        }

        public static BattleSimulatorViewModel GetBattleSimulatorViewModel()
        {
            return Container.GetInstance<BattleSimulatorViewModel>();
        }

        #endregion Methods
    }
}
