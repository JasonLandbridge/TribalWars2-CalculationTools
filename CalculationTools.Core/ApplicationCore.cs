using CalculationTools.Data;

namespace CalculationTools.Core
{
    /// <summary>
    /// The root class of the Application Core Domain
    /// </summary>
    public static class ApplicationCore
    {
        /// <summary>
        /// The start-up, or bootstrapper method, for the application core domain.
        /// </summary>
        public static void OnStartUp()
        {
            // Continue adding the dependencies in the IoC container
            IoC.Setup();


            // Rebuild the database on every debug session
            //if (System.Diagnostics.Debugger.IsAttached)
            //{
            //    using (var db = new CalculationToolsDBContext())
            //    {
            //        db.Database.EnsureDeleted();
            //    }
            //}
        }
    }
}
