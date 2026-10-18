using CrepeDuChef.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrepeDuChef.Infrastructure.Persistence.Configurations
{
    public class ChefConfiguration : IEntityTypeConfiguration<Chef>
    {
        public void Configure(EntityTypeBuilder<Chef> builder)
        {
            builder.ToTable("Chefs");

            builder.Property(c => c.FirstName)
                   .IsRequired()
                   .HasMaxLength(Chef.NameMaxLength);

            builder.Property(c => c.LastName)
                   .IsRequired()
                   .HasMaxLength(Chef.NameMaxLength);

            builder.Ignore(c => c.FullName);

            builder.Property(c => c.UpdatedAt)
                   .IsRequired();

            builder.Property(c => c.OriginDeviceId)
                   .IsRequired();
        }
    }
}
