using Newtonsoft.Json;

namespace CalculationTools.Common
{
    public class GroupDTO : IGroup
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("icon")]
        public int Icon { get; set; }

        [JsonProperty("character_id")]
        public int CharacterId { get; set; }
    }
}
