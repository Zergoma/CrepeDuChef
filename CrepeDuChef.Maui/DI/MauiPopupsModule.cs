using CrepeDuChef.Maui.UI.Popups.Factories;

namespace CrepeDuChef.Maui.DI;

public static class MauiPopupsModule
{
    public static IServiceCollection AddMauiPopups(this IServiceCollection services)
    {
        services.AddTransient<IUserFormPopupFactory, UserPopCommunautyFactory>();
        services.AddTransient<IUserSelectionPopupFactory, UserSelectionPopupFactory>();
        services.AddTransient<IMessagePopupFactory, MessagePopupFactory>();

        return services;
    }
}
