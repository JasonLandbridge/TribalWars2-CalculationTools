using CalculationTools.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CalculationTools.Data
{
    public class CharacterConfig : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            // Configure Character Table
            builder.HasKey(a => a.Id);

            builder
                .HasOne(x => x.AccountOwner)
                .WithMany(x => x.CharacterList)
                .HasForeignKey(x => x.AccountOwnerId)
                .IsRequired(false);

            builder
                .HasOne(x => x.World)
                .WithMany(x => x.Characters)
                .HasForeignKey(x => x.WorldId);

            builder
                .HasMany(x => x.Groups)
                .WithOne(x => x.Character)
                .HasForeignKey(x => x.CharacterId)
                .IsRequired(false);

            builder
                .HasMany<Village>()
                .WithOne()
                .HasForeignKey(x => x.CharacterId)
                .IsRequired(false);

            builder
                .HasOne(a => a.DefaultCharacterFor)
                .WithOne(a => a.DefaultCharacter)
                .HasForeignKey<Character>(a => a.DefaultCharacterForId)
                .IsRequired(false);

        }
    }

}
