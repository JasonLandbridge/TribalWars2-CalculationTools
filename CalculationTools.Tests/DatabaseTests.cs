using CalculationTools.Common;
using CalculationTools.Core;
using CalculationTools.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
        public static void ShouldStoreAllIncomingDataCorrectly()
        {
            ShouldUpdateLoginDataToDbCorrectly();

            ShouldInsertAndUpdateVillages();
        }

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
            _gameDataRepository.DeleteDB();



            ILoginData loginData = MockData.GetLoginData();

            // Act
            _gameDataRepository.UpdateLoginData(loginData);


            using (var db = GetDbContext())
            {
                var characterList = db.Characters.ToList();

                Assert.NotNull(characterList.FirstOrDefault(
                    x => x.CharacterId == 999888 &&
                         x.CharacterName == "FAKENAME" &&
                         x.WorldId == "nl33"));

                Assert.NotNull(characterList.FirstOrDefault(
                    x => x.CharacterId == 999888 &&
                         x.CharacterName == "FAKENAME" &&
                         x.WorldId == "nl35"));

                Assert.NotNull(characterList.FirstOrDefault(
                    x => x.CharacterId == 999888 &&
                         x.CharacterName == "FAKENAME" &&
                         x.WorldId == "nl37"));

            }
        }

        [Fact]
        public static void ShouldInsertAndUpdateVillages()
        {
            //Arrange
            IGameDataRepository _gameDataRepository = new GameDataRepository(IoC.AutoMapperContainer);
            _gameDataRepository.IsInUnitTestMode = true;
            _gameDataRepository.DbContextOptions = dbContextOptions;

            List<IVillage> villages = MockData.GetCharacterVillages();

            //Act
            bool result1 = _gameDataRepository.UpdateVillages(villages);
            bool result2 = _gameDataRepository.UpdateVillages(villages);

            //Assert
            Assert.True(result1);
            Assert.True(result2);

        }


        private static CalculationToolsDBContext GetDbContext()
        {
            return new CalculationToolsDBContext(dbContextOptions);
        }

        #endregion Methods
    }
}