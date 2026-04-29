using Microsoft.Extensions.DependencyInjection;

namespace CrepeDuChef.Avalonia.DI
{
    public static class AvaloniaWindowing
    {
        public static IServiceCollection AddAvaloniaWindowing(this IServiceCollection services)
        {
            services.AddSingleton<MainWindowHolder>();
            return services;
        }
    }
}
