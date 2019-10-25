using CalculationTools.Common;
using Newtonsoft.Json;

namespace CalculationTools.WebSocket
{
    public class ReportDTO : IReport
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
