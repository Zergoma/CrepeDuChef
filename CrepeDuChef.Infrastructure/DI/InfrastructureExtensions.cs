using CrepeDuChef.Infrastructure.Providers;
using CrepeDuChef.Infrastructure.Repositories;

using Microsoft.Extensions.DependencyInjection;

using AppInterface = CrepeDuChef.Application.Interfaces;
using DomainInterface = CrepeDuChef.Domain.Interfaces;

namespace CrepeDuChef.Infrastructure.DI
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddCrepeDuChefInfrastructure(this IServiceCollection services)
        {
            // Repositories
            services.AddTransient<AppInterface.ICrepePartyRepository, CrepePartyRepository>();
            services.AddTransient<AppInterface.IChefRepository, ChefRepository>();

            // Providers
            services.AddSingleton<AppInterface.IDateTimeProvider, SystemDateTimeProvider>();
            services.AddTransient<DomainInterface.IRandomProvider, DefaultRandomProvider>();

            return services;
        }
    }
}
