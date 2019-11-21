using CalculationTools.Tests.Data;
using CalculationTools.WebSocket;
using CalculationTools.WebSocket.Repository;
using Xunit;

namespace CalculationTools.Tests
{
    public static class SocketRepositoryTests
    {

        [Fact]
        public static void GetIdShouldReturnCorrectIds()
        {
            //Arrange
            SocketManager socketManager = MockData.GetSocketManager(true);
            SocketRepository socketRepository = new SocketRepository(socketManager);

            string test =
                "42[\"msg\",{\"id\":15,\"type\":\"System/getTime\",\"headers\":{\"traveltimes\":[[\"browser_send\",1574343907381]]}}]";

            //Act
            int? result = socketRepository.GetId(test);


            // Assert
            Assert.NotNull(result);

        }
    }
}
