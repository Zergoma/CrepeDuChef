using AppInterfaces = CrepeDuChef.Application.Interfaces;
using CrepeDuChef.UI.Shared.Services;

using Microsoft.Extensions.DependencyInjection;

namespace CrepeDuChef.UI.Shared.DI;

public static class ClientServicesRegistration
{
    public static IServiceCollection AddCrepeClientServices(this IServiceCollection services, Uri baseAddress)
    {
        services.AddHttpClient<AppInterfaces.ICrepePartyDataService, CrepePartyDataService>(client =>
        {
            client.BaseAddress = baseAddress;
        });

        services.AddHttpClient<AppInterfaces.IChefDataService, ChefDataService>(client =>
        {
            client.BaseAddress = baseAddress;
        });

        services.AddHttpClient<AppInterfaces.IChefRotationDataService, ChefRotationDataService>(client =>
        {
            client.BaseAddress = baseAddress;
        });


        return services;
    }
}
