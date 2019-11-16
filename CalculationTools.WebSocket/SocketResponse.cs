using Newtonsoft.Json;
using System.Collections.Generic;

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

    }
}
