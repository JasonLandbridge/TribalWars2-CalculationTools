using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalculationTools.Common;
using CalculationTools.Common.DTOs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;

namespace CalculationTools.WebSocket.Repository
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



        #region Methods
        public async Task<DateTime> GetSystemTimeAsync()
        {
            string message = RouteProvider.GetDefaultSendMessage(RouteProvider.SYSTEM_GETTIME);
            int? id = GetId(message);

            string response = await _socketManager.Emit(message, id ?? default);

            var systemTime = ParseDataFromResponse<SystemTimeDTO>(response);

            return DateTime.UnixEpoch.AddSeconds(systemTime.Time + systemTime.Offset);
        }

        public async Task<List<VillageDTO>> GetVillagesByAutocomplete(string nameToSearch)
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

            return autocompleteDto.Result.Village.ToList();
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
        #endregion
        #endregion Methods

    }
}
