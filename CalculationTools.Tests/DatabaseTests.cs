using CalculationTools.Common;
using CalculationTools.Core;
using CalculationTools.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace CalculationTools.Tests
{
    public static class DatabaseTests
    {

        #region Properties

        private static DbContextOptions<CalculationToolsDBContext> dbContextOptions
        {
            get
            {
                const string connectionString = "Data Source=./DEBUG_CalculationToolsDB.db";
                var builder = new DbContextOptionsBuilder<CalculationToolsDBContext>();
                builder.UseSqlite(connectionString);
                return builder.Options;
            }
        }

        #endregion Properties

        #region Methods

        [Fact]
        public static void ShouldAddAnAccountInDBAndRetrieveIt()
        {
            using (var db = GetDbContext())
            {
                //Arrange
                string username = "Jantje";
                db.Accounts.Add(new Account
                {
                    Id = 0,
                    Username = username,
                    Password = "pietje",
                    IsConfirmed = false,
                    OnServerId = "nl",
                });

                // Act
                db.SaveChanges();

                //Assert
                Account account = db.Accounts.FirstOrDefault(x => x.Username == username);
                Assert.NotNull(account);
            }
        }

        [Fact]
        public static void ShouldUpdateLoginDataToDbCorrectly()
        {
            //Arrange
            IGameDataRepository _gameDataRepository = new GameDataRepository(IoC.AutoMapperContainer);
            _gameDataRepository.IsInUnitTestMode = true;
            _gameDataRepository.DbContextOptions = dbContextOptions;

            ILoginData loginData = MockData.GetLoginData();

            // Act
            _gameDataRepository.UpdateLoginData(loginData);

        }

        private static CalculationToolsDBContext GetDbContext()
        {
            return new CalculationToolsDBContext(dbContextOptions);
        }

        #endregion Methods
    }
}