using CalculationTools.Common;
using CalculationTools.Data;
using CalculationTools.Tests.Data;
using CalculationTools.WebSocket;
using System.Threading;
using Xunit;

namespace CalculationTools.Tests
{
    public static class WebSocketTests
    {


        [Fact]
        public static async void CheckCredentialsWithValidAndInValidAndReturnTrue()
        {
            //Arrange
            SocketManager socketManager = MockData.GetSocketManager(true);

            ConnectData wrongCredentials = new ConnectData
            {
                Username = new Bogus.DataSets.Hacker().Phrase(),
                Password = new Bogus.DataSets.Hacker().Phrase(),
                ServerCountryCode = "en",
            };

            Account account = SecretData.GetValidTestAccount();
            ConnectData validCredentials = new ConnectData
            {
                Username = account.Username,
                Password = account.Password,
                ServerCountryCode = "en",
            };

            //Act
            ConnectResult result1 = await socketManager.TestConnection(wrongCredentials);
            ConnectResult result2 = await socketManager.TestConnection(validCredentials);

            //Assert
            Assert.False(result1.IsConnected);
            Assert.True(result2.IsConnected);

        }

        [Fact]
        public static async void StartConnectionAndTestConnectionDuration()
        {
            //Arrange
            SocketManager socketManager = MockData.GetSocketManager(true);

            Account account = SecretData.GetValidTestAccount();
            ConnectData validCredentials = new ConnectData
            {
                Username = account.Username,
                Password = account.Password,
                ServerCountryCode = "en",
                WorldID = "en48"
            };


            //Act
            ConnectResult result1 = await socketManager.StartConnection(validCredentials);

            Thread.Sleep(6000);

            string result = await socketManager
                 .GetSocketClient()
                 .Emit(RouteProvider.GetDefaultSendMessage(RouteProvider.SYSTEM_GETTIME), RouteProvider.Id - 1);

            Thread.Sleep(600000);

            //Assert
            Assert.True(socketManager.IsConnected);
            await socketManager.StopConnection(true);

        }

        [Fact]
        public static async void TestSocketRepository()
        {
            //Arrange
            SocketManager socketManager = MockData.GetSocketManager(true);
            SocketRepository socketRepository = new SocketRepository(socketManager);

            Account account = SecretData.GetValidTestAccount();
            ConnectData validCredentials = new ConnectData
            {
                Username = account.Username,
                Password = account.Password,
                ServerCountryCode = "en",
                WorldID = "en48"
            };


            //Act
            ConnectResult result1 = await socketManager.StartConnection(validCredentials);

            Thread.Sleep(6000);

            var systemTime = await socketRepository.GetSystemTimeAsync();
            var villageList = await socketRepository.GetVillagesByAutocomplete("ka");

            Thread.Sleep(6000);

            systemTime = await socketRepository.GetSystemTimeAsync();

            Thread.Sleep(6000);

            systemTime = await socketRepository.GetSystemTimeAsync();

            Thread.Sleep(600000);

            //Assert
            Assert.True(socketManager.IsConnected);
            await socketManager.StopConnection(true);

        }
    }
}