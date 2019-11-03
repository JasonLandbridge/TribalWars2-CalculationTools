using CalculationTools.Common.Entities.World;
using Microsoft.EntityFrameworkCore;

namespace CalculationTools.Data
{
    public class CalculationToolsDBContext : DbContext
    {
        public DbSet<Village> Villages { get; set; }


        public CalculationToolsDBContext(DbContextOptions options) : base(options) { }


    }
}
