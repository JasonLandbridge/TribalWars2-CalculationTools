using Newtonsoft.Json;

namespace CalculationTools.Common
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

        /// <summary>
        /// The CharacterId which owns this village is not send with the response and has to be added separately
        /// </summary>
        [JsonIgnore]
        public int CharacterId { get; set; }
    }
}
