using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZipCo.Users.Domain.Entities.Members;
using ZipCo.Users.Infrastructure.Persistence.Extensions;

namespace ZipCo.Users.Infrastructure.Persistence.TableConfigurations
{
    public class MemberSalaryTableConfiguration : IEntityTypeConfiguration<MemberSalary>
    {
        public void Configure(EntityTypeBuilder<MemberSalary> builder)
        {
            builder.ToTable("MemberSalary")
                .HasKey(m => m.Id);

            builder.Property(a => a.Amount)
                .HasColumnName("Amount")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.HasAudibleColumns();

            builder.HasOne(ms => ms.PayFrequency)
                .WithMany()
                .HasForeignKey(ms => ms.PayFrequencyId)
                .IsRequired();
        }
    }
}
