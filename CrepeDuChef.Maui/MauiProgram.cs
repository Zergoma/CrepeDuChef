using CommunityToolkit.Maui;

using CrepeDuChef.Application.Extensions;
using AppInterfaces = CrepeDuChef.Application.Interfaces;
using Infrastructure = CrepeDuChef.Infrastructure;

using CrepeDuChef.Infrastructure.DI;
using CrepeDuChef.Maui.DI;

using CrepeDuChef.Maui.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SQLitePCL;

using SkiaSharp.Views.Maui.Controls.Hosting;
using UraniumUI;


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

            builder.Services.AddDbContextFactory<Infrastructure.CrepeDbContext>(
                options =>
                    options.UseSqlite($"Data Source={dbPath}"));

            builder.Services.AddCrepeDuChefInfrastructure();
            builder.Services.AddCrepeDuChefApplication();


            // MAUI Modules
            builder.Services.AddMauiViews();
            builder.Services.AddMauiViewModels();
            builder.Services.AddMauiPopups();
            builder.Services.AddMauiPresenters();

            // Device GUID
            builder.Services.AddSingleton<AppInterfaces.IDeviceIdProvider, MauiDeviceIdProvider>();


#if DEBUG
            DIValidator.Validate(builder.Services);
#endif

            #region Infrastructure Update
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<Infrastructure.CrepeDbContext>();
                db.Database.Migrate();
            }
            #endregion

            return app;
        }
    }
}
