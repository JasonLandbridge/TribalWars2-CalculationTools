﻿using CalculationTools.Common;
using CalculationTools.Common.Connection;
using CalculationTools.Common.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.ComponentModel;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;
using Websocket.Client;

namespace CalculationTools.WebSocket
{
    public class SocketClient : BasePropertyChanged
    {

        #region Fields

        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private readonly IPlayerData _playerData;

        private readonly TaskCompletionSource<ConnectResult> ConnectionResult = new TaskCompletionSource<ConnectResult>();

        #endregion Fields

        #region Constructors

        public SocketClient(IPlayerData playerData)
        {
            _playerData = playerData;

        }

        #endregion Constructors

        public event EventHandler ConnectionLogUpdated;

        #region Properties

        public BackgroundWorker BackgroundWorker { get; } = new BackgroundWorker();
        public WebsocketClient Client { get; set; }
        public ConnectData ConnectData { get; set; }
        public ConnectResult ConnectResult { get; set; } = new ConnectResult();
        public bool IsConnected => ConnectResult.IsConnected;
        public StringBuilder ConnectionLog { get; set; } = new StringBuilder();

        public bool ClientHasBeenSetup { get; set; }
        public int PingCount { get; set; }

        #endregion Properties

        #region Methods

        #region Connection

        public bool SetupConnection(ConnectData connectData, bool shouldReconnect = true)
        {
            ConnectData = connectData;

            Client = new WebsocketClient(ConnectData.Uri)
            {
                ReconnectTimeoutMs = (int)TimeSpan.FromSeconds(30).TotalMilliseconds
            };
            Client.IsReconnectionEnabled = shouldReconnect;

            Client.ReconnectionHappened.Subscribe(type =>
            {

                Log.Info($"Reconnection happened, type: {type}");

                switch (type)
                {
                    case ReconnectionType.Initial:

                        Log.Info("Server is connected!");
                        AddToConnectionLog("Server is connected!");
                        break;
                }
            });
            Client.DisconnectionHappened.Subscribe(async type =>
            {
                Log.Info("Connection was disconnected by server");
                await CloseConnection();
            });
            Client.MessageReceived.Subscribe(msg =>
            {
                string log = $"Message received: {msg.Text}";
                Log.Info(log);
                AddToConnectionLog(log);
                ParseResponseAsync(Client, msg.Text);
            });

            SetupBackgroundWorker();
            ClientHasBeenSetup = true;
            return true;
        }

        public void SetupBackgroundWorker()
        {
            BackgroundWorker.WorkerSupportsCancellation = true;
            BackgroundWorker.DoWork += async (sender, args) =>
            {
                while (true)
                {
                    await Task.Delay(1000);

                    if (!Client.IsRunning)
                        continue;
                    SendMessageAsync("2");
                }
            };
            BackgroundWorker.RunWorkerCompleted += (sender, args) =>
            {
                Log.Info("Background worker has completed their pings!");
            };
            BackgroundWorker.ProgressChanged += (sender, args) => { PingCount++; };

        }

        public async Task<ConnectResult> StartConnectionAsync(bool keepAlive = true)
        {
            if (!ClientHasBeenSetup)
            {
                string message = "Connection setup has not been performed before starting the connection";
                Log.Error(message);
                return new ConnectResult
                {
                    IsConnected = false,
                    Message = message
                };
            }

            if (Client.IsRunning)
            {
                return new ConnectResult
                {
                    IsConnected = true,
                    Message = "Connection is already running"
                };
            }

            //AppDomain.CurrentDomain.ProcessExit += CurrentDomainOnProcessExit;
            //AssemblyLoadContext.Default.Unloading += DefaultOnUnloading;

            Log.Debug("Start connection with TribalWars2");

            await Client.Start();

            if (keepAlive && !BackgroundWorker.IsBusy)
            {
                BackgroundWorker.RunWorkerAsync();
            }


            return await ConnectionResult.Task;

        }
        public async Task<bool> CloseConnection()
        {
            Log.Info("Closing connection!");
            AddToConnectionLog("Closing connection");
            if (Client != null)
            {
                var result = await Client?.Stop(WebSocketCloseStatus.NormalClosure, "Closed by command");
            }
            if (BackgroundWorker.WorkerSupportsCancellation)
            {
                BackgroundWorker.CancelAsync();
            }
            PingCount = 0;
            ClientHasBeenSetup = false;
            return true;
        }

        private async void SendMessageAsync(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (Client != null && Client.IsRunning)
                {
                    string log = $"Message Send:     {message}";
                    AddToConnectionLog(log);
                    Log.Info(log);
                    await Task.Run(() => Client.Send(message));
                }
            }
        }

        /// <summary>
        /// Will login with the <see cref="ConnectData"/> and disconnect again when a result has been returned.
        /// </summary>
        /// <param name="connectData">Connection credentials</param>
        /// <returns></returns>
        public async Task<ConnectResult> TestConnectionAsync(ConnectData connectData)
        {
            // Ensure connection has been closed
            await CloseConnection();
            SetupConnection(connectData, false);
            ConnectResult result = await StartConnectionAsync(false);
            Log.Debug($"Returning result from TestConnection: {result}");
            await CloseConnection();

            return result;
        }

        #endregion
        #region ParseMethods
        private async void ParseResponseAsync(IWebsocketClient client, string response)
        {
            if (string.IsNullOrEmpty(response))
            {
                Log.Info("The response was null/empty");
                //ConnectionResult.TrySetResult(new ConnectResult
                //{
                //    IsConnected = false,
                //    Message = "Received a null/empty answer"
                //});
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
                    SendMessageAsync(RouteProvider.SystemIdentify());
                    break;

                case RouteProvider.SYSTEM_IDENTIFIED:
                    // Once system has been identified then send login credentials
                    SendMessageAsync(RouteProvider.Login(ConnectData));
                    break;

                case RouteProvider.LOGIN_SUCCESS:
                    var loginDto = ParseLoginSuccess(response);
                    SendMessageAsync(RouteProvider.SelectCharacter(loginDto));
                    break;

                case RouteProvider.CHARACTER_SELECTED:

                    break;

                case RouteProvider.SYSTEM_ERROR:
                    ParseSystemError(response);
                    break;

                case RouteProvider.MESSAGE_ERROR:
                    var errorMessage = ParseDataFromResponse<ErrorMessageDTO>(response);
                    Log.Error($"Socket Error: {errorMessage.ErrorCode} - {errorMessage.Message}");
                    await CloseConnection();
                    break;

                case RouteProvider.EXCEPTION_ERROR:
                    var errorMessage2 = ParseDataFromResponse<SystemErrorDTO>(response);
                    Log.Error($"Server exception error: {errorMessage2.Message}");
                    AddToConnectionLog("THE SERVER IS MOST LIKELY DOWN!");
                    await CloseConnection();
                    break;

                default:

                    //ExitEvent.Set();
                    break;
            }
        }

        private SocketResponse ParseSocketResponse(string response)
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

        /// <summary>
        /// This takes the data key from the Socket.io response and parses it to type <see cref="T"/>
        /// </summary>
        /// <typeparam name="T">The DTO class it will use to deserialize and to return</typeparam>
        /// <param name="response">The string to parse</param>
        /// <returns></returns>
        private T ParseDataFromResponse<T>(string response)
        {
            JObject jsonObject = (JObject)JsonConvert.DeserializeObject(response);

            if (jsonObject["data"].Any())
            {
                try
                {
                    return JsonConvert.DeserializeObject<T>(jsonObject["data"].ToString());
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Could not parse type {typeof(T)} with data object in string:{Environment.NewLine}{response}");
                }
            }

            Log.Error($"Could not find data object in JsonString: {response}");

            return default;
        }

        private LoginDataDTO ParseLoginSuccess(string response)
        {
            var loginData = ParseDataFromResponse<LoginDataDTO>(response);

            ConnectResult.IsConnected = true;
            ConnectResult.AccessToken = loginData?.AccessToken;
            ConnectionResult.TrySetResult(ConnectResult);

            // Send parsed data to the PlayerData to be stored
            _playerData.SetLoginData(loginData);
            return loginData;
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
                    ConnectResult.Message = systemError.Message;
                    ConnectionResult.TrySetResult(ConnectResult);
                }
            }

        }

        private static string CleanResponse(string response)
        {
            // Remove the Socket.io identifier string from the start
            var digits = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            response = response.TrimStart(digits);

            //Clean off the outer msg tag since 99% are messages anyway
            if (response.StartsWith("[\"msg\",") && response.EndsWith("]"))
            {
                response = response.Replace("[\"msg\",", "");
                response = response.Remove(response.Length - 1, 1);
            }

            return response;
        }
        #endregion
        private async void CurrentDomainOnProcessExit(object sender, EventArgs eventArgs)
        {
            Log.Info("Exiting process");
            await CloseConnection();
        }

        private async void DefaultOnUnloading(AssemblyLoadContext assemblyLoadContext)
        {
            Log.Info("Unloading process");
            await CloseConnection();
        }

        private void AddToConnectionLog(string message)
        {
            ConnectionLog.Append(message + Environment.NewLine);
            ConnectionLogUpdated.Invoke(this, EventArgs.Empty);
        }
        #endregion Methods

    }


}