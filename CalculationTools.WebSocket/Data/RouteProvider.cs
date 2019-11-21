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
        public const string SYSTEM_ERROR = "System/error";
        public const string MESSAGE_ERROR = "Message/error";
        public const string EXCEPTION_ERROR = "Exception/ErrorException";


        #region Send Types
        public const string SYSTEM_IDENTIFY = "System/identify";
        public const string AUTHENTICATION_RECONNECT = "Authentication/reconnect";


        public const string SELECT_CHARACTER = "Authentication/selectCharacter";

        public const string GET_GAME_DATA = "GameDataBatch/getGameData";
        public const string GET_GROUPS = "Group/getGroups";
        public const string GET_VILLAGES = "Icon/getVillages";
        public const string PREMIUM_LIST_ITEMS = "Premium/listItems";
        public const string GLOBALINFORMATION_GETINFO = "GlobalInformation/getInfo";
        public const string EFFECT_GET_EFFECTS = "Effect/getEffects";
        public const string TRIBE_GET_OWN_INVITATIONS = "TribeInvitation/getOwnInvitations";
        public const string WHEEL_GETEVENT = "WheelEvent/getEvent";
        public const string WHEEL_GETPROGRESS = "WheelEvent/getProgress";
        public const string CHARACTER_GETCOLORS = "Character/getColors";
        public const string CHARACTER_GETINFO = "Character/getInfo";
        public const string TRIBESKILL_GETINFO = "TribeSkill/getInfo";
        public const string SYSTEM_GETTIME = "System/getTime";

        public const string DAILYLOGINBONUS_GETINFO = "DailyLoginBonus/getInfo";
        public const string QUEST_GETQUESTLINES = "Quest/getQuestLines";
        public const string MAP_GETVILLAGESBYAREA = "Map/getVillagesByArea";

        public const string AUTOCOMPLETION_AUTOCOMPLETE = "Autocompletion/autocomplete";

        #endregion

        #region Receive Types
        public const string SID = "sid";
        public const string SYSTEM_WELCOME = "System/welcome";

        public const string CHARACTER_SELECTED = "Authentication/characterSelected";
        public const string GROUPS = "Group/groups";
        public const string GAME_DATA = "GameDataBatch/gameData";
        public const string ICON_VILLAGES = "Icon/villages";
        public const string PREMIUM_ITEMS = "Premium/items";
        public const string GLOBALINFORMATION_INFO = "GlobalInformation/info";
        public const string EFFECT_EFFECTS = "Effect/effects";
        public const string ACHIEVEMENT_PROGRESS = "Achievement/progress";
        public const string TRIBE_OWN_INVITATIONS = "TribeInvitation/ownInvitations";
        public const string WHEEL_EVENT = "WheelEvent/event";
        public const string CHARACTER_COLORS = "Character/colors";
        public const string WHEELEVENT_PROGRESS = "WheelEvent/progress";

        public const string CHARACTER_INFO = "Character/info";
        public const string TRIBESKILL_INFO = "TribeSkill/info";
        public const string SYSTEM_TIME = "System/time";

        public const string AUTOCOMPLETION_RESULTS = "Autocomplete/results";


        #region ErrorMessages

        public const string CHARACTER_SELECTION_FAILED = "Authentication/characterSelectionFailed";


        #endregion

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

        public static object Login(ConnectData connectData)
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

            return data;
        }

        public static object SystemIdentify()
        {
            string fakeUserAgent = new Bogus.DataSets.Internet("en").UserAgent();
            return new
            {
                platform = "browser",
                api_version = "10.*.*",
                device = fakeUserAgent
            };
        }


        public static object AuthenticationReconnect(ConnectData connectData, int characterId)
        {
            return new
            {
                character = characterId,
                name = connectData.Username,
                token = connectData.AccessToken,
                world = connectData.WorldID,
            };
        }
        public static object SelectCharacter(ICharacter selectedCharacter)
        {
            if (selectedCharacter == null)
            {
                return null;
            }

            return new
            {
                id = selectedCharacter.CharacterId,
                world_id = selectedCharacter.WorldId
            };

        }

        /// <summary>
        /// Sends a default message to the server with only a message type
        /// </summary>
        /// <param name="sendType">The message type</param>
        /// <returns>A json formatted message</returns>
        public static string GetDefaultSendMessage(string sendType, object dataObject = null)
        {

            object jsonObject;

            if (dataObject != null)
            {
                jsonObject = new
                {
                    id = Id,
                    type = sendType,
                    data = dataObject,
                    headers = MsgHeader
                };
            }
            else
            {
                jsonObject = new
                {
                    id = Id,
                    type = sendType,
                    headers = MsgHeader
                };
            }


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
