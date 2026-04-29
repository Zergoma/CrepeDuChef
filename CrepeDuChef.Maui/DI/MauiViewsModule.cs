using CrepeDuChef.Maui.MVVM.Views;

namespace CrepeDuChef.Maui.DI;

public static class MauiViewsModule
{
    public static IServiceCollection AddMauiViews(this IServiceCollection services)
    {
        services.AddTransient<CrepeDuChefView>();
        services.AddTransient<UserSettingView>();

        return services;
    }
}
