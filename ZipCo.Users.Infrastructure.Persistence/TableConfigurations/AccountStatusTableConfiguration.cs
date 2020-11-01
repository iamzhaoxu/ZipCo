using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZipCo.Users.Domain.Entities.Accounts;

namespace ZipCo.Users.Infrastructure.Persistence.TableConfigurations
{
    public class AccountStatusTableConfiguration : IEntityTypeConfiguration<AccountStatus>
    {
        public void Configure(EntityTypeBuilder<AccountStatus> builder)
        {
            builder.ToTable("AccountStatus")
                .HasKey(m => m.Id);

            builder.Property(a => a.Name)
                .HasColumnName("Name")
                .HasMaxLength(50)
                .IsRequired();

            builder.HasData(
                new AccountStatus
                {
                    Id = 1,
                    Name = "Active"
                }, new AccountStatus
                {
                    Id = 2,
                    Name = "Closed"
                }
            );

        }
    }
}
