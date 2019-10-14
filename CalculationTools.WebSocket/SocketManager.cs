using CalculationTools.Common;
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

        public async Task<bool> LoginAsync(ConnectData connectData, bool useAccessToken = true)
        {
            if (useAccessToken)
            {
                connectData.AccessToken = ConnectResult.AccessToken;
            }

            ConnectResult = await GetSocketClient(connectData).StartConnectionAsync();

            return SocketClient.IsConnected;
        }

        public SocketClient GetSocketClient(ConnectData connectData)
        {
            return SocketClient ??= new SocketClient(connectData, _playerData);
        }



        #endregion Methods
    }
}
