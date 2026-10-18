using CrepeDuChef.Avalonia.ViewModels;
using CrepeDuChef.Avalonia.Views;
using Microsoft.Extensions.DependencyInjection;
using SharedVm = CrepeDuChef.ViewModels.ViewModels;

namespace CrepeDuChef.Avalonia.DI;

public static class AvaloniaViewModelsModule
{
    public static IServiceCollection AddAvaloniaViewModels(this IServiceCollection services)
    {
        // Pages live as long as the main window, which keeps them while navigating
        services.AddSingleton<SharedVm.CrepeSessionsViewModel>();
        services.AddSingleton<SharedVm.ChefSettingsViewModel>();

        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MainWindow>();

        return services;
    }
}
