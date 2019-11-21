using Newtonsoft.Json;
using System.Collections.Generic;

namespace CalculationTools.Common
{
    public class VillageAutocompleteDTO
    {
        [JsonProperty("types")]
        public IList<string> Types { get; set; }

        [JsonProperty("string")]
        public string String { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }
    }
}
