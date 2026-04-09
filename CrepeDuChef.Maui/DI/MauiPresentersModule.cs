using CrepeDuChef.Maui.UI.Popups.Core;
using CrepeDuChef.Maui.UI.Popups.Factories;
using CrepeDuChef.Maui.UI.Popups.Presenters;
using SharedPresenter = CrepeDuChef.ViewModels.Presenters;

namespace CrepeDuChef.Maui.DI;

public static class MauiPresentersModule
{
    public static IServiceCollection AddMauiPresenters(this IServiceCollection services)
    {
        services.AddSingleton<PopupCoordinator>();
        services.AddSingleton<PopupOptionsFactory>();

        services.AddTransient<SharedPresenter.IUserFormPresenter, MauiUserFormPresenter>();
        services.AddTransient<SharedPresenter.IUserSelectionPresenter, MauiUserSelectionPresenter>();
        services.AddTransient<SharedPresenter.IDialogPresenter, MauiDialogPresenter>();

        return services;
    }
}
