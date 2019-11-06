using CalculationTools.Common;
using Microsoft.EntityFrameworkCore;

namespace CalculationTools.Data
{
    public class CalculationToolsDBContext : DbContext
    {

        public DbSet<Server> Servers { get; set; }
        public DbSet<World> Worlds { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Village> Villages { get; set; }
        public DbSet<Group> Groups { get; set; }


        public CalculationToolsDBContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = $"Data Source=./CalculationToolsDB.db";
            optionsBuilder.UseSqlite(connectionString);
            optionsBuilder.EnableSensitiveDataLogging(true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Server Table
            modelBuilder.Entity<Server>()
                .Property(a => a.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Server>()
                .HasMany(c => c.Worlds)
                .WithOne(a => a.Server);

            // Configure worlds table
            modelBuilder.Entity<World>()
                .HasKey(a => a.Id);
            modelBuilder.Entity<World>()
                .HasOne(e => e.Server)
                .WithMany(a => a.Worlds);
            modelBuilder.Entity<World>()
                .HasOne(e => e.Character)
                .WithMany(a => a.Worlds);

            // Configure Character Table
            modelBuilder.Entity<Character>()
                .Ignore(c => c.Id);
            modelBuilder.Entity<Character>()
                .HasKey(a => a.CharacterId);
            modelBuilder.Entity<Character>()
                .Property(a => a.CharacterId)
                .HasColumnName("Id");
            modelBuilder.Entity<Character>()
                .HasMany(a => a.Worlds)
                .WithOne(a => a.Character);

            // Configure groups
            modelBuilder.Entity<Group>()
                .HasKey(a => a.Id);
            modelBuilder.Entity<Group>()
                .HasOne(a => a.Character)
                .WithMany(a => a.Groups);



            // Seed database with initial data
            AddDefaultEntries(modelBuilder);
        }

        private void AddDefaultEntries(ModelBuilder modelBuilder)
        {

            // Add standard servers available
            var serverList = new[]
            {
                new Server
                {
                    ServerName = "International",
                    ServerCountryCode = "en"
                },
                new Server
                {
                    ServerName = "Netherlands",
                    ServerCountryCode = "nl"
                },
                new Server
                {
                    ServerName = "Germany",
                    ServerCountryCode = "de"
                },
                new Server
                {
                    ServerName = "Brazil",
                    ServerCountryCode = "br"
                },
                new Server
                {
                    ServerName = "Poland",
                    ServerCountryCode = "pl"
                },
                new Server
                {
                    ServerName = "France",
                    ServerCountryCode = "fr"
                },
                new Server
                {
                    ServerName = "Russia",
                    ServerCountryCode = "ru"
                },
                new Server
                {
                    ServerName = "United States",
                    ServerCountryCode = "us"
                },
                new Server
                {
                    ServerName = "Spain",
                    ServerCountryCode = "es"
                },
                new Server
                {
                    ServerName = "Italy",
                    ServerCountryCode = "it"
                },
                new Server
                {
                    ServerName = "Greece",
                    ServerCountryCode = "gr"
                },
                new Server
                {
                    ServerName = "Turkey",
                    ServerCountryCode = "tr"
                },
                new Server
                {
                    ServerName = "Czech Republic",
                    ServerCountryCode = "cz"
                },
                new Server
                {
                    ServerName = "Portugal",
                    ServerCountryCode = "pt"
                },
                new Server
                {
                    ServerName = "Norway",
                    ServerCountryCode = "no"
                },
                new Server
                {
                    ServerName = "Slovakia",
                    ServerCountryCode = "sk"
                },
                new Server
                {
                    ServerName = "Romania",
                    ServerCountryCode = "ro"
                },
                new Server
                {
                    ServerName = "Hungary",
                    ServerCountryCode = "hu"
                },
                new Server
                {
                    ServerName = "Beta Server",
                    ServerCountryCode = "beta"
                },
            };
            for (int i = 0; i < serverList.Length; i++)
            {
                serverList[i].Id = i + 1;
            }

            modelBuilder.Entity<Server>().HasData(serverList);
        }

    }
}
