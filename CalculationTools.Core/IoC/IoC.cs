using Ninject;

namespace CalculationTools.Core
{
    /// <summary>
    /// The IoC container for the Calculation.App
    /// </summary>
    public static class IoC
    {
        #region Properties

        /// <summary>
        /// The kernel for the IoC container
        /// </summary>
        public static IKernel Kernel { get; } = new StandardKernel();

        #endregion Properties

        #region Methods

        /// <summary>
        /// Return a service from the IoC, of the specified type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }

        /// <summary>
        /// Sets up the IoC container, binds all the information required and is ready for use
        /// NOTE: Must be called as soon as your application starts up to ensure all
        ///       services can be found
        /// </summary>
        public static void Setup()
        {
            // Bind all view models
            BindViewModel();
        }

        /// <summary>
        /// Binds all singleton view models
        /// </summary>
        private static void BindViewModel()
        {
            // Binds to a single instance of Application view model
            Kernel.Bind<ApplicationViewModel>().ToConstant(new ApplicationViewModel());
        }

        #endregion Methods
    }
}
