using Newtonsoft.Json;
using System.Collections.Generic;

namespace CalculationTools.Common.DTOs
{
    public class AutocompleteDTO
    {
        [JsonProperty("search")]
        public string Search { get; set; }

        [JsonProperty("types")]
        public IList<string> Types { get; set; }

        [JsonProperty("result")]
        public ResultDTO Result { get; set; }
    }
}
