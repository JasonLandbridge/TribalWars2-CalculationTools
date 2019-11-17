using CalculationTools.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace CalculationTools.Data
{
    public sealed class CalculationToolsDBContext : DbContext
    {

        private readonly string connectionString = "Data Source=./CalculationToolsDB.db";


        public DbSet<Server> Servers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<World> Worlds { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Village> Villages { get; set; }
        public DbSet<Group> Groups { get; set; }


        public CalculationToolsDBContext()
        {
            Database.EnsureCreated();
        }

        public CalculationToolsDBContext(
            DbContextOptions<CalculationToolsDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(connectionString);
                optionsBuilder.EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // import all separate entity config files
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

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
                    Id = "en",
                    ServerName = "International"
                },
                new Server
                {
                    Id = "nl",
                    ServerName = "Netherlands"
                },
                new Server
                {
                    Id = "de",
                    ServerName = "Germany"
                },
                new Server
                {
                    Id = "br",
                    ServerName = "Brazil"
                },
                new Server
                {
                    Id = "pl",
                    ServerName = "Poland"
                },
                new Server
                {
                    Id = "fr",
                    ServerName = "France"
                },
                new Server
                {
                    Id = "ru",
                    ServerName = "Russia"
                },
                new Server
                {
                    Id = "us",
                    ServerName = "United States"
                },
                new Server
                {
                    Id = "es",
                    ServerName = "Spain"
                },
                new Server
                {
                    Id = "it",
                    ServerName = "Italy"
                },
                new Server
                {
                    Id = "gr",
                    ServerName = "Greece"
                },
                new Server
                {
                    Id = "tr",
                    ServerName = "Turkey"
                },
                new Server
                {
                    Id = "cz",
                    ServerName = "Czech Republic"
                },
                new Server
                {
                    Id = "pt",
                    ServerName = "Portugal"
                },
                new Server
                {
                    Id = "no",
                    ServerName = "Norway"
                },
                new Server
                {
                    Id = "sk",
                    ServerName = "Slovakia"
                },
                new Server
                {
                    Id = "ro",
                    ServerName = "Romania"
                },
                new Server
                {
                    Id = "hu",
                    ServerName = "Hungary"
                },
                new Server
                {
                    Id = "beta",
                    ServerName = "Beta Server"
                },
            };

            modelBuilder.Entity<Server>().HasData(serverList);



            //TODO DELETE THIS BEFORE COMMITING
            modelBuilder.Entity<Account>().HasData(
                new List<Account>{
                    new Account
                    {
                        Id = 1,
                        Username = "***REMOVED***",
                        Password = "***REMOVED***",
                        OnServerId = "en",
                        IsConfirmed = true
                    },
                    new Account
                    {
                        Id = 2,
                        Username = "***REMOVED***",
                        Password = "***REMOVED***",
                        OnServerId = "nl",
                        IsConfirmed = true

                    }
                });

        }


        public bool EnsureDeletedAndCreated()
        {
            try
            {
                Database.EnsureDeleted();
                Database.EnsureCreated();
                return true;

            }
            catch (IOException e)
            {
                Console.WriteLine(e);
            }

            return false;
        }
    }
}
