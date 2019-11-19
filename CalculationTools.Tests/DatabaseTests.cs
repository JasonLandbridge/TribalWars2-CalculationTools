using CalculationTools.Common;
using CalculationTools.Data;
using CalculationTools.Tests.Data;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CalculationTools.Tests
{
    public static class DatabaseTests
    {

        #region Properties



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
            using (var db = MockData.GetDbContext())
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
            IGameDataRepository _gameDataRepository = MockData.GetIGameDataRepository(true);

            ILoginData loginData = MockData.GetILoginData();

            // Act
            _gameDataRepository.UpdateLoginData(loginData);


            using (var db = MockData.GetDbContext())
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
            IGameDataRepository _gameDataRepository = MockData.GetIGameDataRepository();

            List<IVillage> villages = MockData.GetCharacterVillages();

            //Act
            bool result1 = _gameDataRepository.UpdateVillages(villages);
            bool result2 = _gameDataRepository.UpdateVillages(villages);

            //Assert
            Assert.True(result1);
            Assert.True(result2);

        }




        #endregion Methods
    }
}