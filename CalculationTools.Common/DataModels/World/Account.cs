using CalculationTools.Common.Connection;

namespace CalculationTools.Common
{
    public class Account : BasePropertyChanged
    {
        public int AccountID { get; set; }
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string ServerCountryCode { get; set; } = string.Empty;
        public string DefaultWorld { get; set; } = string.Empty;

        /// <summary>
        /// Is this account to be confirmed working.
        /// </summary>
        public bool IsConfirmed { get; set; }


        public ConnectData ToConnectData()
        {
            return new ConnectData
            {
                Username = Username,
                Password = Password,
                ServerCountryCode = ServerCountryCode
            };
        }
    }
}
