using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Domain.Interfaces;
using CrepeDuChef.Infrastructure.Persistence;
using CrepeDuChef.Infrastructure.Providers;
using CrepeDuChef.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CrepeDuChef.Infrastructure.DI
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddCrepeDuChefInfrastructure(this IServiceCollection services, string dbPath)
        {
            services.AddDbContextFactory<CrepeDbContext>(options =>
                options.UseSqlite($"Data Source={dbPath}"));

            services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();
            services.AddSingleton<IRandomProvider, DefaultRandomProvider>();

            services.AddTransient<IChefRepository, ChefRepository>();
            services.AddTransient<ICrepePartyRepository, CrepePartyRepository>();

            return services;
        }

        /// <summary>
        /// Applies pending EF Core migrations; call once at startup, before the UI uses the database.
        /// </summary>
        public static void MigrateCrepeDuChefDatabase(this IServiceProvider services)
        {
            IDbContextFactory<CrepeDbContext> factory =
                services.GetRequiredService<IDbContextFactory<CrepeDbContext>>();

            using CrepeDbContext ctx = factory.CreateDbContext();
            ctx.Database.Migrate();
        }
    }
}
