using CrepeDuChef.Maui.UI.Popups.Core;
using CrepeDuChef.Maui.UI.Popups.Presenters;
using CrepeDuChef.ViewModels.Presenters;

namespace CrepeDuChef.Maui.DI;

public static class MauiPresentersModule
{
    public static IServiceCollection AddMauiPresenters(this IServiceCollection services)
    {
        services.AddSingleton<PopupCoordinator>();
        services.AddSingleton<PopupOptionsFactory>();

        services.AddTransient<IChefFormPresenter, MauiUserFormPresenter>();
        services.AddTransient<IChefSelectionPresenter, MauiUserSelectionPresenter>();
        services.AddTransient<IDialogPresenter, MauiDialogPresenter>();

        return services;
    }
}
