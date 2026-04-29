using Microsoft.Extensions.DependencyInjection;
using CrepeDuChef.Avalonia.Dialogs.Factories;

namespace CrepeDuChef.Avalonia.DI;

public static class AvaloniaDialogsModule
{
    public static IServiceCollection AddAvaloniaDialogs(this IServiceCollection services)
    {
        services.AddTransient<IAvaloniaMessageDialogFactory, AvaloniaMessageDialogFactory>();
        services.AddTransient<IAvaloniaUserFormDialogFactory, AvaloniaUserFormDialogFactory>();
        services.AddTransient<IAvaloniaUserSelectionDialogFactory, AvaloniaUserSelectionDialogFactory>();

        return services;
    }
}
