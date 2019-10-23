using CalculationTools.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CalculationTools.WebSocket
{
    public static class RouteProvider
    {
        private static int _id;


        #region Fields

        public const string LOGIN = "Authentication/login";
        public const string LOGIN_SUCCESS = "Login/success";
        public const string SYSTEM_IDENTIFIED = "System/identified";
        public const string SYSTEM_IDENTIFY = "System/identify";
        public const string SYSTEM_ERROR = "System/error";
        public const string SYSTEM_WELCOME = "System/welcome";
        public const string MESSAGE_ERROR = "Message/error";
        public const string EXCEPTION_ERROR = "Exception/ErrorException";


        #region Send Types
        public const string SELECT_CHARACTER = "Authentication/selectCharacter";

        public const string GET_GAME_DATA = "GameDataBatch/getGameData";
        public const string GET_GROUPS = "Group/getGroups";
        public const string GET_VILLAGES = "Icon/getVillages";
        public const string PREMIUM_LIST_ITEMS = "Premium/listItems";
        public const string GLOBALINFORMATION_GETINFO = "GlobalInformation/getInfo";


        #endregion

        #region Receive Types
        public const string CHARACTER_SELECTED = "Authentication/characterSelected";

        public const string GROUPS = "Group/groups";
        public const string GAME_DATA = "GameDataBatch/gameData";
        public const string ICON_VILLAGES = "Icon/villages";
        public const string PREMIUM_ITEMS = "Premium/items";
        public const string GLOBALINFORMATION_INFO = "GlobalInformation/info";


        #endregion

        #endregion Fields

        #region Properties

        public static long UnixTime => new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();

        /// <summary>
        /// Returns an unique Id incrementing on every message send
        /// </summary>
        public static int Id
        {
            get
            {
                _id++;
                return _id;
            }
        }

        #endregion Properties

        #region Methods

        public static string Login(ConnectData connectData)
        {
            object data;
            if (!string.IsNullOrEmpty(connectData.AccessToken))
            {
                data = new
                {
                    name = connectData.Username,
                    token = connectData.AccessToken
                };
            }
            else
            {
                data = new
                {
                    name = connectData.Username,
                    pass = connectData.Password
                };
            }

            var jsonObject = new
            {
                id = Id,
                type = LOGIN,
                data,
                headers = MsgHeader,
            };
            return AddMsg(JsonConvert.SerializeObject(jsonObject));
        }

        public static string SystemIdentify()
        {
            string fakeUserAgent = new Bogus.DataSets.Internet("en").UserAgent();

            var jsonObject = new
            {
                id = Id,
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
        public static string SelectCharacter(LoginDataDTO loginDto)
        {
            var jsonObject = new
            {
                id = Id,
                type = SELECT_CHARACTER,
                data = new
                {
                    id = loginDto.PlayerId,
                    world_id = loginDto.Characters[0].WorldId
                },
                headers = MsgHeader

            };
            return AddMsg(JsonConvert.SerializeObject(jsonObject));
        }

        /// <summary>
        /// Sends a default message to the server with only a message type
        /// </summary>
        /// <param name="sendType">The message type</param>
        /// <returns>A json formatted message</returns>
        public static string GetDefaultSendMessage(string sendType)
        {

            var jsonObject = new
            {
                id = Id,
                type = sendType,
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


        public static string GetVillages()
        {
            var jsonObject = new
            {
                id = Id,
                type = GET_VILLAGES,
                headers = MsgHeader
            };
            return AddMsg(JsonConvert.SerializeObject(jsonObject));
        }

        public static string GetPremiumListItems()
        {
            var jsonObject = new
            {
                id = Id,
                type = PREMIUM_LIST_ITEMS,
                headers = MsgHeader
            };
            return AddMsg(JsonConvert.SerializeObject(jsonObject));
        }
    }

}
