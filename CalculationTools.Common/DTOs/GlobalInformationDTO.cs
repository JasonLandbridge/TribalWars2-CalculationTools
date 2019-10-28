using Newtonsoft.Json;
using System.Collections.Generic;

namespace CalculationTools.Common
{
    public class GlobalInformationDTO
    {
        [JsonProperty("incoming_commands")]
        public IList<IncomingCommandDTO> IncomingCommands { get; set; }

    }
}
