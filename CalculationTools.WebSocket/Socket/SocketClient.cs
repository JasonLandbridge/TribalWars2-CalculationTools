using CalculationTools.Common;
using NLog;
using NLog.Fluent;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Websocket.Client;

namespace CalculationTools.WebSocket
{
    public class SocketClient : BasePropertyChanged
    {

        #region Fields

        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private readonly IDataManager _dataManager;
        private readonly IMessageHandling _messageHandling;
        private readonly IPlayerData _playerData;

        private readonly TaskCompletionSource<ConnectResult> ConnectionResult = new TaskCompletionSource<ConnectResult>();

        private static readonly object lockObj = new Object();

        private bool _keepAlive;
        private int _pingInterval = 1000;
        private static readonly Timer LastMsgSendTimer = new Timer(1000);


        #endregion Fields

        #region Constructors

        public SocketClient(
            IPlayerData playerData,
            IDataManager dataManager,
            IMessageHandling messageHandling)
        {
            _playerData = playerData;
            _dataManager = dataManager;
            _messageHandling = messageHandling;
        }

        #endregion Constructors

        public event EventHandler ConnectionLogUpdated;

        #region Properties

        public WebsocketClient Client { get; set; }
        public ConnectData ConnectData { get; set; }
        public ConnectResult ConnectResult { get; set; } = new ConnectResult();
        public bool IsConnected => Client?.IsRunning ?? false;
        public StringBuilder ConnectionLog { get; set; } = new StringBuilder();

        public bool ClientHasBeenSetup { get; set; }
        public int PingCount { get; set; }
        public DateTime LastMessageSend { get; set; }
        public TimeSpan TimeSinceLastMsg => DateTime.Now.Subtract(LastMessageSend);
        #endregion Properties

        #region Methods

        #region Connection

        public bool SetupConnection(ConnectData connectData, bool shouldReconnect = true)
        {
            ConnectData = connectData;
            Client = new WebsocketClient(ConnectData.Uri)
            {
                ReconnectTimeoutMs = (int)TimeSpan.FromSeconds(60).TotalMilliseconds
            };
            Client.IsReconnectionEnabled = shouldReconnect;

            Client.ReconnectionHappened.Subscribe(type =>
            {

                Log.Info($"Reconnection happened, type: {type}");
                // TODO Reconnect with command from TW2
                // Authentication/reconnect
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
                _messageHandling.ParseResponseAsync(msg.Text);
            });

            SetupPingWorker();

            ClientHasBeenSetup = true;
            return true;
        }

        public void SetupPingWorker()
        {
            // Start ping worker if there has been no message send for the last ping interval seconds. 
            LastMsgSendTimer.AutoReset = true;
            LastMsgSendTimer.Enabled = true;
            LastMsgSendTimer.Elapsed += async (sender, args) =>
            {
                if (TimeSinceLastMsg.TotalMilliseconds >= _pingInterval)
                {
                    if (Client.IsRunning)
                    {
                        await SendMessageAsync("2");
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
                LastMsgSendTimer.Start();
            }

            await Client.Start();


            return await ConnectionResult.Task;

        }
        /// <summary>
        /// Will close the current connection to TW2
        /// </summary>
        /// <returns>Was the connection closed successfully</returns>
        public async Task<bool> CloseConnection()
        {

            if (IsConnected)
            {
                Log.Info("Closing connection!");
                AddToConnectionLog("Closing connection");

                if (_keepAlive)
                {
                    LastMsgSendTimer.Stop();
                }

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
            _dataManager.SetConnectionStatus(false);
            return true;
        }



        public async Task<bool> SendMessageAsync(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (Client != null && Client.IsRunning)
                {
                    string log = $"Message Send:     {message}";
                    AddToConnectionLog(log);
                    Log.Info(log);

                    await Client.Send(message);

                    LastMessageSend = DateTime.Now;
                    return true;
                }
            }

            return false;
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

        public void AddToConnectionLog(string message)
        {
            lock (lockObj)
            {
                if (message.Length > 2000)
                {
                    message = message.Substring(0, 2000);
                }

                ConnectionLog.Append(message + Environment.NewLine);
                ConnectionLogUpdated.Invoke(this, EventArgs.Empty);
            }
        }

        public void SetPingInterval(int pingInterval)
        {
            if (pingInterval >= 1000)
            {
                _pingInterval = pingInterval;
                LastMsgSendTimer.Interval = _pingInterval;
            }
        }


        public bool SetConnectionResult()
        {
            return ConnectionResult.TrySetResult(ConnectResult);
        }
        #endregion Methods

    }


}
