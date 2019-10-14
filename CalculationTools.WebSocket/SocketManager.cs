using CalculationTools.Common;
using CalculationTools.Common.Connection;
using CalculationTools.Common.Data;
using NLog;
using System.Threading.Tasks;


namespace CalculationTools.WebSocket
{
    public class SocketManager : ISocketManager
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

        private SocketClient SocketClient { get; set; }

        #endregion Properties

        #region Methods

        public SocketClient GetSocketClient()
        {
            return SocketClient ??= new SocketClient(_playerData);
        }

        public async Task<ConnectResult> TestConnection(ConnectData connectData)
        {
            ConnectResult = await GetSocketClient().TestConnectionAsync(connectData);
            return ConnectResult;

        }

        public async Task<ConnectResult> StartConnection(ConnectData connectData, bool useAccessToken = true)
        {
            if (useAccessToken)
            {
                connectData.AccessToken = ConnectResult.AccessToken;
            }

            await GetSocketClient().SetupConnectionAsync(connectData);
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

        #endregion Methods
    }
}
