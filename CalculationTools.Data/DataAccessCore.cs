using Microsoft.EntityFrameworkCore;

namespace CalculationTools.Data
{
    public static class DataAccessCore
    {
        public static void OnStartUp()
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                dbContext.Database.Migrate();
            }
        }
    }
}
