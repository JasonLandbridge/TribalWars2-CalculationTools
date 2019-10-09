using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CalculationTools.WebSocket
{
    public static class RouteProvider
    {


        public static long UnixTime => new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();

        public const string SYSTEM_WELCOME = "System/welcome";
        public const string SYSTEM_IDENTIFY = "System/identify";
        public const string SYSTEM_IDENTIFIED = "System/identified";
        public const string LOGIN = "Authentication/login";

        public static string SystemIdentify()
        {
            var jsonObject = new
            {
                type = SYSTEM_IDENTIFY,
                data = new
                {
                    platform = "browser",
                    api_version = "10.*.*",
                    device = "Mozilla/5.0%20(Windows%20NT%2010.0;%20Win64;%20x64)%20AppleWebKit/537.36%20(KHTML,%20like%20Gecko)%20Chrome/77.0.3865.90%20Safari/537.36"
                },
                id = 1
            };
            return JsonConvert.SerializeObject(jsonObject);
        }

        public static string Login(string name, string pass)
        {
            var jsonObject = new
            {
                type = LOGIN,
                data = new
                {
                    name,
                    pass
                },
                id = 2,
                headers = new
                {
                    // Needs to be nested in [[]] JSON to work
                    traveltimes = new List<List<object>>
                    {
                        new List<object>{
                            "browser_send", UnixTime
                        }
                    }
                },
            };
            return JsonConvert.SerializeObject(jsonObject);
        }

        public static string AddMsg(string message)
        {
            return $"42[\"msg\", {message}]";
        }


    }

}
