using CalculationTools.Common;
using System;

namespace CalculationTools.Data
{
    public static class DataEvents
    {
        public static event EventHandler VillagesUpdated;

        public static event EventHandler<ConnectResult> ConnectResultUpdated;


        public static void InvokeVillagesUpdated()
        {
            VillagesUpdated?.Invoke(null, EventArgs.Empty);
        }
    }
}
