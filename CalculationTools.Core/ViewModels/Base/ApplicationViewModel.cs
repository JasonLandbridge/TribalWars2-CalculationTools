using System;
using CalculationTools.Core.Base;
using NLog;


namespace CalculationTools.Core
{
    /// <summary>
    /// The application state as a view model
    /// </summary>
    public class ApplicationViewModel : BaseViewModel
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ApplicationViewModel()
        {
            SetupLogging();
        }
        public void SetupLogging()
        {
            logger.Error("This is an error message");
        }
    }
}
