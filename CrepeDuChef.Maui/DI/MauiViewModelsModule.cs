using CrepeDuChef.Maui.MVVM.ViewModels;
using SharedVm = CrepeDuChef.ViewModels.ViewModels;

namespace CrepeDuChef.Maui.DI;

public static class MauiViewModelsModule
{
    public static IServiceCollection AddMauiViewModels(this IServiceCollection services)
    {
        // Shared ViewModels
        services.AddTransient<SharedVm.CrepeSessionsViewModel>();
        services.AddTransient<SharedVm.UserSettingViewModel>();

        // MAUI adapter
        services.AddTransient<CrepeSessionsViewModel_MauiAdapter>();

        return services;
    }
}
