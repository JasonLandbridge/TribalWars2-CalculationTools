using System.Collections.Generic;

namespace CalculationTools.Common
{
    public interface IGameDataRepository
    {
        void UpdateVillages(List<IVillage> villages);
    }
}