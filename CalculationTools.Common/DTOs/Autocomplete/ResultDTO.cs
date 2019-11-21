using Newtonsoft.Json;
using System.Collections.Generic;

namespace CalculationTools.Common
{
    public class ResultDTO
    {
        [JsonProperty("village")]
        public IList<VillageDTO> Village { get; set; }
    }
}
