using System;
using CalculationTools.Common.Entities.World;
using Microsoft.EntityFrameworkCore;

namespace CalculationTools.Data
{
    public class CalculationToolsDBContext : DbContext
    {
        public DbSet<Village> Villages { get; set; }

        public CalculationToolsDBContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = $"Data Source={AppDomain.CurrentDomain.BaseDirectory}CalculationToolsDB.sqlite";
            optionsBuilder.UseSqlite(connectionString);
        }
    }
}
