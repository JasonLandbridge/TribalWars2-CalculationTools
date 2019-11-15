using CalculationTools.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CalculationTools.Data
{
    public class VillageConfig : IEntityTypeConfiguration<Village>
    {
        public void Configure(EntityTypeBuilder<Village> builder)
        {

            // Configure Villages
            builder
                .HasKey(a => a.Id);
            builder
                .HasOne(a => a.Character)
                .WithMany(a => a.Villages)
                .HasForeignKey(a => a.CharacterId);

        }
    }

}
