using System;
using System.Globalization;
using System.IO;
using System.Threading;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

using CrepeDuChef.Application.DI;
using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Avalonia.DI;
using CrepeDuChef.Avalonia.Services;
using CrepeDuChef.Infrastructure.DbContexts;
using CrepeDuChef.Infrastructure.DI;
using CrepeDuChef.UI.Shared.DI;
using CrepeDuChef.UI.Shared.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SkiaSharp;

using AppInfra = CrepeDuChef.Infrastructure;
using AppInterfaces = CrepeDuChef.Application.Interfaces;
using avaApp = Avalonia.Application;


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
                .ConfigureAppConfiguration(config =>
                {
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices(
                    (context, services) =>
                    {
                        services.AddLocalization();

                        // Navigation
                        services.AddSingleton<INavigationService, NavigationService>();

                        // Dialog service
                        services.AddSingleton<IDialogService, DialogService>();

                        // Main Window holder
                        services.AddAvaloniaWindowing();
                        
                        // Avalonia DI modules
                        services.AddAvaloniaViews();
                        services.AddAvaloniaViewModels();
                        services.AddAvaloniaDialogs();
                        services.AddAvaloniaPresenters();

                        string? apiBaseUrl = context.Configuration["Api:BaseUrl"];
                        ArgumentNullException.ThrowIfNullOrWhiteSpace(apiBaseUrl);

                        services.AddCrepeClientServices(new Uri(apiBaseUrl));

                        // Device GUID
                        services.AddSingleton<AppInterfaces.IDeviceIdProvider, AvaloniaDeviceIdProvider>();

                        // Database
                        string dbPath =
                            Path.Combine(
                                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                "CrepeDuChef.db3"
                                );
                        services.AddDbContextFactory<CrepeDbContext>(
                            options =>
                                options.UseSqlite($"Data Source={dbPath}"));

                        services.AddCrepeDuChefInfrastructure();
                        services.AddCrepeDuChefApplication();

                        services.AddSingleton<Window>(sp =>
                        {
                            var lifetime =
                                (IClassicDesktopStyleApplicationLifetime)avaApp.Current!.ApplicationLifetime!;

                            return lifetime.MainWindow!;
                        });

                    })
                .Build();

            // Run EF Core migrations
            using (var scope = AppHost.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<CrepeDbContext>();
                db.Database.Migrate();
            }

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
