using Newtonsoft.Json;

namespace CalculationTools.WebSocket
{
    public class ErrorMessageDTO
    {
        [JsonProperty("error_code")]
        public string ErrorCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
