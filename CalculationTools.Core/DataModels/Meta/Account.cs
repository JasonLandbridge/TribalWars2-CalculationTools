namespace CalculationTools.Core
{
    public class Account : BaseViewModel
    {
        public int AccountID { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }

        public string ServerCountryCode { get; set; }
    }
}
