﻿using AutoMapper;
using CalculationTools.Common;
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
        /// The IoC container used to register all dependencies.
        /// </summary>
        public static Container Container { get; } = new Container();


        public static IMapper AutoMapperContainer => new Mapper(AutoMapperConfig.RegisterMappings());

        #endregion Properties

        #region Methods


        public static void Setup()
        {

            Container.Register<IPlayerData, PlayerData>(Lifestyle.Singleton);
            Container.Register<IDataManager, DataManager>(Lifestyle.Singleton);
            Container.Register<ISocketManager, SocketManager>(Lifestyle.Singleton);
            Container.Register<IMessageHandling, MessageHandling>(Lifestyle.Singleton);

            Container.Register<IGameDataRepository, GameDataRepository>(Lifestyle.Singleton);

            // Injectable service
            IMapper mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            Container.RegisterInstance(typeof(IMapper), AutoMapperContainer);

        }


        #endregion Methods
    }
}
