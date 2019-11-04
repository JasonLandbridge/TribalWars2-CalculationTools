using CalculationTools.Common;
using NLog;
using System;
using System.Text;
using System.Threading.Tasks;


namespace CalculationTools.WebSocket
{
    public class SocketManager : BasePropertyChanged, ISocketManager
    {
        private readonly IPlayerData _playerData;

        #region Fields


        private static Logger Log = LogManager.GetCurrentClassLogger();

        #endregion Fields

        public SocketManager(IPlayerData playerData)
        {
            _playerData = playerData;
        }

        #region Properties


        public ConnectResult ConnectResult { get; set; } = new ConnectResult();

        public StringBuilder ConnectionLog { get; set; } = new StringBuilder();

        private SocketClient SocketClient { get; set; }

        public event EventHandler ConnectionLogUpdated;

        #endregion Properties

        #region Methods

        public SocketClient GetSocketClient()
        {

            if (SocketClient == null)
            {
                SocketClient = new SocketClient(_playerData);
                SocketClient.ConnectionLogUpdated +=
                    (sender, args) =>
                    {
                        ConnectionLog = SocketClient.ConnectionLog;
                        ConnectionLogUpdated?.Invoke(this, EventArgs.Empty);
                    };
            }

            return SocketClient;
        }

        public async Task<ConnectResult> TestConnection(ConnectData connectData)
        {
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
            }

            return true;
        }

        public void ClearConnectionLog()
        {
            GetSocketClient().ConnectionLog.Clear();
        }


        #endregion Methods
    }
}
