using CalculationTools.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CalculationTools.Data
{
    public class ServerConfig : IEntityTypeConfiguration<Server>
    {
        public void Configure(EntityTypeBuilder<Server> builder)
        {
            // Configure Server Table
            builder
                .Property(a => a.Id).ValueGeneratedOnAdd();
            builder
                .HasMany(c => c.Worlds)
                .WithOne(a => a.OnServer);
            builder
                .HasMany(c => c.Accounts)
                .WithOne(a => a.OnServer);
        }
    }

}
