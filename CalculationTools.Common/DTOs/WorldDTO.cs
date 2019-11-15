using Newtonsoft.Json;

namespace CalculationTools.Common
{
    public class WorldDTO : IWorld
    {
        [JsonProperty("id")]
        public string WorldId { get; set; }

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