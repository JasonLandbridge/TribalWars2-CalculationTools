using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Websocket.Client;


namespace CalculationTools.WebSocket
{
    public static class WebSocketConnect
    {
        #region Fields

        public static readonly Uri SocketURL = new Uri("wss://en.tribalwars2.com/socket.io/?platform=desktop&EIO=3&transport=websocket");
        public static WebsocketClient Client = new WebsocketClient(SocketURL);

        private static readonly ManualResetEvent ExitEvent = new ManualResetEvent(false);
        private static Logger Log = LogManager.GetCurrentClassLogger();
        #endregion Fields

        #region Properties

        public static bool IsConnected => Client.IsRunning;

        public static bool IsLoggedIn => PlayerData.IsLoggedIn;
        public static string AccessToken { get; set; }
        public static string SessionID { get; set; }

        #endregion Properties

        #region Methods

        public static void CloseConnection()
        {
            Client.Stop(WebSocketCloseStatus.NormalClosure, "").Wait();
            Client.Dispose();
            PlayerData.IsLoggedIn = false;
        }

        public static SocketResponse ParseSocketResponse(string response)
        {

            string headerType = string.Empty;

            if (response.StartsWith("0{"))
            {
                response = response.Remove(0, 1);
                headerType = "sid";
            }

            SocketResponse socketResponse = new SocketResponse();
            try
            {
                socketResponse = JsonConvert.DeserializeObject<SocketResponse>(response);
                socketResponse.HeaderType = headerType;
            }
            catch (Exception e)
            {
                Log.Info(e, "Could not deserialize the following string:");
            }

            // Set parsed values
            if (!string.IsNullOrEmpty(socketResponse.SessionID))
            {
                SessionID = socketResponse.SessionID;
            }

            return socketResponse;
        }

        public static void StartConnection()
        {

            Client.ReconnectTimeoutMs = (int)TimeSpan.FromSeconds(30).TotalMilliseconds;
            Client.ReconnectionHappened.Subscribe(type =>
            {
                Log.Info($"Reconnection happened, type: {type}");

                switch (type)
                {
                    case ReconnectionType.Initial:
                        Log.Info("Server is connected!");
                        break;
                    case ReconnectionType.Lost:
                        CloseConnection();
                        break;
                }
            });
            Client.DisconnectionHappened.Subscribe(type =>
            {
                Log.Info("Connection was disconnected by server");
            });
            Client.MessageReceived.Subscribe(msg =>
            {
                Log.Info($"Message received: {msg.Text}");
                ParseResponse(msg.Text);
            });

            Log.Debug("Start connection with TribalWars2");
            Client.Start().Wait();

            // Send keep alive ping
            Task.Run(StartSendingPing);

            ExitEvent.WaitOne();
        }




        private static string CleanResponse(string response)
        {
            //TODO Delete all numbers from the beginning
            List<string> filterIds = new List<string>
            {
                "0",
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "40",
                "42",
            };

            // Remove the Socket.io identifier string from the start
            foreach (string filterId in filterIds)
            {
                response = response.StartsWith(filterId, StringComparison.Ordinal) ? response.Replace($"{filterId}[", "[", StringComparison.Ordinal) : response;
            }

            //Clean off the outer msg tag
            if (response.StartsWith("[\"msg\",") && response.EndsWith("]"))
            {
                response = response.Replace("[\"msg\",", "");
                response = response.Remove(response.Length - 1, 1);
            }

            return response;
        }

        private static LoginDTO ParseLoginSuccess(string response)
        {
            LoginDTO loginDto = new LoginDTO();

            try
            {
                loginDto = JsonConvert.DeserializeObject<LoginDTO>(response);
            }
            catch (Exception e)
            {
                Log.Info(e, "Could not deserialize the following string:");
            }

            AccessToken = loginDto.Data.AccessToken;
            PlayerData.Name = loginDto.Data.Name;
            PlayerData.PlayerId = loginDto.Data.PlayerId;
            PlayerData.IsLoggedIn = true;

            return loginDto;
        }

        private static void ParseResponse(string response)
        {
            if (string.IsNullOrEmpty(response))
            {
                Log.Info("The response was null/empty");
                return;
            }

            // Ping response or open connection response
            if (response == "3" || response == "40")
            {
                return;
            }

            response = CleanResponse(response);

            SocketResponse socketResponse = ParseSocketResponse(response);

            switch (socketResponse.SocketType)
            {
                case RouteProvider.SYSTEM_WELCOME:
                    // On system welcome send system identify
                    SendMessage(RouteProvider.SystemIdentify());
                    break;

                case RouteProvider.SYSTEM_IDENTIFIED:
                    // Once system has been identified then send login credentials
                    SendMessage(RouteProvider.Login("***REMOVED***", "***REMOVED***"));
                    break;

                case RouteProvider.LOGIN_SUCCESS:
                    LoginDTO loginDto = ParseLoginSuccess(response);
                    SendMessage(RouteProvider.SelectCharacter(loginDto));
                    break;

                case RouteProvider.CHARACTER_SELECTED:

                    break;

                default:
                    break;
            }
        }
        private static void SendMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (Client.IsRunning)
                {
                    Log.Info($"Message Send:     {message}");
                    Task.Run(() => Client.Send(message));
                }
            }
        }

        private static async Task StartSendingPing()
        {
            while (true)
            {
                await Task.Delay(2000);

                if (!Client.IsRunning)
                    continue;
                SendMessage("2");
            }
        }

        #endregion Methods
    }
}
