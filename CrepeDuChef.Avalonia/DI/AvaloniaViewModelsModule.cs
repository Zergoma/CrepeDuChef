using Microsoft.Extensions.DependencyInjection;
using CrepeDuChef.Avalonia.ViewModels;
using SharedVm = CrepeDuChef.ViewModels.ViewModels;

namespace CrepeDuChef.Avalonia.DI;

public static class AvaloniaViewModelsModule
{
    public static IServiceCollection AddAvaloniaViewModels(this IServiceCollection services)
    {
        services.AddSingleton<MainViewModel>();
        services.AddTransient<SharedVm.UserSettingViewModel>();

        services.AddTransient<SharedVm.CrepeSessionsViewModel>();
        services.AddTransient<CrepeSessionsViewModelAvaloniaAdapter>();

        return services;
    }
}
