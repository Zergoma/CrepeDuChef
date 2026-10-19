using CrepeDuChef.Maui.MVVM.Views;

namespace CrepeDuChef.Maui.DI;

public static class MauiViewsModule
{
    public static IServiceCollection AddMauiViews(this IServiceCollection services)
    {
        services.AddTransient<CrepeSessionsView>();
        services.AddTransient<ChefSettingsView>();

        return services;
    }
}
