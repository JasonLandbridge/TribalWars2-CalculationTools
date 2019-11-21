using Newtonsoft.Json;

namespace CalculationTools.Common
{
    public class SystemTimeDTO
    {
        [JsonProperty("time")]
        public int Time { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }
    }
}
