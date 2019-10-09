using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CalculationTools.WebSocket
{
    public class LoginDTO
    {
        [JsonProperty("data")]
        public LoginDataDTO Data { get; set; }
    }
}
