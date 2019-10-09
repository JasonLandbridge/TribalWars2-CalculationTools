using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;

namespace CalculationTools.WebSocket
{
    public class SocketResponse
    {
        [JsonProperty("msg")]
        public string HeaderType { get; set; }

        [JsonProperty("sid")]
        public string SessionID { get; set; }

        [JsonProperty("type")]
        public string SocketType { get; set; }

        [JsonProperty("upgrades")]
        public List<string> Upgrades { get; set; }

        [JsonProperty("pingInterval")]
        public int PingInterval { get; set; }
        [JsonProperty("pingTimeout")]
        public int PingTimeout { get; set; }

        [JsonProperty("data")]
        public SocketData SocketData { get; set; }


    }

    public class SocketData
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("transport")]
        public string Transport { get; set; }

        [JsonProperty("host")]
        public string Host { get; set; }

        [JsonProperty("maintenance")]
        public bool Maintenance { get; set; }

    }
}
