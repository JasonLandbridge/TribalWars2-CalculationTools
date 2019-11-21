using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CalculationTools.Common
{
    public interface ISocketRepository
    {
        Task<DateTime> GetSystemTimeAsync();
        Task<List<IVillage>> GetVillagesByAutocomplete(string nameToSearch);
        Task<bool> EstablishConnection(ConnectData connectData);
        event EventHandler<LoginDataDTO> LoginDataAvailable;
    }
}