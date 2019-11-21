﻿using System;
using System.Text;
using System.Threading.Tasks;

namespace CalculationTools.Common
{
    public interface ISocketManager
    {
        Task<ConnectResult> TestConnection(ConnectData connectData);
        Task<ConnectResult> StartConnection(ConnectData connectData, bool useAccessToken = true);

        Task<bool> StopConnection(bool deleteConnection = false);
        StringBuilder ConnectionLog { get; }

        /// <summary>
        /// The last used connection credentials. This will be null is connection was deleted.
        /// </summary>
        ConnectData ConnectData { get; set; }

        bool IsConnected { get; }

        event EventHandler ConnectionLogUpdated;
        void ClearConnectionLog();
        Task<bool> SendMessageAsync(string message);
        void AddToConnectionLog(string message);
        void SetPingSettings(int pingTimeout, int pingInterval);
        ConnectResult GetConnectResult();
        Task<string> Emit(string message, int id);
    }
}