using System;
using System.Text;
using System.Threading.Tasks;

namespace CalculationTools.Common
{
    public interface ISocketManager
    {
        Task<ConnectResult> StartConnection(ConnectData connectData, bool useAccessToken = true);

        Task<bool> StopConnection(bool deleteConnection = false);
        StringBuilder ConnectionLog { get; }

        /// <summary>
        /// The last used connection credentials. This will be null is connection was deleted.
        /// </summary>
        ConnectData ConnectData { get; set; }

        bool IsConnected { get; }

        /// <summary>
        /// Once the SocketClient has logged in then the SocketClient will be re-connecting instead of starting a new connection. 
        /// </summary>
        bool IsReconnecting { get; set; }

        int ActiveCharacterId { get; set; }
        string ActiveWorldId { get; set; }

        event EventHandler ConnectionLogUpdated;
        void ClearConnectionLog();
        Task<bool> SendMessageAsync(string message);
        void AddToConnectionLog(string message);
        ConnectResult GetConnectResult();
        Task<string> Emit(string message, int id);
        string GetCurrentWorldId();
    }
}