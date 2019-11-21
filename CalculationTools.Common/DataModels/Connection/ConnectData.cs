using System;

namespace CalculationTools.Common
{
    public class ConnectData
    {
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string ServerCountryCode { get; set; } = string.Empty;
        public string WorldID { get; set; } = string.Empty;

        public ICharacter SelectedCharacter { get; set; }

        public Uri Uri => new Uri($"wss://{ServerCountryCode}.tribalwars2.com/socket.io/?platform=desktop&EIO=3&transport=websocket");

        public string AccessToken { get; set; } = string.Empty;

    }
}
