using System;
using System.Globalization;
using System.IO;
using System.Threading;

using Avalonia;

using CrepeDuChef.Application.DI;
using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Avalonia.DI;
using CrepeDuChef.Avalonia.Services;
using CrepeDuChef.Infrastructure.DI;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CrepeDuChef.Avalonia
{
    internal class Program
    {
        public static IHost AppHost { get; private set; } = null!;

        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
#if DEBUG
            Thread.CurrentThread.CurrentCulture = new CultureInfo("fr");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr");
#endif

            AppHost =
                Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddLocalization();

                    services.AddAvaloniaWindowing();
                    services.AddAvaloniaViewModels();
                    services.AddAvaloniaDialogs();
                    services.AddAvaloniaPresenters();

                    services.AddSingleton<IDeviceIdProvider, AvaloniaDeviceIdProvider>();

                    string dbPath =
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                            "CrepeDuChef.db3");

                    services.AddCrepeDuChefInfrastructure(dbPath);
                    services.AddCrepeDuChefApplication();
                })
                .Build();

            AppHost.Services.MigrateCrepeDuChefDatabase();

            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace()
                .AfterSetup(builder =>
                {
                    var app = (App)builder.Instance!;
                    app.InitializeServices(AppHost.Services);
                });
    }
}
