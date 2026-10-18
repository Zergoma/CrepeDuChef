using CrepeDuChef.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrepeDuChef.Infrastructure.Persistence
{
    public class CrepeDbContext : DbContext
    {
        public DbSet<Chef> Chefs => Set<Chef>();
        public DbSet<CrepeParty> CrepeParties => Set<CrepeParty>();

        public CrepeDbContext(DbContextOptions<CrepeDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CrepeDbContext).Assembly);
        }
    }
}
