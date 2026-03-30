using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CrepeDuChef.Infrastructure
{
    public class CrepeDbContextDesignTimeFactory : IDesignTimeDbContextFactory<CrepeDbContext>
    {
        public CrepeDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CrepeDbContext>();

            // Path used only for migrations
            var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "CrepeDuChef.design.db3");

            optionsBuilder.UseSqlite($"Data Source={dbPath}");

            return new CrepeDbContext(optionsBuilder.Options);
        }
    }
}
