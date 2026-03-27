using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Application.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CrepeDuChef.Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddCrepeDuChefApplication(this IServiceCollection services)
        {
            services.AddTransient<ICrepePartyRepositoryApplication, CrepePartyRepositoryApplication>();
            return services;
        }
    }
}
