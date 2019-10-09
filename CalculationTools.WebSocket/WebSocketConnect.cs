using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NLog;
using Websocket.Client;


namespace CalculationTools.WebSocket
{
    public static class WebSocketConnect
    {
        private static readonly ManualResetEvent ExitEvent = new ManualResetEvent(false);
        public static string SessionID { get; set; }

        public static readonly Uri SocketURL = new Uri("wss://en.tribalwars2.com/socket.io/?platform=desktop&EIO=3&transport=websocket");
        private static Logger Log = LogManager.GetCurrentClassLogger();
        public static string AccessToken { get; set; }

        public static WebsocketClient Client = new WebsocketClient(SocketURL);

        //public static async void StartConnection()
        //{
        //    ManualResetEvent exitEvent = new ManualResetEvent(false);
        //    var url = "wss://en.tribalwars2.com/socket.io/";

        //    using (var ws = new WebSocketSharp.WebSocket(url))
        //    {
        //        ws.OnOpen += (sender, e) =>
        //        {
        //            Log.Info("Server is connected!");
        //        };
        //        ws.OnMessage += (sender, e) =>
        //        {
        //            Log.Info("Message received: " + e.Data);
        //        };
        //        ws.OnError += (sender, e) =>
        //        {
        //            Log.Info("Message Error: " + e.Message);
        //        };
        //        ws.Connect();

        //        string message = $"42[\"msg\", {RouteProvider.SystemIdentify()}]";

        //        ws.SendAsync(message, b =>
        //         {
        //             Log.Info("Sending second message:");
        //             message = $"42[\"msg\", {RouteProvider.Login("***REMOVED***", "***REMOVED***")},\"id\":2,\"headers\":{{\"traveltimes\":[[\"browser_send\",1570555427473]]}}}} ]";
        //             ws.Send(message);
        //         });
        //        Console.ReadKey(true);
        //    }

        //}
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
                        Client.Dispose();
                        break;
                }
            });
            Client.DisconnectionHappened.Subscribe(type =>
            {
                Log.Info("Connection was disconnected by server");
            });
            Client.MessageReceived.Subscribe(msg =>
            {
                Log.Info($"Message received: {msg}");
                ParseResponse(msg.Text);
            });


            Log.Info("Start connection with TribalWars2");
            Client.Start().Wait();

            // Send keep alive ping
            Task.Run(StartSendingPing);

            ExitEvent.WaitOne();


        }

        public static void CloseConnection()
        {
            Client.Dispose();
        }

        private static void ParseResponse(string response)
        {
            if (response == null)
            {
                Log.Info("The response was null");
                return;
            }

            if (response == "40")
            {
                Log.Info("The connection was confirmed with code 40");
                return;
            }

            response = CleanResponse(response);

            SocketResponse socketResponse = ParseSocketResponse(response);

            switch (socketResponse.SocketType)
            {
                case RouteProvider.SYSTEM_WELCOME:
                    // On system welcome send system identify
                    SendMessage(RouteProvider.AddMsg(RouteProvider.SystemIdentify()));
                    break;

                case RouteProvider.SYSTEM_IDENTIFIED:
                    // Once system has been identified then send login credentials
                    SendMessage(RouteProvider.AddMsg(RouteProvider.Login("***REMOVED***", "***REMOVED***")));
                    break;

                case RouteProvider.LOGIN_SUCCESS:
                    ParseLoginSuccess(response);
                    break;


                default:
                    break;
            }
        }

        private static void ParseLoginSuccess(string response)
        {
            LoginDTO loginDto = new LoginDTO();

            try
            {
                loginDto = JsonConvert.DeserializeObject<LoginDTO>(response);
            }
            catch (Exception e)
            {
                Log.Info("Could not deserialize the following string:", e);
            }

            AccessToken = loginDto.Data.AccessToken;
            PlayerData.Name = loginDto.Data.Name;
            PlayerData.PlayerId = loginDto.Data.PlayerId;
            PlayerData.IsLoggedIn = true;
        }


        private static string CleanResponse(string response)
        {
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

        private static void SendMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (Client.IsRunning)
                {
                    Log.Info("Message send: " + message);
                    Task.Run(() => Client.Send(message));
                }
            }
        }

        private static async Task StartSendingPing()
        {
            while (true)
            {
                await Task.Delay(1000);

                if (!Client.IsRunning)
                    continue;
                SendMessage("ping");
            }
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
                Log.Info("Could not deserialize the following string:");
                Log.Info(response);
            }

            // Set parsed values
            if (!string.IsNullOrEmpty(socketResponse.SessionID))
            {
                SessionID = socketResponse.SessionID;
            }

            return socketResponse;
        }
    }
}
