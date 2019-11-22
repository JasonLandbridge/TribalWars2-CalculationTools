using CalculationTools.Common;
using CalculationTools.Data;
using CalculationTools.Tests.Data;
using CalculationTools.WebSocket;
using Xunit;

namespace CalculationTools.Tests
{
    public static class SocketRepositoryTests
    {

        [Fact]
        public static void GetIdShouldReturnCorrectIds()
        {
            //Arrange
            SocketManager socketManager = MockData.GetSocketManager();
            SocketRepository socketRepository = new SocketRepository(socketManager);

            string test =
                "42[\"msg\",{\"id\":15,\"type\":\"System/getTime\",\"headers\":{\"traveltimes\":[[\"browser_send\",1574343907381]]}}]";

            //Act
            int? result = SocketUtilities.GetId(test);


            // Assert
            Assert.NotNull(result);

        }


        [Fact]
        public static async void EstablishConnectionTestIfConnecting()
        {
            //Arrange
            ISocketRepository socketRepository = MockData.GetISocketRepository(true);
            Account account = SecretData.GetValidTestAccount();

            //Act
            var result = await socketRepository.EstablishConnection(account);

            //Assert
            Assert.True(result);


        }
    }
}
