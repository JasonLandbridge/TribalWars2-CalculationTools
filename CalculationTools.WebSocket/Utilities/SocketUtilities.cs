using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.Linq;

namespace CalculationTools.WebSocket
{
    public static class SocketUtilities
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public static int? GetId(string message)
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

        public static string CleanResponse(string response)
        {

            response = response.Replace("Message received: ", string.Empty);
            response = response.Replace("Message Send:     ", string.Empty);

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
                    string data = jsonObject["data"].ToString(Formatting.None);
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

        public static string ParseHeaderStringFromResponse(string response)
        {
            if (string.IsNullOrEmpty(response) || !response.Contains("\"headers\":")) { return string.Empty; }

            response = CleanResponse(response);

            JsonSerializerSettings serializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };

            try
            {
                var jsonObject = (JObject)JsonConvert.DeserializeObject(response, serializerSettings);
                if (jsonObject["headers"].Any())
                {
                    return jsonObject["headers"].ToString(Formatting.None);
                }
            }
            catch (Exception e)
            {
                Log.Error(e, $"Could not parse header from string:{Environment.NewLine}{response} - {e.Message}");
            }

            Log.Error($"Could not find header object in JsonString: {response}");

            return string.Empty;
        }
    }
}
