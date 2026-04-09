using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace CrepeDuChef.Maui.DI;

public static class DIValidator
{
    public static void Validate(IServiceCollection services)
    {
        var provider = services.BuildServiceProvider();

        foreach (var descriptor in services)
        {
            try
            {
                provider.GetRequiredService(descriptor.ServiceType);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ DI ERROR: {descriptor.ServiceType.Name} → {ex.Message}");
            }
        }

        Debug.WriteLine("✔ DI validation completed");
    }
}
