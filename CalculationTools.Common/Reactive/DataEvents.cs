using System;
using System.Collections.Generic;

namespace CalculationTools.Common
{
    public static class DataEvents
    {
        #region Events

        public static event EventHandler<ConnectResult> ConnectionResult;

        public static event EventHandler<bool> ConnectionStatus;


        public static event EventHandler VillagesUpdated;

        /// <summary>
        /// Fired when the LoginData has been received by the server and is ready to be stored in the database.
        /// </summary>
        public static event EventHandler<ILoginData> LoginDataAvailable;

        /// <summary>
        /// Fired when the LoginData has been stored in the database and can be requested. 
        /// </summary>
        public static event EventHandler LoginDataIsUpdated;

        /// <summary>
        /// Fired when the List of <see cref="IGroup"/> has been received by the server and is ready to be stored in the database.
        /// </summary>
        public static event EventHandler<List<IGroup>> GroupsDataAvailable;


        /// <summary>
        /// Will be fired once all the initial loading data from the server has been completed 
        /// </summary>
        public static event EventHandler TW2DataLoadingCompleted;



        public static event EventHandler<ICharacterData> CharacterDataAvailable;


        public static event EventHandler<bool> LoginComplete;

        #endregion Events

        #region Methods

        public static void InvokeConnectionResult(ConnectResult connectionResult, object sender = null)
        {
            ConnectionResult?.Invoke(sender, connectionResult);
        }

        public static void InvokeConnectionStatus(bool connectionStatus, object sender = null)
        {
            ConnectionStatus?.Invoke(sender, connectionStatus);
        }

        public static void InvokeLoginDataIsUpdated(object sender = null)
        {
            LoginDataIsUpdated?.Invoke(sender, EventArgs.Empty);
        }

        public static void InvokeVillagesUpdated(object sender = null)
        {
            VillagesUpdated?.Invoke(sender, EventArgs.Empty);
        }

        public static void InvokeTW2DataLoadingCompleted(object sender = null)
        {
            TW2DataLoadingCompleted?.Invoke(sender, EventArgs.Empty);
        }

        public static void InvokeLoginDataAvailable(ILoginData loginDataDto, object sender = null)
        {
            LoginDataAvailable?.Invoke(sender, loginDataDto);
        }

        public static void InvokeGroupsDataAvailable(List<IGroup> groupList, object sender = null)
        {
            GroupsDataAvailable?.Invoke(sender, groupList);
        }

        public static void InvokeCharacterDataAvailable(ICharacterData characterData, object sender = null)
        {
            CharacterDataAvailable?.Invoke(sender, characterData);
        }

        public static void InvokeCharacterDataAvailable(bool success, object sender = null)
        {
            LoginComplete?.Invoke(sender, success);
        }

        #endregion Methods
    }
}
