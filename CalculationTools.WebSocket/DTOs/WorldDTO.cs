

using CalculationTools.Common;
using Newtonsoft.Json;

namespace CalculationTools.WebSocket
{
    public class WorldDTO : IWorld
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("full")]
        public bool Full { get; set; }

        [JsonProperty("recommended")]
        public int Recommended { get; set; }

        [JsonProperty("key_required")]
        public bool KeyRequired { get; set; }
    }
}