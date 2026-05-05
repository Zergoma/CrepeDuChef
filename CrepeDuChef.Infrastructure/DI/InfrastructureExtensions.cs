using AppInterface = CrepeDuChef.Application.Interfaces;
using DomainInterface = CrepeDuChef.Domain.Interfaces;

using CrepeDuChef.Infrastructure.Repositories;
using CrepeDuChef.Infrastructure.Services;

using Microsoft.Extensions.DependencyInjection;

namespace CrepeDuChef.Infrastructure.DI
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddCrepeDuChefInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<CrepeDbContext>();

            services.AddSingleton<AppInterface.IDateTimeProvider, SystemDateTimeProvider>();
            services.AddTransient<AppInterface.ICrepePartyRepository, CrepePartyRepository>();

            services.AddTransient<DomainInterface.IRandomProvider, DefaultRandomProvider>();

            return services;
        }
    }
}
