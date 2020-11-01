using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZipCo.Users.Domain.Entities.AccountSignUp;
using ZipCo.Users.Infrastructure.Persistence.Extensions;

namespace ZipCo.Users.Infrastructure.Persistence.TableConfigurations
{
    public class AccountSignUpStrategyTableConfiguration : IEntityTypeConfiguration<AccountSignUpStrategy>
    {
        public void Configure(EntityTypeBuilder<AccountSignUpStrategy> builder)
        {
            builder.ToTable("AccountSignUpStrategy")
                .HasKey(asus => asus.Id);

            builder.Property(asus => asus.Name)
                .HasColumnName("Name")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(asus => asus.IsDefault)
                .HasColumnName("IsDefault")
                .IsRequired();

            builder.Property(asus => asus.MonthNetIncomeLimit)
                .HasColumnName("MonthNetIncomeLimit")
                .HasColumnType("decimal(10,2)")
                .IsRequired(false);

            builder.HasAudibleColumns();

            // The data below should be configured from a AccountSignUpStrategy API
            // For demo purpose, we will hard here.
            builder.HasData(
                new AccountSignUpStrategy
                {
                    Id = 1,
                    Name = "Premium member program",
                    CreatedOn = DateTime.UtcNow,
                    ModifiedOn = DateTime.UtcNow,
                    MonthNetIncomeLimit = 1000,
                    IsDefault = true
                }
            );
        }
    }
}
