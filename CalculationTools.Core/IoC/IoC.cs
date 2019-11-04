﻿using AutoMapper;
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
            Container.Register<CalculationToolsDBContext>(Lifestyle.Scoped);


            // Injectable service
            IMapper mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            Container.RegisterInstance(typeof(IMapper), mapper);

            // SetupDatabaseDI();
        }

        public static void SetupDatabaseDI()
        {
            // See Simple Injector integration guide
            // https://simpleinjector.readthedocs.io/en/latest/servicecollectionintegration.html
            var services = new ServiceCollection();
            services.AddDbContextPool<CalculationToolsDBContext>(options =>
                {
                    options.UseSqlite("Data Source=./CalculationToolsDB.db");
                }).AddSimpleInjector(Container);

            // Save yourself pain and agony and always use "validateScopes: true"
            IServiceProvider provider = services.BuildServiceProvider(true).UseSimpleInjector(Container);

            // Ensures framework components are cross wired.
            provider.UseSimpleInjector(Container);
        }

        #endregion Methods
    }
}
