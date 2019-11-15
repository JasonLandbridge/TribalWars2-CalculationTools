namespace CalculationTools.Common
{
    public class ConnectResult
    {
        public bool IsConnected { get; set; }
        public string AccessToken { get; set; }
        public string SessionID { get; set; }
        public string Message { get; set; }
        public int? TW2AccountId { get; set; }
    }
}
