using CalculationTools.Common;
using Newtonsoft.Json;
using NLog;
using System;
using System.Net.WebSockets;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Websocket.Client;
using Timer = System.Timers.Timer;

namespace CalculationTools.WebSocket
{
    public class SocketClient : BasePropertyChanged, IDisposable
    {

        #region Fields

        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private readonly TaskCompletionSource<ConnectResult> ConnectionResult = new TaskCompletionSource<ConnectResult>();

        private static readonly object lockObj = new Object();

        private bool _keepAlive;
        private int _pingInterval = 25000;
        private int _pingTimeout = 5000;
        private string _sessionId = "";

        /// <summary>
        /// The first ping is send after <see cref="_pingTimeout"/> has expired, after which a ping will be send every <see cref="_pingInterval"/> until a non-ping message is send. 
        /// </summary>
        private bool _firstPingSend;

        private static readonly Timer LastMsgSendTimer = new Timer(200);
        private readonly IErrorMessageHandling _errorMessageHandling;

        #endregion Fields

        #region Constructors

        public SocketClient(IErrorMessageHandling errorMessageHandling)
        {
            _errorMessageHandling = errorMessageHandling;
            _errorMessageHandling.AddToConnectionLogEvent += (sender, s) => AddToConnectionLog(s);
        }

        #endregion Constructors

        public event EventHandler<string> ConnectionLogUpdated;

        #region Properties

        public WebsocketClient Client { get; set; }
        public ConnectData ConnectData { get; set; }
        public ConnectResult ConnectResult { get; set; } = new ConnectResult();
        public bool IsConnected => Client?.IsRunning ?? false;

        public bool IsClosingConnection { get; set; }

        public bool ClientHasBeenSetup { get; set; }
        public int PingCount { get; set; }
        public DateTime LastMessageSend { get; set; }
        public TimeSpan TimeSinceLastMsg => DateTime.Now.Subtract(LastMessageSend);
        #endregion Properties


        public event EventHandler<bool> IsReconnecting;

        #region Methods

        #region Connection

        public bool SetupConnection(ConnectData connectData, bool shouldReconnect = true)
        {
            ConnectData = connectData;
            Client = new WebsocketClient(ConnectData.Uri)
            {
                ReconnectTimeoutMs = (int)TimeSpan.FromSeconds(60).TotalMilliseconds,
                IsReconnectionEnabled = shouldReconnect
            };

            Client.ReconnectionHappened.Subscribe(type =>
            {
                AddToConnectionLog($"Reconnection happening with type: {type}");

                switch (type)
                {
                    case ReconnectionType.Initial:
                        IsReconnecting?.Invoke(this, false);
                        break;

                    case ReconnectionType.Lost:
                        IsReconnecting?.Invoke(this, true);
                        break;
                }
            });

            Client.DisconnectionHappened
                .Subscribe(async type =>
                {
                    AddToConnectionLog("Connection was disconnected by server");
                    await CloseConnection();
                });

            Client.MessageReceived
                .Subscribe(msg =>
                {
                    if (IsClosingConnection) { return; }

                    MessageReceived(msg.Text);
                });

            // Apply connection settings
            Client.MessageReceived
                .Where(x => x?.Text != null && x.Text.StartsWith("0{\"sid\":"))
                .Subscribe(msg =>
                {
                    if (IsClosingConnection) { return; }

                    ParseSocketResponse(msg.Text);
                });

            SetupPingWorker();

            // TODO This does not work with multiple concurrent connections with TW2
            Observable
                .FromEventPattern<ConnectResult>(
                    ev => DataEvents.ConnectionResult += ev,
                    ev => DataEvents.ConnectionResult -= ev)
                .Subscribe(x =>
                {
                    ConnectionResult.TrySetResult(x.EventArgs);
                });

            ClientHasBeenSetup = true;
            return true;
        }

        private void MessageReceived(string response)
        {
            AddToConnectionLog($"Message received: {response}");

            _errorMessageHandling.ParseResponseAsync(response);
        }

        public void SetupPingWorker()
        {
            // Start ping worker if there has been no message send for the last ping interval seconds. 
            LastMsgSendTimer.AutoReset = true;
            LastMsgSendTimer.Enabled = true;
            LastMsgSendTimer.Elapsed += async (sender, args) =>
            {

                if (TimeSinceLastMsg.TotalMilliseconds >= _pingTimeout && !_firstPingSend)
                {
                    _firstPingSend = true;
                    await SendMessageAsync(SocketMessage.ToPing());
                    PingCount++;
                    return;
                }


                if (TimeSinceLastMsg.TotalMilliseconds >= _pingInterval)
                {
                    if (Client.IsRunning)
                    {
                        await SendMessageAsync(SocketMessage.ToPing());
                        PingCount++;
                    }
                }
            };

        }

        public async Task<ConnectResult> StartConnectionAsync(bool keepAlive = true)
        {
            _keepAlive = keepAlive;
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

            if (_keepAlive)
            {
                LastMessageSend = DateTime.Now;
                LastMsgSendTimer.Start();
            }

            await Client.Start();

            return new ConnectResult
            {
                IsConnected = Client.IsRunning,
                Message = "Client is connected to TribalWars2"
            };

        }
        /// <summary>
        /// Will close the current connection to TW2
        /// </summary>
        /// <returns>Was the connection closed successfully</returns>
        public async Task<bool> CloseConnection()
        {
            IsClosingConnection = true;

            if (_keepAlive)
            {
                LastMsgSendTimer.Stop();
            }

            if (IsConnected)
            {
                AddToConnectionLog("Closing connection");

                if (Client != null)
                {
                    var result = await Client?.Stop(WebSocketCloseStatus.NormalClosure, "Closed by command");
                }
            }
            else
            {
                return false;
            }

            PingCount = 0;
            ClientHasBeenSetup = false;

            DataEvents.InvokeConnectionStatus(false);

            IsClosingConnection = false;

            return true;
        }



        public async Task<bool> SendMessageAsync(SocketMessage message)
        {
            if (message.IsMessageValid && Client != null && Client.IsRunning)
            {
                await Client.Send(message.Message);

                // Check if the message was a ping message
                if (!message.IsMessagePing)
                {
                    _firstPingSend = false;
                }

                LastMessageSend = DateTime.Now;
                AddToConnectionLog($"Message Send:     {message.Message}");
                return true;

            }

            return false;
        }


        public async Task<SocketMessage> Emit(SocketMessage message)
        {
            if (!message.IsMessageValid)
            {
                message.IsMessageSendSuccessfully = false;
                return message;
            }

            if (message.IsResponseExpected)
            {
                var exitEvent = new ManualResetEvent(false);

                var disposable = Client.MessageReceived
                     .Where(msg => msg.Text.Contains($"\"id\":{message.Id}"))
                     .Subscribe(info =>
                     {
                         message.Response = info.Text;
                         exitEvent.Set();

                     });

                message.IsMessageSendSuccessfully = await SendMessageAsync(message);

                exitEvent.WaitOne(TimeSpan.FromSeconds(5));
                disposable.Dispose();
            }
            else
            {
                message.IsMessageSendSuccessfully = await SendMessageAsync(message);
            }

            return message;
        }


        #endregion

        public void AddToConnectionLog(string message)
        {
            lock (lockObj)
            {
                // Filter out the header
                string header = SocketUtilities.ParseHeaderStringFromResponse(message);
                if (!string.IsNullOrEmpty(header))
                {
                    message = message.Replace(header, string.Empty);
                }

                // Filter out the msg tag
                message = message.Replace("\"msg\",", string.Empty);

                // Limit the message length
                if (message.Length > 2000)
                {
                    message = message.Substring(0, 2000);
                }

                Log.Info(message);
                ConnectionLogUpdated.Invoke(this, message + Environment.NewLine);
            }
        }


        /// <summary>
        /// Used to determine the type of response received and to parse the initial connection configuration.
        /// </summary>
        /// <param name="response">The response of the websocket</param>
        /// <returns></returns>
        private void ParseSocketResponse(string response)
        {
            if (string.IsNullOrEmpty(response) || !response.StartsWith("0{\"sid\":")) { return; }

            response = SocketUtilities.CleanResponse(response);

            SocketResponse socketResponse = new SocketResponse();

            JsonSerializerSettings serializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            try
            {
                socketResponse = JsonConvert.DeserializeObject<SocketResponse>(response, serializerSettings);
            }
            catch (Exception e)
            {
                Log.Error(e, $"Could not deserialize the following string in {nameof(ParseSocketResponse)}: {Environment.NewLine} {response}");
            }

            if (!string.IsNullOrEmpty(socketResponse.SessionID))
            {
                _sessionId = socketResponse.SessionID;
            }

            // The pingTimeout and pingInterval are always send in the same message.
            if (socketResponse.PingTimeout > 1000 && socketResponse.PingInterval > 1000)
            {
                _pingTimeout = socketResponse.PingTimeout;
                _pingInterval = socketResponse.PingInterval;
            }

        }

        #endregion Methods
        public void Dispose()
        {
            LastMsgSendTimer.Stop();
            CloseConnection().Wait(5000);
        }
    }


}
