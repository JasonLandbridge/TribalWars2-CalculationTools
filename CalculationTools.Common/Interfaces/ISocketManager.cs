using CalculationTools.Common.Connection;
using System.Threading.Tasks;

namespace CalculationTools.Common
{
    public interface ISocketManager
    {
        Task<ConnectResult> TestConnection(ConnectData connectData);
        Task<ConnectResult> StartConnection(ConnectData connectData, bool useAccessToken = true);

        Task<bool> StopConnection(bool deleteConnection = false);
    }
}