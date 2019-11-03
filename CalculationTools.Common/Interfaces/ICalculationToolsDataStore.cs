using System.Collections.Generic;

namespace CalculationTools.Common
{
    public interface ICalculationToolsDataStore
    {
        void UpdateVillages(List<IVillage> villages);
    }
}