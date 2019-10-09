using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Websocket.Client;
using WebSocketSharp;

namespace CalculationTools.WebSocket
{
    public static class WebSocketConnect
    {
        private static readonly ManualResetEvent ExitEvent = new ManualResetEvent(false);
        public static string SessionID { get; set; }

        public static readonly Uri SocketURL = new Uri("wss://en.tribalwars2.com/socket.io/?platform=desktop&EIO=3&transport=websocket");

        public static WebsocketClient Client = new WebsocketClient(SocketURL);

        //public static async void StartConnection()
        //{
        //    ManualResetEvent exitEvent = new ManualResetEvent(false);
        //    var url = "wss://en.tribalwars2.com/socket.io/";

        //    using (var ws = new WebSocketSharp.WebSocket(url))
        //    {
        //        ws.OnOpen += (sender, e) =>
        //        {
        //            Debug.WriteLine("Server is connected!");
        //        };
        //        ws.OnMessage += (sender, e) =>
        //        {
        //            Debug.WriteLine("Message received: " + e.Data);
        //        };
        //        ws.OnError += (sender, e) =>
        //        {
        //            Debug.WriteLine("Message Error: " + e.Message);
        //        };
        //        ws.Connect();

        //        string message = $"42[\"msg\", {RouteProvider.SystemIdentify()}]";

        //        ws.SendAsync(message, b =>
        //         {
        //             Debug.WriteLine("Sending second message:");
        //             message = $"42[\"msg\", {RouteProvider.Login("***REMOVED***", "***REMOVED***")},\"id\":2,\"headers\":{{\"traveltimes\":[[\"browser_send\",1570555427473]]}}}} ]";
        //             ws.Send(message);
        //         });
        //        Console.ReadKey(true);
        //    }

        //}
        public static async void StartConnection()
        {

            Client.ReconnectTimeoutMs = (int)TimeSpan.FromSeconds(30).TotalMilliseconds;
            Client.ReconnectionHappened.Subscribe(type =>
            {
                Debug.WriteLine($"Reconnection happened, type: {type}");

                switch (type)
                {
                    case ReconnectionType.Initial:
                        Debug.WriteLine("Server is connected!");
                        break;
                    case ReconnectionType.Lost:
                        Client.Dispose();
                        break;
                }
            });
            Client.DisconnectionHappened.Subscribe(type =>
            {
                Debug.WriteLine("Connection was disconnected by server");
            });
            Client.MessageReceived.Subscribe(msg =>
            {
                Debug.WriteLine($"Message received: {msg}");
                ParseResponse(msg.Text);
            });

            Client.Start().Wait();

            // Send keep alive ping
            await Task.Run(StartSendingPing);

            ExitEvent.WaitOne();


        }



        private static void ParseResponse(string response)
        {
            if (response == null)
            {
                Debug.WriteLine("The response was null");
                return;
            }

            if (response == "40")
            {
                Debug.WriteLine("The connection was confirmed with code 40");
                return;
            }

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

                default:
                    break;
            }
        }


        private static string FilterSocketId(string response)
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

            return response;
        }

        private static void SendMessage(string message)
        {
            if (!message.IsNullOrEmpty())
            {
                if (Client.IsRunning)
                {
                    Debug.WriteLine("Message send: " + message);
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

            response = FilterSocketId(response);
            string headerType = string.Empty;

            if (response.StartsWith("0{"))
            {
                response = response.Remove(0, 1);
                headerType = "sid";
            }

            if (response.StartsWith("[\"msg\",") && response.EndsWith("]"))
            {
                response = response.Replace("[\"msg\",", "");
                response = response.Remove(response.Length - 1, 1);
                headerType = "msg";
            }

            SocketResponse socketResponse = new SocketResponse();
            try
            {
                socketResponse = JsonConvert.DeserializeObject<SocketResponse>(response);
                socketResponse.HeaderType = headerType;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Could not deserialize the following string:");
                Debug.WriteLine(response);
            }

            // Set parsed values
            if (!socketResponse.SessionID.IsNullOrEmpty())
            {
                SessionID = socketResponse.SessionID;
            }

            return socketResponse;
        }
    }
}
