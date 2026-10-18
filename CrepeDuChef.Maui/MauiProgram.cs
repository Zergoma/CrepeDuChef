using CommunityToolkit.Maui;

using CrepeDuChef.Application.Extensions;
using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Application.DI;
using CrepeDuChef.Application.Services;
using CrepeDuChef.Infrastructure.DbContexts;
using CrepeDuChef.Infrastructure.DI;
using CrepeDuChef.Maui.DI;
using CrepeDuChef.Maui.Services;
using CrepeDuChef.UI.Shared.DI;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using SkiaSharp.Views.Maui.Controls.Hosting;

using SQLitePCL;

using UraniumUI;

using AppInterfaces = CrepeDuChef.Application.Interfaces;


namespace CrepeDuChef.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            Batteries_V2.Init();
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseUraniumUI()
                .UseUraniumUIMaterial()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Salon du Chocolat.ttf", "SalonDuChocolat");
                    fonts.AddFont("SwirlyCanalope_PERSONAL_USE_ONLY.otf", "Swirly");
                    fonts.AddFont("Ananda Personal Use.ttf", "Ananda");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder
                .UseSkiaSharp()
                .UseMauiCommunityToolkit();
            
            builder.Services.AddLocalization();

            // Views & Shell
            builder.Services.AddTransient<App>();
            builder.Services.AddTransient<AppShell>();

            string dbPath = 
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "CrepeDuChef.db3"
                    );


            builder.Services.AddDbContextFactory<CrepeDbContext>(
                options =>
                    options.UseSqlite($"Data Source={dbPath}"));

            
            builder.Services.AddCrepeDuChefInfrastructure();
            builder.Services.AddCrepeDuChefApplication();


            // MAUI Modules
            builder.Services.AddMauiViews();
            builder.Services.AddMauiViewModels();
            builder.Services.AddMauiPopups();
            builder.Services.AddMauiPresenters();


            // Network status
            builder.Services.AddSingleton<INetworkStatusService, NetworkStatusService>();


            //var config =
            //    new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //    .Build();

            //builder.Configuration.AddConfiguration(config);

            //string? apiBaseUrl = builder.Configuration["Api:BaseUrl"];
            //ArgumentNullException.ThrowIfNullOrWhiteSpace(apiBaseUrl);


            // Remote HTTP client
            //builder.Services.AddCrepeClientServices(new Uri(apiBaseUrl));

            // Device GUID
            builder.Services.AddSingleton<AppInterfaces.IDeviceIdProvider, MauiDeviceIdProvider>();

            builder.Services.AddSingleton<AppInterfaces.IChefRotationLocalService, ChefRotationLocalService>();




#if DEBUG
            DIValidator.Validate(builder.Services);
#endif
            var app = builder.Build();

            #region Infrastructure Update

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<CrepeDbContext>();
                db.Database.Migrate();
            }
            #endregion

            return app;
        }
    }
}
