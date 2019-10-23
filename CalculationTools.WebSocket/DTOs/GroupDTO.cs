﻿using CalculationTools.Common.Data;
using Newtonsoft.Json;

namespace CalculationTools.WebSocket
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
