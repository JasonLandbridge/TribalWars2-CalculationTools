using Newtonsoft.Json;
using System.Collections.Generic;

namespace CalculationTools.Common
{
    public class VillagesDTO
    {
        [JsonProperty("villages")]
        public IList<Village> Villages { get; set; }

        [JsonProperty("x")]
        public int X { get; set; }

        [JsonProperty("y")]
        public int Y { get; set; }
    }
}
