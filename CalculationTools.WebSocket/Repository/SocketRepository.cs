using CalculationTools.Common;
using CalculationTools.Common.DTOs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculationTools.WebSocket
{
    public class SocketRepository : ISocketRepository
    {
        #region Fields

        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private readonly ISocketManager _socketManager;

        #endregion Fields

        #region Constructors

        public SocketRepository(ISocketManager socketManager)
        {
            _socketManager = socketManager;
        }

        #endregion Constructors

        public event EventHandler<LoginDataDTO> LoginDataAvailable;

        #region Methods


        public async Task<bool> EstablishConnection(ConnectData connectData)
        {
            await _socketManager.StartConnection(connectData);

            await SendAccountAuthentication(connectData);
            await SendSystemIdentify();
            await SendCharacterSelect(connectData.SelectedCharacter);

            return true;
        }

        private async Task<bool> SendCharacterSelect(ICharacter selectedCharacter)
        {
            if (selectedCharacter == null) { return false; }

            object dataObject = RouteProvider.SelectCharacter(selectedCharacter);

            var response = await SendStandardMessage(RouteProvider.SELECT_CHARACTER, dataObject);
            var charSelected = ParseDataFromResponse<CharacterSelectedDTO>(response);

            _socketManager.ActiveCharacterId = charSelected.OwnerId;
            _socketManager.ActiveWorldId = charSelected.WorldId;

            return true;
        }

        public async Task<DateTime> GetSystemTimeAsync()
        {
            string message = RouteProvider.GetDefaultSendMessage(RouteProvider.SYSTEM_GETTIME);
            int? id = GetId(message);

            string response = await _socketManager.Emit(message, id ?? default);

            var systemTime = ParseDataFromResponse<SystemTimeDTO>(response);

            return DateTime.UnixEpoch.AddSeconds(systemTime.Time + systemTime.Offset);
        }

        public async Task<List<IVillage>> GetVillagesByAutocomplete(string nameToSearch)
        {

            object data = new VillageAutocompleteDTO
            {
                Types = new List<string>
                {
                    "village"
                },
                String = nameToSearch,
                Amount = 5
            };


            string message = RouteProvider.GetDefaultSendMessage(RouteProvider.AUTOCOMPLETION_AUTOCOMPLETE, data);
            int? id = GetId(message);

            string response = await _socketManager.Emit(message, id ?? default);

            var autocompleteDto = ParseDataFromResponse<AutocompleteDTO>(response);

            //Set the worldId for each village
            foreach (VillageDTO villageDto in autocompleteDto.Result.Village)
            {
                villageDto.WorldId = _socketManager.GetCurrentWorldId();
            }

            return autocompleteDto.Result.Village.ToList<IVillage>();
        }

        public async Task<bool> SendAccountAuthentication(ConnectData connectData)
        {
            string response = string.Empty;

            if (_socketManager.IsReconnecting)
            {
                response = await SendStandardMessage(
                    RouteProvider.AUTHENTICATION_RECONNECT,
                    RouteProvider.AuthenticationReconnect(connectData, _socketManager.ActiveCharacterId));
            }
            else
            {
                response = await SendStandardMessage(RouteProvider.LOGIN, RouteProvider.Login(connectData));
            }

            var loginData = ParseDataFromResponse<LoginDataDTO>(response);

            ConnectResult connectResult = _socketManager.GetConnectResult();

            connectResult.IsConnected = true;
            connectResult.AccessToken = loginData?.AccessToken;
            connectResult.TW2AccountId = loginData?.PlayerId;

            _socketManager.ConnectData.AccessToken = loginData?.AccessToken;

            LoginDataAvailable?.Invoke(this, loginData);
            DataEvents.InvokeConnectionResult(connectResult);

            return true;
        }


        public async Task<bool> SendSystemIdentify()
        {
            string response = string.Empty;

            response = await SendStandardMessage(RouteProvider.SYSTEM_IDENTIFY, RouteProvider.SystemIdentify());

            return true;
        }

        #region Helpers
        public int? GetId(string message)
        {
            const string idStr = "\"id\":";

            if (message.Contains(idStr))
            {
                int startIndex = message.IndexOf(idStr, StringComparison.Ordinal) + idStr.Length;
                int endIndex = message.IndexOf(',', startIndex, 10);

                if (startIndex > -1 && endIndex > -1)
                {
                    string value = message.Substring(startIndex, endIndex - startIndex);

                    if (value == "null")
                    {
                        return null;
                    }

                    if (value.All(char.IsDigit))
                    {
                        bool result = int.TryParse(value, out int intResult);

                        if (intResult > 0)
                        {
                            return intResult;
                        }
                    }
                }
            }
            return null;
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

        public static T ParseDataFromResponse<T>(string response)
        {
            if (string.IsNullOrEmpty(response)) { return default; }

            response = CleanResponse(response);

            JsonSerializerSettings serializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            try
            {
                JObject jsonObject = (JObject)JsonConvert.DeserializeObject(response, serializerSettings);
                if (jsonObject["data"].Any())
                {
                    string data = jsonObject["data"].ToString();
                    return JsonConvert.DeserializeObject<T>(data, serializerSettings);
                }
            }
            catch (Exception e)
            {
                Log.Error(e, $"Could not parse type {typeof(T)} with data object in string:{Environment.NewLine}{response} - {e.Message}");
            }


            Log.Error($"Could not find data object in JsonString: {response}");

            return default;
        }

        /// <summary>
        /// A helper function which uses the default format for sending messages to the server
        /// </summary>
        /// <param name="sendType">The message type</param>
        public async Task<string> SendStandardMessage(string sendType, object dataObject = null)
        {
            return await SendMessageAsync(RouteProvider.GetDefaultSendMessage(sendType, dataObject));
        }

        /// <summary>
        /// Will send a message to TW2 and await the response to that message. 
        /// </summary>
        /// <param name="message">The message to be send</param>
        /// <returns>The response to that message from TW2</returns>
        private async Task<string> SendMessageAsync(string message)
        {
            int? id = GetId(message);

            return await _socketManager.Emit(message, id ?? default);

        }
        #endregion
        #endregion Methods

    }
}
