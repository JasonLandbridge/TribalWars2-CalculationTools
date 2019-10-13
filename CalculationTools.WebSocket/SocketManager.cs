using NLog;
using System.Threading.Tasks;


namespace CalculationTools.WebSocket
{
    public static class SocketManager
    {
        #region Fields


        private static Logger Log = LogManager.GetCurrentClassLogger();

        #endregion Fields



        #region Properties


        public static ConnectResult ConnectResult { get; set; } = new ConnectResult();

        private static SocketClient SocketClient { get; set; }

        #endregion Properties

        #region Methods

        public static async Task<bool> LoginAsync(ConnectData connectData, bool useAccessToken = true)
        {
            if (useAccessToken)
            {
                connectData.AccessToken = ConnectResult.AccessToken;
            }

            ConnectResult = await GetSocketClient(connectData).StartConnectionAsync(connectData);

            return SocketClient.IsConnected;
        }

        public static SocketClient GetSocketClient(ConnectData connectData)
        {
            return SocketClient ??= new SocketClient(connectData);
        }



        #endregion Methods
    }
}
