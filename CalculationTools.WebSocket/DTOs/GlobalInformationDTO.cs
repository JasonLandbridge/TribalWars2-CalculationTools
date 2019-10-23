using Newtonsoft.Json;
using System.Collections.Generic;

namespace CalculationTools.WebSocket
{
    public class GlobalInformationDTO
    {
        [JsonProperty("incoming_commands")]
        public IList<IncomingCommandDTO> IncomingCommands { get; set; }

    }
}
