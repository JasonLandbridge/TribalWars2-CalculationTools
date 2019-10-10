using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;
using System.Threading;
using System.Threading.Tasks;
using Websocket.Client;

namespace CalculationTools.WebSocket
{
    public class SocketClient
    {
        #region Fields


        private static readonly ManualResetEvent ExitEvent = new ManualResetEvent(false);
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        #endregion Fields

        #region Events

        private readonly TaskCompletionSource<ConnectResult> ConnectionResult = new TaskCompletionSource<ConnectResult>();

        #endregion

        public SocketClient()
        {

        }

        #region Properties

        public WebsocketClient Client { get; set; }

        public ConnectData ConnectData { get; set; } = new ConnectData();

        public ConnectResult ConnectResult { get; set; } = new ConnectResult();

        public bool IsConnected => ConnectResult.IsConnected;

        public int PingCount { get; set; }

        #endregion Properties

        #region Methods

        public async Task<ConnectResult> StartConnectionAsync(ConnectData connectData, bool keepAlive = false)
        {
            ConnectData = connectData;
            AppDomain.CurrentDomain.ProcessExit += CurrentDomainOnProcessExit;
            AssemblyLoadContext.Default.Unloading += DefaultOnUnloading;

            using (IWebsocketClient client = new WebsocketClient(connectData.Uri))
            {
                client.ReconnectTimeoutMs = (int)TimeSpan.FromSeconds(30).TotalMilliseconds;
                client.ReconnectionHappened.Subscribe(type =>
                {
                    Log.Info($"Reconnection happened, type: {type}");

                    switch (type)
                    {
                        case ReconnectionType.Initial:
                            Log.Info("Server is connected!");
                            break;
                    }
                });
                client.DisconnectionHappened.Subscribe(type =>
                {
                    Log.Info("Connection was disconnected by server");
                    ConnectionResult.TrySetResult(ConnectResult);
                });
                client.MessageReceived.Subscribe(msg =>
                {
                    Log.Info($"Message received: {msg.Text}");
                    ParseResponse(client, msg.Text);
                });

                Log.Debug("Start connection with TribalWars2");

                await client.Start();

                if (keepAlive)
                {
                    // Send keep alive ping
                    await Task.Run(() => SendPingAsync(client));
                }

                return await ConnectionResult.Task;

            }
        }

        public void CloseConnection()
        {
            ConnectionResult.TrySetCanceled(CancellationToken.None);
        }
        #region ParseResponse

        private void ParseResponse(IWebsocketClient client, string response)
        {
            if (string.IsNullOrEmpty(response))
            {
                Log.Info("The response was null/empty");
                return;
            }

            // open connection response
            if (response == "40")
            {
                return;
            }

            if (response == "3")
            {
                PingCount++;
                return;
            }

            response = CleanResponse(response);

            SocketResponse socketResponse = ParseSocketResponse(response);

            switch (socketResponse.SocketType)
            {
                case RouteProvider.SYSTEM_WELCOME:
                    // On system welcome send system identify
                    SendMessageAsync(client, RouteProvider.SystemIdentify());
                    break;

                case RouteProvider.SYSTEM_IDENTIFIED:
                    // Once system has been identified then send login credentials
                    SendMessageAsync(client, RouteProvider.Login(ConnectData));
                    break;

                case RouteProvider.LOGIN_SUCCESS:
                    LoginDTO loginDto = ParseLoginSuccess(response);
                    SendMessageAsync(client, RouteProvider.SelectCharacter(loginDto));
                    break;

                case RouteProvider.CHARACTER_SELECTED:

                    break;

                case RouteProvider.SYSTEM_ERROR:
                    ParseSystemError(response);
                    ConnectResult.IsConnected = false;
                    ConnectionResult.TrySetResult(ConnectResult);
                    break;

                default:

                    //ExitEvent.Set();
                    break;
            }
        }

        public SocketResponse ParseSocketResponse(string response)
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
                Log.Error(e, "Could not deserialize the following string:");
            }

            // Set parsed values
            if (!string.IsNullOrEmpty(socketResponse.SessionID))
            {
                ConnectResult.SessionID = socketResponse.SessionID;
            }

            return socketResponse;
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

        private LoginDTO ParseLoginSuccess(string response)
        {
            LoginDTO loginDto = new LoginDTO();

            try
            {
                loginDto = JsonConvert.DeserializeObject<LoginDTO>(response);
            }
            catch (Exception e)
            {
                Log.Error(e, "Could not deserialize the following string:");
            }

            ConnectResult.AccessToken = loginDto.Data.AccessToken;
            ConnectResult.IsConnected = true;
            ConnectionResult.TrySetResult(ConnectResult);
            return loginDto;
        }
        private void ParseSystemError(string response)
        {
            SystemErrorDTO systemError = new SystemErrorDTO();
            var jsonObject = (JObject)JsonConvert.DeserializeObject(response);

            if (jsonObject["data"].Any())
            {
                try
                {
                    systemError = JsonConvert.DeserializeObject<SystemErrorDTO>(jsonObject["data"].ToString());
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Could not deserialize the following string in {nameof(ParseSystemError)}:");
                }

                if (systemError.Cause == RouteProvider.LOGIN)
                {
                    ConnectResult.IsConnected = false;
                    ConnectionResult.TrySetResult(ConnectResult);
                }
            }

        }


        #endregion

        private static async void SendMessageAsync(IWebsocketClient client, string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (client.IsRunning)
                {
                    Log.Info($"Message Send:     {message}");
                    await Task.Run(() => client.Send(message));
                }
            }
        }

        private static async Task SendPingAsync(IWebsocketClient client)
        {
            while (true)
            {
                await Task.Delay(2000);

                if (!client.IsRunning)
                    continue;
                SendMessageAsync(client, "2");
            }
        }

        private static void CurrentDomainOnProcessExit(object sender, EventArgs eventArgs)
        {
            Log.Info("Exiting process");
            ExitEvent.Set();
        }

        private static void DefaultOnUnloading(AssemblyLoadContext assemblyLoadContext)
        {
            Log.Info("Unloading process");
            ExitEvent.Set();
        }
        #endregion Methods
    }


}
