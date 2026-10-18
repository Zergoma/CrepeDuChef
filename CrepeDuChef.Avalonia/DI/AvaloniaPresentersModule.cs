using CrepeDuChef.Avalonia.Dialogs.Presenters;
using CrepeDuChef.ViewModels.Presenters;
using Microsoft.Extensions.DependencyInjection;

namespace CrepeDuChef.Avalonia.DI;

public static class AvaloniaPresentersModule
{
    public static IServiceCollection AddAvaloniaPresenters(this IServiceCollection services)
    {
        services.AddTransient<IDialogPresenter, AvaloniaDialogPresenter>();
        services.AddTransient<IChefFormPresenter, AvaloniaUserFormPresenter>();
        services.AddTransient<IChefSelectionPresenter, AvaloniaUserSelectionPresenter>();

        return services;
    }
}
