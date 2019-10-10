using NLog;
using System.Threading.Tasks;


namespace CalculationTools.WebSocket
{
    public static class WebSocketConnect
    {
        #region Fields


        private static Logger Log = LogManager.GetCurrentClassLogger();

        #endregion Fields



        #region Properties


        public static ConnectResult ConnectResult { get; set; } = new ConnectResult();

        #endregion Properties

        #region Methods

        public static async Task<bool> CheckLoginCredentialsAsync(ConnectData connectData)
        {
            SocketClient socketClient = await CreateSocketConnectAsync(connectData);

            return socketClient.IsConnected;
        }

        public static async Task<bool> LoginAsync(ConnectData connectData)
        {
            connectData.AccessToken = ConnectResult.AccessToken;
            SocketClient socketClient = await CreateSocketConnectAsync(connectData);
            return socketClient.IsConnected;
        }

        public static async Task<SocketClient> CreateSocketConnectAsync(ConnectData connectData)
        {
            SocketClient socketClient = new SocketClient();

            ConnectResult = await Task.Run(() => socketClient.StartConnectionAsync(connectData));

            return socketClient;
        }




        #endregion Methods
    }
}
