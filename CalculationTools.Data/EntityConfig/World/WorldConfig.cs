using CalculationTools.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CalculationTools.Data
{
    public class WorldConfig : IEntityTypeConfiguration<World>
    {
        public void Configure(EntityTypeBuilder<World> builder)
        {
            // Configure worlds table
            builder
                .HasKey(a => a.WorldId);

            builder
                .Property(a => a.WorldId)
                .HasColumnName("Id");

            builder
                .HasOne(e => e.OnServer)
                .WithMany(a => a.Worlds)
                .HasForeignKey(x => x.OnServerId);

            builder
                .HasMany(e => e.Characters)
                .WithOne(a => a.World)
                .HasForeignKey(a => a.WorldId);

        }
    }

}
