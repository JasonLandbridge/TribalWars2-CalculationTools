using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CalculationTools.WebSocket
{
    public class CharacterDTO
    {
        [JsonProperty("character_id")]
        public int CharacterId { get; set; }

        [JsonProperty("character_name")]
        public string CharacterName { get; set; }

        [JsonProperty("world_id")]
        public string WorldId { get; set; }

        [JsonProperty("world_name")]
        public string WorldName { get; set; }

        [JsonProperty("maintenance")]
        public bool Maintenance { get; set; }

        [JsonProperty("allow_login")]
        public bool AllowLogin { get; set; }

        [JsonProperty("character_owner_id")]
        public int CharacterOwnerId { get; set; }

        [JsonProperty("character_owner_name")]
        public string CharacterOwnerName { get; set; }

        [JsonProperty("key_required")]
        public bool KeyRequired { get; set; }
    }

}
