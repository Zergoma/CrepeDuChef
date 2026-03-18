using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CrepeDuChef.Infrastructure
{
    public class CrepeDbContextDesignTimeFactory : IDesignTimeDbContextFactory<CrepeDbContext>
    {
        public CrepeDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CrepeDbContext>();

            string path = Constants.GetDbPath();
            string connection = $"Data Source={path}";

            optionsBuilder.UseSqlite(connection);

            return new CrepeDbContext(optionsBuilder.Options);
        }
    }
}
