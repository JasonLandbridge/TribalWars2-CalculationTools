using System.Threading.Tasks;

namespace CalculationTools.Common
{
    public interface ISocketManager
    {
        Task<bool> LoginAsync(ConnectData connectData, bool useAccessToken = true);
    }
}