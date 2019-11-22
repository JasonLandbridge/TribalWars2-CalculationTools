using CalculationTools.Common;
using CalculationTools.Data;
using CalculationTools.Tests.Data;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CalculationTools.Tests
{
    public static class GameDataRepositoryTests
    {


        [Fact]
        public static async void FullTW2DataHarvest()
        {
            //Arrange
            IGameDataRepository _gameDataRepository = MockData.GetIGameDataRepository(true);
            Account account = SecretData.GetValidTestAccount();

            //Act
            _gameDataRepository.InsertOrUpdateAccount(account);
            await _gameDataRepository.TestAccountASync(account);
            Account foundAccount = _gameDataRepository.GetAccount(account.Id);

            //Assert
            Assert.NotNull(foundAccount);
            Assert.NotNull(foundAccount.TW2AccountID);

            //Act
            var result = await _gameDataRepository.EstablishConnection(foundAccount);

            //Assert
            Assert.True(result);

        }


        [Fact]
        public static async void CheckCredentialsWithValidAndInValidAndReturnTrue()
        {
            //Arrange
            IGameDataRepository _gameDataRepository = MockData.GetIGameDataRepository(true);

            Account wrongAccount = new Account
            {
                Username = new Bogus.DataSets.Hacker().Phrase(),
                Password = new Bogus.DataSets.Hacker().Phrase(),
                OnServerId = "en"
            };
            Account validAccount = SecretData.GetValidTestAccount();

            //Act
            ConnectResult result1 = await _gameDataRepository.TestAccountASync(wrongAccount);
            ConnectResult result2 = await _gameDataRepository.TestAccountASync(validAccount);

            //Assert
            Assert.False(result1.IsConnected);
            Assert.True(result2.IsConnected);

        }


        #region ParseMockData
        [Fact]
        public static void ShouldStoreAllIncomingDataCorrectly()
        {
            ShouldUpdateLoginDataToDbCorrectly();

            ShouldInsertAndUpdateVillages();
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

        #endregion

    }
}
