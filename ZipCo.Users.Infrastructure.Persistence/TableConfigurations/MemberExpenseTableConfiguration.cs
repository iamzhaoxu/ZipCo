using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZipCo.Users.Domain.Entities.Members;
using ZipCo.Users.Infrastructure.Persistence.Extensions;

namespace ZipCo.Users.Infrastructure.Persistence.TableConfigurations
{
    public class MemberExpenseTableConfiguration : IEntityTypeConfiguration<MemberExpense>
    {
        public void Configure(EntityTypeBuilder<MemberExpense> builder)
        {
            builder.ToTable("MemberExpense")
                .HasKey(m => m.Id);

            builder.Property(a => a.Amount)
                .HasColumnName("Amount")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.HasAudibleColumns();

            builder.HasOne(me => me.BillFrequency)
                .WithMany()
                .HasForeignKey(me => me.BillFrequencyId)
                .IsRequired();
        }
    }
}
