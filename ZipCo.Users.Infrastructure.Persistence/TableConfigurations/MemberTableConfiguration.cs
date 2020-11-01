using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZipCo.Users.Domain.Entities.Members;
using ZipCo.Users.Infrastructure.Persistence.Extensions;

namespace ZipCo.Users.Infrastructure.Persistence.TableConfigurations
{
    public class MemberTableConfiguration: IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.ToTable("Member")
                .HasKey(m => m.Id);

            builder.Property(a => a.Email)
                .IsRequired();

            builder.HasAudibleColumns();

            builder.HasOne(m => m.MemberSalary)
                .WithOne(ms => ms.Member)
                .HasForeignKey<MemberSalary>(ms => ms.MemberId)
                .IsRequired();

            builder.HasOne(m => m.MemberExpense)
                .WithOne(me => me.Member)
                .HasForeignKey<MemberExpense>(me => me.MemberId)
                .IsRequired();

            builder.HasIndex(m => m.Email)
                .IsUnique();

        }
    }
}
