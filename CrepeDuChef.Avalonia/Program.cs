using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using CrepeDuChef.Application.Extensions;
using CrepeDuChef.Avalonia.DI;
using CrepeDuChef.Avalonia.Services;
using CrepeDuChef.Infrastructure.Extensions;    // for db Migrate
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using AppInfra = CrepeDuChef.Infrastructure;
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
                .ConfigureServices(
                    services =>
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


                        // Database
                        string dbPath =
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                            "CrepeDuChef.db3"
                            );

                        services.AddCrepeDuChefInfrastructure(dbPath);
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
                var db = scope.ServiceProvider.GetRequiredService<AppInfra.CrepeDbContext>();
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
