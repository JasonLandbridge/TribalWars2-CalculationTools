using System;

namespace CalculationTools.Common
{
    public interface IErrorMessageHandling
    {
        void ParseResponseAsync(string response);
        event EventHandler StopConnectionEvent;
        event EventHandler<string> AddToConnectionLogEvent;
    }
}