using CalculationTools.Common;
using CalculationTools.Common.DTOs;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculationTools.WebSocket
{
    /// <summary>
    /// The only place that interacts with this repository is the GameDataRepository
    /// </summary>
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

        #region Events



        #endregion

        #region Methods


        public async Task<bool> EstablishConnection(Account account)
        {
            await LoginWithAccount(account);

            await Task.Delay(GetRandomDelay());

            await SendGetGameData();
            await SendGetGroups();

            await SendStandardMessage(RouteProvider.PREMIUM_LIST_ITEMS);
            await SendStandardMessage(RouteProvider.ICON_GETVILLAGES);

            await SendGetGlobalInformation();

            await SendStandardMessage(RouteProvider.EFFECT_GET_EFFECTS);

            await SendStandardMessage(RouteProvider.TRIBE_GET_OWN_INVITATIONS);

            await SendStandardMessage(RouteProvider.WHEEL_GETEVENT);
            await SendStandardMessage(RouteProvider.WHEEL_GETPROGRESS);

            await SendStandardMessage(RouteProvider.CHARACTER_GETCOLORS);

            await SendGetCharacterInfo();

            //TODO Request tribe data based on the active character

            await GetSystemTimeAsync();

            await SendStandardMessage(RouteProvider.INVITEPLATER_GETINFO);

            await SendStandardMessage(RouteProvider.AUTHENTICATION_COMPLETELOGIN);

            await _socketManager.StopConnection(true);
            return true;
        }


        /// <summary>
        /// Will attempt to login with the account provided, if in test-mode then the connection will be closed immediately after.
        /// </summary>
        /// <param name="account">The account with which to login</param>
        /// <param name="testMode">Will close the connection immediately after if true</param>
        /// <returns>The connection result</returns>
        public async Task<ConnectResult> LoginWithAccount(Account account, bool testMode = false)
        {
            // Make sure to reset the connection and deleting it.
            await _socketManager.StopConnection(true);

            ConnectResult result = await _socketManager.StartConnection(account.ToConnectData());

            if (!result.IsConnected) { return result; }

            result = await SendAccountAuthentication(account.ToConnectData());

            await SendSystemIdentify();

            result.IsConnected = await SendCharacterSelect(account.DefaultCharacter);

            if (testMode)
            {
                await _socketManager.StopConnection(true);
            }
            else
            {
                DataEvents.InvokeConnectionStatus(true);
            }

            return result;
        }

        /// <summary>
        /// This will request the incoming commands and the events on the player
        /// </summary>
        /// <returns></returns>
        private async Task<bool> SendGetGlobalInformation()
        {
            var response = await SendStandardMessage(RouteProvider.GLOBALINFORMATION_GETINFO);
            var globalInfo = SocketUtilities.ParseDataFromResponse<GlobalInformationDTO>(response);

            //TODO Store commands in DB
            return true;
        }

        private async Task<bool> SendGetGameData()
        {
            var response = await SendStandardMessage(RouteProvider.GET_GAME_DATA);

            // TODO Extract WorldConfig

            return !string.IsNullOrEmpty(response);
        }

        private async Task<bool> SendGetGroups()
        {
            var response = await SendStandardMessage(RouteProvider.GET_GROUPS);
            var groups = SocketUtilities.ParseDataFromResponse<GroupsDTO>(response)?.Groups?.ToList();

            DataEvents.InvokeGroupsDataAvailable(groups.ToList<IGroup>());
            return groups.Count > 0;
        }

        private async Task<bool> SendCharacterSelect(ICharacter selectedCharacter)
        {
            if (selectedCharacter == null) { return false; }

            object dataObject = RouteProvider.SelectCharacter(selectedCharacter);

            var response = await SendStandardMessage(RouteProvider.SELECT_CHARACTER, dataObject);
            var charSelected = SocketUtilities.ParseDataFromResponse<CharacterSelectedDTO>(response);

            _socketManager.ActiveCharacterId = charSelected.OwnerId;
            _socketManager.ActiveWorldId = charSelected.WorldId;

            return true;
        }

        private async Task<bool> SendGetCharacterInfo()
        {
            var response = await SendStandardMessage(RouteProvider.CHARACTER_GETINFO);

            var characterData = SocketUtilities.ParseDataFromResponse<CharacterDataDTO>(response);
            if (characterData != null)
            {
                foreach (IVillage village in characterData.Villages)
                {
                    village.CharacterId = _socketManager.ActiveCharacterId;
                    village.WorldId = _socketManager.ActiveWorldId;
                }

                DataEvents.InvokeCharacterDataAvailable(characterData);
            }

            return characterData != null;
        }


        public async Task<DateTime> GetSystemTimeAsync()
        {

            string response = await SendStandardMessage(RouteProvider.SYSTEM_GETTIME);

            var systemTime = SocketUtilities.ParseDataFromResponse<SystemTimeDTO>(response);

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

            string response = await SendStandardMessage(RouteProvider.AUTOCOMPLETION_AUTOCOMPLETE, data);

            var autocompleteDto = SocketUtilities.ParseDataFromResponse<AutocompleteDTO>(response);

            //Set the worldId for each village
            foreach (VillageDTO villageDto in autocompleteDto.Result.Village)
            {
                villageDto.WorldId = _socketManager.GetCurrentWorldId();
            }

            return autocompleteDto.Result.Village.ToList<IVillage>();
        }

        public async Task<ConnectResult> SendAccountAuthentication(ConnectData connectData)
        {
            string response;

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

            var loginData = SocketUtilities.ParseDataFromResponse<LoginDataDTO>(response);

            ConnectResult connectResult = _socketManager.GetConnectResult();

            connectResult.IsConnected = true;
            connectResult.AccessToken = loginData?.AccessToken;
            connectResult.TW2AccountId = loginData?.PlayerId;

            _socketManager.ConnectData.AccessToken = loginData?.AccessToken;

            DataEvents.InvokeLoginDataAvailable(loginData);
            DataEvents.InvokeConnectionResult(connectResult);

            return connectResult;
        }


        public async Task<List<IVillage>> SendGetVillagesByArea(int x, int y, int width = 25, int height = 25)
        {
            object dataObject = new
            {
                height,
                width,
                x,
                y,
            };

            var response = await SendStandardMessage(RouteProvider.MAP_GETVILLAGESBYAREA, dataObject);
            var villageList = SocketUtilities.ParseDataFromResponse<VillagesDTO>(response).Villages;

            return villageList.ToList<IVillage>();
        }

        public async Task<bool> SendSystemIdentify()
        {
            string response = await SendStandardMessage(
                RouteProvider.SYSTEM_IDENTIFY,
                RouteProvider.SystemIdentify());

            return true;
        }

        private int GetRandomDelay(int maxValue = 1000)
        {
            Random rnd = new Random();

            return rnd.Next(maxValue);
        }

        #region Helpers


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
            int? id = SocketUtilities.GetId(message);

            return await _socketManager.Emit(message, id ?? default);

        }
        #endregion
        #endregion Methods

    }
}
