using CalculationTools.Common;
using NLog;
using System;
using System.Text;
using System.Threading.Tasks;


namespace CalculationTools.WebSocket
{
    public class SocketManager : BasePropertyChanged, ISocketManager
    {
        private readonly IDataManager _dataManager;

        #region Fields


        private static Logger Log = LogManager.GetCurrentClassLogger();

        #endregion Fields

        public SocketManager(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        #region Properties

        /// <summary>
        /// The last used connection credentials. This will be null is connection was deleted.
        /// </summary>
        public ConnectData ConnectData { get; set; }
        public ConnectResult ConnectResult { get; set; } = new ConnectResult();

        public StringBuilder ConnectionLog { get; set; } = new StringBuilder();

        private SocketClient SocketClient { get; set; }

        public event EventHandler ConnectionLogUpdated;

        public bool IsConnected => GetSocketClient() != null && GetSocketClient().IsConnected;

        #endregion Properties

        #region Methods

        public SocketClient GetSocketClient()
        {

            if (SocketClient == null)
            {
                MessageHandling messageHandling = new MessageHandling(this, _dataManager);

                SocketClient = new SocketClient(_dataManager, messageHandling);
                SocketClient.ConnectionLogUpdated +=
                    (sender, args) =>
                    {
                        ConnectionLog = SocketClient?.ConnectionLog;
                        ConnectionLogUpdated?.Invoke(this, EventArgs.Empty);
                    };
            }

            return SocketClient;
        }

        public async Task<ConnectResult> TestConnection(ConnectData connectData)
        {
            ConnectData = connectData;
            ConnectResult connectResult = await GetSocketClient().TestConnectionAsync(connectData);
            await StopConnection(true);
            return connectResult;

        }

        public async Task<ConnectResult> StartConnection(ConnectData connectData, bool useAccessToken = true)
        {
            if (useAccessToken)
            {
                connectData.AccessToken = ConnectResult?.AccessToken;
            }
            ConnectData = connectData;

            GetSocketClient().SetupConnection(connectData);
            ConnectResult = await GetSocketClient().StartConnectionAsync(useAccessToken);
            return ConnectResult;

        }

        public async Task<bool> StopConnection(bool deleteConnection = false)
        {
            if (GetSocketClient() != null && GetSocketClient().IsConnected)
            {
                await GetSocketClient().CloseConnection();
            }
            if (deleteConnection)
            {
                SocketClient = null;
                ConnectResult = null;
                ConnectData = null;
            }

            return true;
        }

        public void ClearConnectionLog()
        {
            GetSocketClient().ConnectionLog.Clear();
        }

        public async Task<bool> SendMessageAsync(string message)
        {
            return await GetSocketClient().SendMessageAsync(message);
        }

        public void AddToConnectionLog(string message)
        {
            GetSocketClient().AddToConnectionLog(message);
        }

        public void SetPingSettings(int pingTimeout, int pingInterval)
        {
            GetSocketClient().SetPingSettings(pingTimeout, pingInterval);
        }

        public ConnectResult GetConnectResult()
        {
            if (GetSocketClient().IsConnected)
            {
                return GetSocketClient().ConnectResult;
            }
            return null;
        }


        #endregion Methods
    }
}
