using Newtonsoft.Json;

namespace CalculationTools.WebSocket
{
    public class IncomingCommandDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("target_village_id")]
        public int TargetVillageId { get; set; }

        [JsonProperty("time_completed")]
        public int TimeCompleted { get; set; }

    }
}
