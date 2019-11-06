using Newtonsoft.Json;
using System.Collections.Generic;

namespace CalculationTools.Common
{
    public class CharacterSelectedDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("world_id")]
        public string WorldId { get; set; }

        [JsonProperty("map_name")]
        public string MapName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("owner_id")]
        public int OwnerId { get; set; }

        [JsonProperty("owner_name")]
        public string OwnerName { get; set; }

        [JsonProperty("tribe_id")]
        public int TribeId { get; set; }

        [JsonProperty("tribe_rights")]
        public IList<string> TribeRights { get; set; }
    }
}
