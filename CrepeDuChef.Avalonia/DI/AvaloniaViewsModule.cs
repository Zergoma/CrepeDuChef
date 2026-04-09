using Microsoft.Extensions.DependencyInjection;
using CrepeDuChef.Avalonia.Views;

namespace CrepeDuChef.Avalonia.DI;

public static class AvaloniaViewsModule
{
    public static IServiceCollection AddAvaloniaViews(this IServiceCollection services)
    {
        services.AddTransient<MainWindow>();
        services.AddTransient<UserSettingsView>();
        services.AddTransient<CrepeSessionsView>();

        return services;
    }
}
