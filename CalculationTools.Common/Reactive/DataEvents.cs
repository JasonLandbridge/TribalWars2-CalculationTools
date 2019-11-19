using System;

namespace CalculationTools.Common
{
    public static class DataEvents
    {
        #region Events

        public static event EventHandler<ConnectResult> ConnectionResult;

        public static event EventHandler<bool> ConnectionStatus;

        public static event EventHandler LoginDataIsUpdated;

        public static event EventHandler VillagesUpdated;

        #endregion Events

        #region Methods

        public static void InvokeConnectionResult(ConnectResult connectionResult)
        {
            ConnectionResult?.Invoke(null, connectionResult);
        }

        public static void InvokeConnectionStatus(bool connectionStatus)
        {
            ConnectionStatus?.Invoke(null, connectionStatus);
        }

        public static void InvokeLoginDataIsUpdated()
        {
            LoginDataIsUpdated?.Invoke(null, EventArgs.Empty);
        }

        public static void InvokeVillagesUpdated()
        {
            VillagesUpdated?.Invoke(null, EventArgs.Empty);
        }

        #endregion Methods
    }
}
