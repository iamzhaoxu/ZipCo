using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZipCo.Users.Domain.Entities;

namespace ZipCo.Users.Infrastructure.Persistence.Extensions
{
    public static class EntityTypeBuilderExtension
    {
        public static void HasAudibleColumns<T>(this EntityTypeBuilder<T> builder) where T: AudibleBaseEntity
        {
            builder.Property(d => d.CreatedOn)
                .HasColumnName("CreatedOn")
                .HasDefaultValueSql("getutcdate()")
                .IsRequired();

            builder.Property(d => d.ModifiedOn)
                .HasColumnName("ModifiedOn")
                .HasDefaultValueSql("getutcdate()")
                .IsRequired();
        }
    }
}
