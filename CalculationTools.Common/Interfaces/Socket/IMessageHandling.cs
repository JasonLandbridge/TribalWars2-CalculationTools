namespace CalculationTools.Common.Socket
{
    public interface IMessageHandling
    {
        void ParseResponseAsync(string response);
    }
}