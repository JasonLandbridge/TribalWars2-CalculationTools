using CalculationTools.Common;
using NLog;
using System;
using System.Text;
using System.Threading.Tasks;


namespace CalculationTools.WebSocket
{
    public class SocketManager : BasePropertyChanged, ISocketManager
    {

        #region Fields

        private static Logger Log = LogManager.GetCurrentClassLogger();
        private readonly IErrorMessageHandling _errorMessageHandling;

        #endregion Fields

        #region Constructors

        public SocketManager(IErrorMessageHandling errorMessageHandling)
        {
            _errorMessageHandling = errorMessageHandling;
            _errorMessageHandling.StopConnectionEvent += async (sender, args) => { await StopConnection(true); };
        }

        #endregion Constructors

        #region Events

        public event EventHandler ConnectionLogUpdated;

        #endregion Events

        #region Properties

        public int ActiveCharacterId { get; set; }

        public string ActiveWorldId { get; set; }

        /// <summary>
        /// The last used connection credentials. This will be null is connection was deleted.
        /// </summary>
        public ConnectData ConnectData { get; set; }
        public StringBuilder ConnectionLog { get; set; } = new StringBuilder();
        public ConnectResult ConnectResult { get; set; } = new ConnectResult();
        public bool IsConnected => GetSocketClient() != null && GetSocketClient().IsConnected;
        bool ISocketManager.IsConnected => IsConnected;

        /// <summary>
        /// Once the SocketClient has logged in then the SocketClient will be re-connecting instead of starting a new connection. 
        /// </summary>
        public bool IsReconnecting { get; set; }

        private SocketClient SocketClient { get; set; }

        #endregion Properties

        #region Methods

        public void AddToConnectionLog(string message)
        {
            GetSocketClient().AddToConnectionLog(message);
        }

        public void ClearConnectionLog()
        {
            ConnectionLog.Clear();
        }

        public async Task<string> Emit(string message, int id)
        {
            return await GetSocketClient().Emit(message, id);
        }

        public ConnectResult GetConnectResult()
        {
            if (GetSocketClient().IsConnected)
            {
                return GetSocketClient().ConnectResult;
            }
            return null;
        }

        public string GetCurrentWorldId()
        {
            return GetSocketClient().ConnectData.WorldID;

        }

        public SocketClient GetSocketClient()
        {

            if (SocketClient == null)
            {

                SocketClient = new SocketClient(_errorMessageHandling);
                SocketClient.ConnectionLogUpdated +=
                    (sender, args) =>
                    {
                        ConnectionLog.Append(args);
                        ConnectionLogUpdated?.Invoke(this, EventArgs.Empty);
                    };
                SocketClient.IsReconnecting += (sender, b) => { IsReconnecting = b; };

            }

            return SocketClient;
        }

        public async Task<bool> SendMessageAsync(string message)
        {
            return await GetSocketClient().SendMessageAsync(message);
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
            if (GetSocketClient() != null && IsConnected)
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

        #endregion Methods

    }
}
