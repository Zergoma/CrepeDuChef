using CommunityToolkit.Maui;

using CrepeDuChef.Application.Extensions;
using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Infrastructure.Extensions;
using CrepeDuChef.Maui.DI;
using CrepeDuChef.Maui.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SkiaSharp.Views.Maui.Controls.Hosting;

using SQLitePCL;

using UraniumUI;

using AppInfra = CrepeDuChef.Infrastructure;

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

            builder.Services.AddCrepeDuChefInfrastructure(dbPath);
            builder.Services.AddCrepeDuChefApplication();


            // MAUI Modules
            builder.Services.AddMauiViews();
            builder.Services.AddMauiViewModels();
            builder.Services.AddMauiPopups();
            builder.Services.AddMauiPresenters();

            // Device GUID
            builder.Services.AddSingleton<IDeviceIdProvider, MauiDeviceIdProvider>();


#if DEBUG
            DIValidator.Validate(builder.Services);
#endif

            #region Infrastructure Update
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppInfra.CrepeDbContext>();
                db.Database.Migrate();
            }
            #endregion

            return app;
        }
    }
}
