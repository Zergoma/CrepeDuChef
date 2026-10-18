using CrepeDuChef.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrepeDuChef.Infrastructure.Persistence.Configurations
{
    public class CrepePartyConfiguration : IEntityTypeConfiguration<CrepeParty>
    {
        public void Configure(EntityTypeBuilder<CrepeParty> builder)
        {
            builder.ToTable("CrepeParties");

            builder.Property(cp => cp.Date)
                   .IsRequired();

            builder.Property(cp => cp.SessionNumber)
                   .IsRequired();

            builder.HasOne(cp => cp.Chef)
                   .WithMany(c => c.CrepeParties)
                   .HasForeignKey(cp => cp.ChefId)
                   .IsRequired();

            builder.HasIndex(cp => new { cp.SessionNumber, cp.ChefId, cp.OriginDeviceId })
                   .IsUnique();

            builder.Property(cp => cp.UpdatedAt)
                   .IsRequired();

            builder.Property(cp => cp.OriginDeviceId)
                   .IsRequired();
        }
    }
}
