namespace CalculationTools.Common
{
    public interface IMessageHandling
    {
        void ParseResponseAsync(string response);
        bool IsReconnecting { get; set; }
    }
}