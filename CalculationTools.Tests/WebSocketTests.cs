using System;
using System.Threading.Tasks;
using CalculationTools.Core;
using CalculationTools.WebSocket;
using Xunit;

namespace CalculationTools.Tests
{
    public static class WebSocketTests
    {
        [Theory]
        [InlineData(5)]
        public static void TestConnection(int i)
        {

            Random rnd = new Random();

            WebSocketConnect.StartConnection();

            Task.Delay(rnd.Next(6000, 12000)).Wait();

            WebSocketConnect.CloseConnection();
            Assert.True(!WebSocketConnect.IsConnected);
        }

        [Fact]
        public static void TestSocketResponse()
        {
            string message = "0{\"sid\":\"usiEsHnoa12XtKxAAAA6\",\"upgrades\":[],\"pingInterval\":25000,\"pingTimeout\":5000}";
            SocketResponse socketResponse = WebSocketConnect.ParseSocketResponse(message);


        }

        [Fact]
        public static void TestSendSocketMessage()
        {
            string message = RouteProvider.Login("***REMOVED***", "***REMOVED***");

            Assert.NotEmpty(message);
        }
    }
}