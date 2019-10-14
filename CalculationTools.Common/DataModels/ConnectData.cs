

using System;

namespace CalculationTools.Common
{
    public class ConnectData
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string ServerCountryCode { get; set; }

        public Uri Uri => new Uri($"wss://{ServerCountryCode}.tribalwars2.com/socket.io/?platform=desktop&EIO=3&transport=websocket");

        public string AccessToken { get; set; }

    }
}
