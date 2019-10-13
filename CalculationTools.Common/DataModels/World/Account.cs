namespace CalculationTools.Common
{
    public class Account : BasePropertyChanged
    {
        public int AccountID { get; set; }
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string ServerCountryCode { get; set; } = string.Empty;
    }
}
