using CalculationTools.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CalculationTools.Data
{
    public class AccountConfig : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            // Configure accounts table
            builder
                .HasOne(a => a.OnServer)
                .WithMany(a => a.Accounts)
                .HasForeignKey(a => a.OnServerId)
                .IsRequired();

            builder
                .HasMany(a => a.CharacterList)
                .WithOne(a => a.AccountOwner)
                .HasForeignKey(a => a.AccountOwnerId);


            builder
                .HasOne(a => a.DefaultCharacter)
                .WithOne();
        }
    }

}
