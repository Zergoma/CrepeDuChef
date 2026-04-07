using CrepeDuChef.Domain.Interfaces;
using CrepeDuChef.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace CrepeDuChef.Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddCrepeDuChefInfrastructure(this IServiceCollection services, string dbPath)
        {
            string connectionString = $"Data Source={dbPath}";

            Debug.WriteLine($"DI configuration --> Connection string for sqlite file is : {connectionString}");

            services.AddDbContextFactory<CrepeDbContext>(options =>
                options.UseSqlite(connectionString));

            services.AddTransient<ICrepePartyRepository, CrepePartyRepository>();

            return services;
        }
    }
}
