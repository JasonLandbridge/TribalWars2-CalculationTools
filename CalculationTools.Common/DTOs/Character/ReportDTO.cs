using Newtonsoft.Json;

namespace CalculationTools.Common
{
    public class ReportDTO : IReport
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
