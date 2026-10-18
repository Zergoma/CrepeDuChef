using SharedVm = CrepeDuChef.ViewModels.ViewModels;

namespace CrepeDuChef.Maui.DI;

public static class MauiViewModelsModule
{
    public static IServiceCollection AddMauiViewModels(this IServiceCollection services)
    {
        services.AddTransient<SharedVm.CrepeSessionsViewModel>();
        services.AddTransient<SharedVm.ChefSettingsViewModel>();

        return services;
    }
}
