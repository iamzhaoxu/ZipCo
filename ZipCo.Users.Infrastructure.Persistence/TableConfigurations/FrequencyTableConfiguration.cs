using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZipCo.Users.Domain.Entities.Members;

namespace ZipCo.Users.Infrastructure.Persistence.TableConfigurations
{
    public class FrequencyTableConfiguration : IEntityTypeConfiguration<Frequency>
    {
        public void Configure(EntityTypeBuilder<Frequency> builder)
        {
            builder.ToTable("Frequency")
                .HasKey(m => m.Id);

            builder.Property(a => a.Name)
                .HasColumnName("Name")
                .HasMaxLength(30)
                .IsRequired();

            builder.HasData(
                new Frequency
                {
                    Id = 1,
                    Name = "Month"
                }, new Frequency
                {
                    Id = 2,
                    Name = "Annual"
                }
            );
        }
    }
}
