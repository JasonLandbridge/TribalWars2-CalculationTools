using CalculationTools.Common;
using NLog;
using System;

namespace CalculationTools.WebSocket
{
    public class ErrorMessageHandling : IErrorMessageHandling
    {
        #region Fields

        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        #endregion Fields

        #region Constructors

        public ErrorMessageHandling()
        {
        }

        #endregion Constructors


        public event EventHandler StopConnectionEvent;
        public event EventHandler<string> AddToConnectionLogEvent;


        #region Methods
        #region ParseMethods
        public void ParseResponseAsync(string response)
        {
            if (string.IsNullOrEmpty(response))
            {
                Log.Info("The response was null/empty");
                return;
            }

            // open connection or ping response
            if (response == "40" || response == "3")
            {
                return;
            }


            response = SocketUtilities.CleanResponse(response);

            var errorMessage = new ErrorMessageDTO();
            var sytemError = new SystemErrorDTO();


            switch (string.Empty)
            {
                case RouteProvider.CHARACTER_SELECTION_FAILED:
                    errorMessage = SocketUtilities.ParseDataFromResponse<ErrorMessageDTO>(response);
                    Log.Error($"Character selection failed: {errorMessage.Message}");

                    StopConnection();
                    break;


                case RouteProvider.SYSTEM_ERROR:
                    sytemError = SocketUtilities.ParseDataFromResponse<SystemErrorDTO>(response);

                    ParseSystemError(response);
                    Log.Error($"Character selection failed: {errorMessage.Message}");

                    StopConnection();
                    break;

                case RouteProvider.MESSAGE_ERROR:
                    errorMessage = SocketUtilities.ParseDataFromResponse<ErrorMessageDTO>(response);
                    Log.Error($"Socket Error: {errorMessage.ErrorCode} - {errorMessage.Message}");
                    StopConnection();
                    break;

                case RouteProvider.EXCEPTION_ERROR:
                    sytemError = SocketUtilities.ParseDataFromResponse<SystemErrorDTO>(response);
                    Log.Error($"Server exception error: {errorMessage.Message}");

                    AddToConnectionLog("THE SERVER IS MOST LIKELY DOWN!");
                    StopConnection();
                    break;

                default:

                    break;
            }
        }

        private void ParseSystemError(string response)
        {
            var systemError = SocketUtilities.ParseDataFromResponse<SystemErrorDTO>(response);

            if (systemError.Cause == RouteProvider.LOGIN)
            {
                //ConnectResult connectResult = _socketManager.GetConnectResult();
                //connectResult.IsConnected = false;
                //connectResult.Message = systemError.Message;

                //_dataManager.SetConnectionResult(connectResult);
            }

        }




        #endregion

        #region SocketClient Helper Methods

        public void StopConnection()
        {
            DataEvents.InvokeConnectionStatus(false);

            StopConnectionEvent?.Invoke(this, EventArgs.Empty);
        }


        public void AddToConnectionLog(string message)
        {
            AddToConnectionLogEvent?.Invoke(this, message);
        }

        #endregion

        #endregion Methods
    }
}
