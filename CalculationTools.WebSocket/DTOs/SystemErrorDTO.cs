using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CalculationTools.WebSocket
{
    public class SystemErrorDTO
    {
        [JsonProperty("cause")]
        public string Cause { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("details")]
        public IList<object> Details { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
