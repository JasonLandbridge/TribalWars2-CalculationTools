using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalculationTools.Common
{
    public class Account : BasePropertyChanged
    {
        #region Properties

        /// <summary>
        /// The internal account Id used in this application.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The username of this account.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// The password of this account.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// The account Id used by Tribal Wars 2, this will not be set until it is confirmed. 
        /// </summary>
        public int? TW2AccountID { get; set; }

        /// <summary>
        /// Is this account to be confirmed working after the credentials have been tested.
        /// </summary>
        public bool IsConfirmed { get; set; }

        #region Relationships

        /// <summary>
        /// Which server this account is used on. 
        /// </summary>
        public Server OnServer { get; set; }

        public string OnServerId { get; set; }

        public Character DefaultCharacter { get; set; }

        public int? DefaultCharacterId { get; set; }


        public ICollection<Character> CharacterList { get; set; }

        #endregion

        #region NotMapped

        [NotMapped]
        public string ServerCountryCode => OnServer?.Id;

        /// <summary>
        /// The formatted name for this account in the format of {ServerCountryCode} - {Username}.
        /// </summary>
        [NotMapped]
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
                    displayName += $"{ServerCountryCode.ToUpper()} - ";
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

        #endregion

        #endregion Properties

        #region Methods

        /// <summary>
        /// Helper function which creates a ConnectData object with the account credentials.
        /// </summary>
        /// <returns></returns>
        public ConnectData ToConnectData()
        {
            return new ConnectData
            {
                Username = Username,
                Password = Password,
                ServerCountryCode = ServerCountryCode,
                WorldID = DefaultCharacter?.World?.WorldId
            };
        }

        #endregion Methods
    }
}
