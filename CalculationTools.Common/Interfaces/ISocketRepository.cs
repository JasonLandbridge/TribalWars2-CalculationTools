using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CalculationTools.Common
{
    public interface ISocketRepository
    {
        Task<DateTime> GetSystemTimeAsync();
        Task<List<VillageDTO>> GetVillagesByAutocomplete(string nameToSearch);
    }
}