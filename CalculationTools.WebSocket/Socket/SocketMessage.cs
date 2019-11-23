namespace CalculationTools.WebSocket
{
    public class SocketMessage
    {
        public string Message { get; set; }
        public string Response { get; set; }

        /// <summary>
        /// The Id used for tracking the the socket message, a socket message being send always has a valid Id
        /// </summary>
        public int? Id { get; set; }

        public bool IsResponseExpected { get; set; } = true;

        public bool IsMessageSendSuccessfully { get; set; }

        public bool IsMessageValid => !string.IsNullOrEmpty(Message) && Id.HasValue;
        public bool IsResponseValid => !string.IsNullOrEmpty(Response);

        public bool IsMessagePing => Message == Ping;
        public bool IsResponsePing => Message == Pong;


        public SocketMessage()
        {

        }

        public SocketMessage(string message, bool responseExpected = true)
        {
            Message = message;
            IsResponseExpected = responseExpected;
        }


        public static string Ping => "2";
        public static string Pong => "3";

        public static SocketMessage ToPing()
        {
            return new SocketMessage(Ping);
        }
        public static SocketMessage ToPong()
        {
            return new SocketMessage(Pong);
        }

        public static SocketMessage FromRoute(string route, bool responseExpected = true)
        {
            return FromRoute(route, null, responseExpected);
        }

        public static SocketMessage FromRoute(string route, object data, bool responseExpected = true)
        {
            SocketMessage socketMessage = RouteProvider.GetDefaultSendMessage(route, data);
            socketMessage.IsResponseExpected = responseExpected;
            return socketMessage;
        }
    }
}
