using System;
using System.Collections.Generic;

namespace CalculationTools.Common
{
    public class Account : BasePropertyChanged
    {
        public int AccountID { get; set; }
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string ServerCountryCode { get; set; } = string.Empty;
        public string DefaultWorld { get; set; } = string.Empty;

        public List<CharacterWorld> WorldList { get; set; } = new List<CharacterWorld>();
        public CharacterWorld SelectedWorld { get; set; }
        /// <summary>
        /// Is this account to be confirmed working.
        /// </summary>
        public bool IsConfirmed { get; set; }

        public string DisplayName
        {
            get
            {
                string displayName = string.Empty;

                if (string.IsNullOrEmpty(ServerCountryCode))
                {
                    displayName += "XX - ";
                }
                else
                {
                    displayName += $"{ServerCountryCode} - ";
                }

                if (string.IsNullOrEmpty(Username))
                {
                    displayName += "Empty Username";
                }
                else
                {
                    displayName += Username;
                }
                return displayName;
            }
        }

        public ConnectData ToConnectData()
        {


            return new ConnectData
            {
                Username = Username,
                Password = Password,
                ServerCountryCode = ServerCountryCode,
                WorldID = SelectedWorld?.WorldId
            };
        }
    }
}
