using CalculationTools.Common;
using Newtonsoft.Json;

namespace CalculationTools.WebSocket
{
    public class VillageDTO : IVillage
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("x")]
        public int X { get; set; }

        [JsonProperty("y")]
        public int Y { get; set; }
    }
}
