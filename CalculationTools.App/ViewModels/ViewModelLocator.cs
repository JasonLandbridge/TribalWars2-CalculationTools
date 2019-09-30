using System;
using System.Collections.Generic;
using System.Text;
using CalculationTools.Core;

namespace CalculationTools.App
{
    /// <summary>
    /// Locates view models from the IoC for user in binding in XAML
    /// </summary>
    public class ViewModelLocator
    {
        #region Properties
        /// <summary>
        /// The application view model
        /// </summary>
        public static ApplicationViewModel ApplicationViewModel => IoC.Get<ApplicationViewModel>();

        /// <summary>
        /// Singleton instance of the locator
        /// </summary>
        public static ViewModelLocator Instance { get; } = new ViewModelLocator();

        #endregion Properties
    }
}
