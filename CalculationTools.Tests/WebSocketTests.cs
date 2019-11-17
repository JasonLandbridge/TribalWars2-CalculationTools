using CalculationTools.Common;
using CalculationTools.WebSocket;
using Xunit;

namespace CalculationTools.Tests
{
    public static class WebSocketTests
    {
        [Fact]
        public static void TestSendSocketMessage()
        {
            ConnectData connectData = new ConnectData
            {
                Username = "",
                Password = "",
                ServerCountryCode = "en"
            };
            string message = RouteProvider.Login(connectData);

            Assert.NotEmpty(message);
        }
    }
}