using Microsoft.Extensions.DependencyInjection;
using CrepeDuChef.Avalonia.Dialogs.Presenters;
using SharedPresenters = CrepeDuChef.ViewModels.Presenters;

namespace CrepeDuChef.Avalonia.DI;

public static class AvaloniaPresentersModule
{
    public static IServiceCollection AddAvaloniaPresenters(this IServiceCollection services)
    {
        services.AddTransient<SharedPresenters.IDialogPresenter, AvaloniaDialogPresenter>();
        services.AddTransient<SharedPresenters.IUserFormPresenter, AvaloniaUserFormPresenter>();
        services.AddTransient<SharedPresenters.IUserSelectionPresenter, AvaloniaUserSelectionPresenter>();

        return services;
    }
}
