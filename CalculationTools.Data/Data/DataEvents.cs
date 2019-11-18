using System;

namespace CalculationTools.Data
{
    public static class DataEvents
    {
        public static event EventHandler VillagesUpdated;

        public static event EventHandler<bool> ConnectionStatus;
        public static void InvokeVillagesUpdated()
        {
            VillagesUpdated?.Invoke(null, EventArgs.Empty);
        }

        public static void InvokeConnectionStatus(bool connectionStatus)
        {
            ConnectionStatus?.Invoke(null, connectionStatus);
        }
    }
}
