using CalculationTools.Common.Entities.World;
using Microsoft.EntityFrameworkCore;

namespace CalculationTools.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Village> Villages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=CalculationToolsDB.sqlite");
        }
    }
}
