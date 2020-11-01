using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZipCo.Users.Domain.Entities.Accounts;
using ZipCo.Users.Infrastructure.Persistence.Extensions;

namespace ZipCo.Users.Infrastructure.Persistence.TableConfigurations
{
    public class AccountTableConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Account")
                .HasKey(m => m.Id);

            builder.Property(a => a.AccountNumber)
                .HasColumnName("AccountNumber")
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(a => a.AccountBalance)
                .HasColumnName("AccountBalance")
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(a => a.PendingBalance)
                .HasColumnName("PendingBalance")
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0)
                .IsRequired();

            builder.HasAudibleColumns();
            
            builder.HasMany(a => a.MemberAccounts)
                .WithOne(ma => ma.Account)
                .HasForeignKey(a => a.AccountId);

            builder.HasOne(a => a.AccountStatus)
                .WithMany()
                .HasForeignKey(a => a.AccountStatusId)
                .IsRequired();

            builder.HasIndex(m => m.AccountNumber)
                .IsUnique();

            builder.HasIndex(m => m.AccountStatusId);
        }
    }
}
