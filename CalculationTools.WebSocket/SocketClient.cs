using CalculationTools.Common;
using CalculationTools.Common.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.ComponentModel;
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

        private readonly IPlayerData _playerData;

        private readonly TaskCompletionSource<ConnectResult> ConnectionResult = new TaskCompletionSource<ConnectResult>();

        #endregion Fields

        #region Constructors

        public SocketClient(ConnectData connectData, IPlayerData playerData)
        {
            _playerData = playerData;
            ConnectData = connectData;

            Client = new WebsocketClient(ConnectData.Uri)
            {
                ReconnectTimeoutMs = (int)TimeSpan.FromSeconds(30).TotalMilliseconds
            };

            Client.ReconnectionHappened.Subscribe(type =>
            {
                Log.Info($"Reconnection happened, type: {type}");

                switch (type)
                {
                    case ReconnectionType.Initial:
                        Log.Info("Server is connected!");
                        break;
                }
            });
            Client.DisconnectionHappened.Subscribe(type =>
            {
                Log.Info("Connection was disconnected by server");
                ConnectionResult.TrySetResult(ConnectResult);
            });
            Client.MessageReceived.Subscribe(msg =>
            {
                Log.Info($"Message received: {msg.Text}");
                ParseResponse(Client, msg.Text);
            });

            SetupBackgroundWorker();

        }

        #endregion Constructors

        #region Properties

        public BackgroundWorker BackgroundWorker { get; } = new BackgroundWorker();
        public WebsocketClient Client { get; set; }
        public ConnectData ConnectData { get; set; } = new ConnectData();
        public ConnectResult ConnectResult { get; set; } = new ConnectResult();
        public bool IsConnected => ConnectResult.IsConnected;
        public int PingCount { get; set; }

        #endregion Properties

        #region Methods

        public void CloseConnection()
        {
            BackgroundWorker.CancelAsync();

            ConnectionResult.TrySetCanceled(CancellationToken.None);
        }

        public void SetupBackgroundWorker()
        {
            BackgroundWorker.WorkerSupportsCancellation = true;
            BackgroundWorker.DoWork += async (sender, args) =>
            {
                while (true)
                {
                    await Task.Delay(2000);

                    if (!Client.IsRunning)
                        continue;
                    SendMessageAsync(Client, "2");
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
            AppDomain.CurrentDomain.ProcessExit += CurrentDomainOnProcessExit;
            AssemblyLoadContext.Default.Unloading += DefaultOnUnloading;

            Log.Debug("Start connection with TribalWars2");

            await Client.Start();

            if (keepAlive)
            {
                BackgroundWorker.RunWorkerAsync();
            }

            return await ConnectionResult.Task;

        }

        #region ParseMethods
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
                    var loginDto = ParseLoginSuccess(response);
                    SendMessageAsync(client, RouteProvider.SelectCharacter(loginDto));
                    break;

                case RouteProvider.CHARACTER_SELECTED:

                    break;

                case RouteProvider.SYSTEM_ERROR:
                    ParseSystemError(response);
                    ConnectResult.IsConnected = false;
                    ConnectionResult.TrySetResult(ConnectResult);
                    break;

                case RouteProvider.MESSAGE_ERROR:
                    var errorMessage = ParseDataFromResponse<ErrorMessageDTO>(response);
                    Log.Error($"Socket Error: {errorMessage.ErrorCode} - {errorMessage.Message}");
                    CloseConnection();
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
            ConnectionResult.TrySetResult(ConnectResult);
            ConnectResult.AccessToken = loginData?.AccessToken;

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
                    ConnectionResult.TrySetResult(ConnectResult);
                }
            }

        }




        #endregion

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

        private void CurrentDomainOnProcessExit(object sender, EventArgs eventArgs)
        {
            Log.Info("Exiting process");
            CloseConnection();
        }

        private void DefaultOnUnloading(AssemblyLoadContext assemblyLoadContext)
        {
            Log.Info("Unloading process");
            CloseConnection();
        }
        #endregion Methods

    }


}
