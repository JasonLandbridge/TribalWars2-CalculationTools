using CalculationTools.Common;
using CalculationTools.Common.Data;
using CalculationTools.Data;
using CalculationTools.WebSocket;
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
        public static void Setup()
        {

            //IMySettings settings = new ConfigurationBuilder<IMySettings>().UseAppConfig().Build();

            //Container.Register<IMySettings>(() => settings);

            Container.Register<IPlayerData, PlayerData>(Lifestyle.Singleton);
            Container.Register<IDataManager, DataManager>(Lifestyle.Singleton);
            Container.Register<ISocketManager, SocketManager>(Lifestyle.Singleton);

        }

        public static BattleSimulatorViewModel GetBattleSimulatorViewModel()
        {
            return Container.GetInstance<BattleSimulatorViewModel>();
        }

        public static IDataManager GetDataManager()
        {
            return Container.GetInstance<IDataManager>();
        }


        public static SettingsWindowViewModel SettingsWindowViewModel => Container.GetInstance<SettingsWindowViewModel>();

        #endregion Methods
    }
}
