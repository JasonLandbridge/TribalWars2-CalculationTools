using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CalculationTools.WebSocket
{
    public static class RouteProvider
    {


        #region Fields

        public const string LOGIN = "Authentication/login";
        public const string LOGIN_SUCCESS = "Login/success";
        public const string SYSTEM_IDENTIFIED = "System/identified";
        public const string SYSTEM_IDENTIFY = "System/identify";
        public const string SYSTEM_WELCOME = "System/welcome";
        public const string SELECT_CHARACTER = "Authentication/selectCharacter";
        public const string CHARACTER_SELECTED = "Authentication/characterSelected";

        #endregion Fields

        #region Properties

        public static long UnixTime => new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();

        #endregion Properties

        #region Methods

        public static string Login(string name, string pass)
        {
            var jsonObject = new
            {
                id = 2,
                type = LOGIN,
                data = new
                {
                    name,
                    pass
                },
                headers = MsgHeader,
            };
            return AddMsg(JsonConvert.SerializeObject(jsonObject));
        }

        public static string SystemIdentify()
        {
            string fakeUserAgent = new Bogus.DataSets.Internet("en").UserAgent();

            var jsonObject = new
            {
                id = 1,
                type = SYSTEM_IDENTIFY,
                data = new
                {
                    platform = "browser",
                    api_version = "10.*.*",
                    device = fakeUserAgent
                },
            };
            return AddMsg(JsonConvert.SerializeObject(jsonObject));
        }
        public static string SelectCharacter(LoginDTO loginDto)
        {
            var jsonObject = new
            {
                id = 7,
                type = SELECT_CHARACTER,
                data = new
                {
                    id = loginDto.Data.PlayerId,
                    world_id = loginDto.Data.Characters[0].WorldId
                },
                headers = MsgHeader

            };
            return AddMsg(JsonConvert.SerializeObject(jsonObject));
        }
        #endregion Methods

        #region MessageHelpers

        public static object MsgHeader =>
            new
            {
                traveltimes = new List<List<object>>
                {
                    // Needs to be nested in [[]] JSON to work
                    new List<object>
                    {
                        "browser_send", UnixTime
                    }
                }
            };

        public static string AddMsg(string message)
        {
            return $"42[\"msg\",{message}]";
        }

        #endregion


    }

}
