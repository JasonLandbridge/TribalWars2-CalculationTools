using AutoMapper;
using CalculationTools.Common;
using CalculationTools.Data;
using CalculationTools.WebSocket;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using System;

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
        #endregion Properties

        #region Methods


        public static void Setup()
        {

            Container.Register<IPlayerData, PlayerData>(Lifestyle.Singleton);
            Container.Register<IDataManager, DataManager>(Lifestyle.Singleton);
            Container.Register<ISocketManager, SocketManager>(Lifestyle.Singleton);

            Container.Register<IGameDataRepository, GameDataRepository>(Lifestyle.Singleton);


            // Injectable service
            IMapper mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            Container.RegisterInstance(typeof(IMapper), mapper);

        }


        #endregion Methods
    }
}
