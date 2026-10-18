using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Application.Services;

namespace CrepeDuChef.Api.DI
{
    public static class BackendServicesRegistration
    {
        public static IServiceCollection AddCrepeBackendServices(this IServiceCollection services)
        {
            services.AddScoped<IChefDataService, ChefLocalDataService>();
            services.AddScoped<ICrepePartyDataService, CrepePartyLocalServicexxxxx>();

            return services;
        }
    }
}
