using CrepeDuChef.Maui.UI.Popups.Factories;

namespace CrepeDuChef.Maui.DI;

public static class MauiPopupsModule
{
    public static IServiceCollection AddMauiPopups(this IServiceCollection services)
    {
        services.AddTransient<IChefFormPopupFactory, ChefFormPopupFactory>();
        services.AddTransient<IChefSelectionPopupFactory, ChefSelectionPopupFactory>();
        services.AddTransient<IMessagePopupFactory, MessagePopupFactory>();

        return services;
    }
}
