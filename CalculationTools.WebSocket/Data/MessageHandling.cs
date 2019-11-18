using CalculationTools.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CalculationTools.WebSocket
{
    public class MessageHandling : IMessageHandling
    {
        #region Fields

        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private readonly IDataManager _dataManager;
        private readonly ISocketManager _socketManager;

        #endregion Fields

        #region Constructors

        public MessageHandling(ISocketManager socketManager, IDataManager dataManager)
        {
            _socketManager = socketManager;
            _dataManager = dataManager;
        }

        #endregion Constructors

        private ConnectData ConnectData => _socketManager.ConnectData;

        #region Methods

        /// <summary>
        /// A helper function which uses the default format for sending messages to the server
        /// </summary>
        /// <param name="sendType">The message type</param>
        public void SendDefaultMessage(string sendType, object dataObject = null)
        {
            _socketManager.SendMessageAsync(RouteProvider.GetDefaultSendMessage(sendType, dataObject));
        }

        #region ParseMethods
        public async void ParseResponseAsync(string response)
        {
            if (string.IsNullOrEmpty(response))
            {
                Log.Info("The response was null/empty");
                return;
            }

            // open connection or ping response
            if (response == "40" || response == "3")
            {
                return;
            }


            response = CleanResponse(response);

            SocketResponse socketResponse = ParseSocketResponse(response);

            switch (socketResponse.SocketType)
            {
                case RouteProvider.SID:
                    // First introduction message

                    break;

                case RouteProvider.SYSTEM_WELCOME:
                    // On system welcome send system identify
                    SendDefaultMessage(RouteProvider.SYSTEM_IDENTIFY, RouteProvider.SystemIdentify());
                    break;

                case RouteProvider.SYSTEM_IDENTIFIED:
                    // Once system has been identified then send login credentials
                    SendDefaultMessage(RouteProvider.LOGIN, RouteProvider.Login(ConnectData));
                    break;

                case RouteProvider.LOGIN_SUCCESS:
                    var loginDto = ParseLoginSuccess(response);

                    object dataObject = RouteProvider.SelectCharacter(loginDto, ConnectData);
                    SendDefaultMessage(RouteProvider.SELECT_CHARACTER, dataObject);
                    break;

                case RouteProvider.CHARACTER_SELECTED:
                    var charSelected = ParseDataFromResponse<CharacterSelectedDTO>(response);

                    _dataManager.SetActiveCharacterId(charSelected.Id);
                    _dataManager.SetActiveWorldId(charSelected.WorldId);
                    _dataManager.SetConnectionStatus(true);

                    SendDefaultMessage(RouteProvider.GET_GAME_DATA);
                    SendDefaultMessage(RouteProvider.GET_GROUPS);
                    break;

                case RouteProvider.GROUPS:

                    var groupsData = ParseDataFromResponse<GroupsDTO>(response);
                    // _playerData.SetGroups(groupsData.ToIGroupList());

                    SendDefaultMessage(RouteProvider.GET_VILLAGES);
                    SendDefaultMessage(RouteProvider.PREMIUM_LIST_ITEMS);
                    break;

                case RouteProvider.GAME_DATA:
                    //TODO This return object is huge and contains all game metrics
                    break;

                case RouteProvider.ICON_VILLAGES:
                    //TODO This returns an object containing icon codes

                    break;

                case RouteProvider.PREMIUM_ITEMS:
                    //TODO This returns an object containing the usable premium items
                    SendDefaultMessage(RouteProvider.GLOBALINFORMATION_GETINFO);
                    break;

                case RouteProvider.GLOBALINFORMATION_INFO:
                    //TODO This returns an object containing the incoming support and attacks
                    var globalInfo = ParseDataFromResponse<GlobalInformationDTO>(response);
                    SendDefaultMessage(RouteProvider.EFFECT_GET_EFFECTS);
                    break;

                case RouteProvider.EFFECT_EFFECTS:
                    // Returns the current effect on the player, such as recruitment or resource boost
                    SendDefaultMessage(RouteProvider.TRIBE_GET_OWN_INVITATIONS);

                    break;

                case RouteProvider.ACHIEVEMENT_PROGRESS:

                    break;

                case RouteProvider.TRIBE_OWN_INVITATIONS:
                    SendDefaultMessage(RouteProvider.WHEEL_GETEVENT);
                    break;

                case RouteProvider.WHEEL_EVENT:
                    SendDefaultMessage(RouteProvider.CHARACTER_GETCOLORS);
                    break;

                case RouteProvider.CHARACTER_COLORS:
                    SendDefaultMessage(RouteProvider.CHARACTER_GETINFO);
                    SendDefaultMessage(RouteProvider.TRIBESKILL_GETINFO);
                    SendDefaultMessage(RouteProvider.SYSTEM_GETTIME);
                    break;

                case RouteProvider.CHARACTER_INFO:
                    // All the character data of the logged in player
                    var characterData = ParseDataFromResponse<CharacterDataDTO>(response);
                    _dataManager.SetCharacterData(characterData);

                    // Add this to the last command send to keep alive
                    break;

                case RouteProvider.TRIBESKILL_INFO:

                    break;

                case RouteProvider.SYSTEM_TIME:

                    break;

                case RouteProvider.SYSTEM_ERROR:
                    ParseSystemError(response);
                    break;

                case RouteProvider.MESSAGE_ERROR:
                    var errorMessage = ParseDataFromResponse<ErrorMessageDTO>(response);
                    Log.Error($"Socket Error: {errorMessage.ErrorCode} - {errorMessage.Message}");
                    await StopConnection();
                    break;

                case RouteProvider.EXCEPTION_ERROR:
                    var errorMessage2 = ParseDataFromResponse<SystemErrorDTO>(response);
                    Log.Error($"Server exception error: {errorMessage2.Message}");
                    AddToConnectionLog("THE SERVER IS MOST LIKELY DOWN!");
                    await StopConnection();
                    break;

                default:
                    AddToConnectionLog($"Received a response on which the parser defaulted: {socketResponse.SocketType}");
                    //ExitEvent.Set();
                    break;
            }
        }

        private static string CleanResponse(string response)
        {
            // Remove the Socket.io identifier string from the start
            var digits = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            response = response.TrimStart(digits);

            //Clean off the outer msg tag since 99% are messages anyway
            if (response.StartsWith("[\"msg\",") && response.EndsWith("]"))
            {
                response = response.Replace("[\"msg\",", "");
                response = response.Remove(response.Length - 1, 1);
            }

            return response;
        }

        /// <summary>
        /// Used to determine the type of response received and to parse the initial connection configuration.
        /// </summary>
        /// <param name="response">The response of the websocket</param>
        /// <returns></returns>
        private SocketResponse ParseSocketResponse(string response)
        {
            SocketResponse socketResponse = new SocketResponse();

            JsonSerializerSettings serializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            try
            {
                socketResponse = JsonConvert.DeserializeObject<SocketResponse>(response, serializerSettings);
            }
            catch (Exception e)
            {
                Log.Error(e, $"Could not deserialize the following string: {Environment.NewLine} {response}");
            }

            if (!string.IsNullOrEmpty(socketResponse.SessionID))
            {
                socketResponse.SocketType = RouteProvider.SID;
                ConnectResult connectResult = _socketManager.GetConnectResult();
                if (connectResult != null)
                {
                    connectResult.SessionID = socketResponse.SessionID;
                }
            }

            if (socketResponse.PingInterval > 0)
            {
                _socketManager.SetPingInterval(socketResponse.PingInterval);
            }

            return socketResponse;
        }

        /// <summary>
        /// This takes the data key from the Socket.io response and parses it to type <see cref="T"/>
        /// </summary>
        /// <typeparam name="T">The DTO class it will use to deserialize and to return</typeparam>
        /// <param name="response">The string to parse</param>
        /// <returns></returns>
        private T ParseDataFromResponse<T>(string response)
        {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            JObject jsonObject = (JObject)JsonConvert.DeserializeObject(response, serializerSettings);

            if (jsonObject["data"].Any())
            {
                try
                {
                    string data = jsonObject["data"].ToString();
                    return JsonConvert.DeserializeObject<T>(data, serializerSettings);
                }
                catch (Exception e)
                {
                    Log.Error(e, $"Could not parse type {typeof(T)} with data object in string:{Environment.NewLine}{response} - {e.Message}");
                }
            }

            Log.Error($"Could not find data object in JsonString: {response}");

            return default;
        }

        private LoginDataDTO ParseLoginSuccess(string response)
        {
            var loginData = ParseDataFromResponse<LoginDataDTO>(response);
            ConnectResult connectResult = _socketManager.GetConnectResult();

            connectResult.IsConnected = true;
            connectResult.AccessToken = loginData?.AccessToken;
            connectResult.TW2AccountId = loginData?.PlayerId;

            _socketManager.SetConnectionResult();
            // Send parsed data to the PlayerData to be stored
            _dataManager.SetLoginData(loginData);
            return loginData;
        }

        private void ParseSystemError(string response)
        {
            var systemError = ParseDataFromResponse<SystemErrorDTO>(response);

            if (systemError.Cause == RouteProvider.LOGIN)
            {
                ConnectResult connectResult = _socketManager.GetConnectResult();
                connectResult.IsConnected = false;
                connectResult.Message = systemError.Message;

                _socketManager.SetConnectionResult();
            }

        }

        #endregion

        #region SocketClient Helper Methods

        public async Task<bool> StopConnection()
        {
            _dataManager.SetConnectionStatus(false);

            return await _socketManager.StopConnection(true);
        }


        public void AddToConnectionLog(string message)
        {
            _socketManager.AddToConnectionLog(message);
        }

        #endregion

        #endregion Methods
    }
}
