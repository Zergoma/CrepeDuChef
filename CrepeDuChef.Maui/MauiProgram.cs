using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Views;
using CrepeDuChef.Application.Services;
using CrepeDuChef.Common;
using CrepeDuChef.Common.DTOs;
using CrepeDuChef.Common.Interfaces;
using CrepeDuChef.Common.Mappers;
using CrepeDuChef.Infrastructure;
using CrepeDuChef.Infrastructure.Extensions;
using CrepeDuChef.Maui.Factories;
using CrepeDuChef.Maui.Mappers;
using CrepeDuChef.Maui.MVVM.ViewModels;
using CrepeDuChef.Maui.MVVM.Views;
using CrepeDuChef.Maui.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;
using SQLitePCL;
using UraniumUI;

namespace CrepeDuChef.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            Batteries_V2.Init();  // <-- ici
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
                

            builder.Services.AddTransient<App>();
            builder.Services.AddTransient<AppShell>();
            builder.Services.AddTransient<CrepeDuChefViewModel>();
            builder.Services.AddTransient<CrepeDuChefView>();

            builder.Services.AddTransient<UserSettingViewModel>();
            builder.Services.AddTransient<UserSettingView>();

            builder.Services.UseCrepeDuChefSqliteDb();

            builder.Services.AddTransient<IRandomProvider, DefaultRandomProvider>();
            builder.Services.AddTransient<IChefRotationService, ChefRotationService>();

            builder.Services.AddSingleton<IUserDialogService, CrepeDuChefViewDialogService>();
            builder.Services.AddSingleton<IUserDtoPopupService, UserDtoPopupService>();

            builder.Services.AddTransient<IUserPopupFactory<Popup, UserDto>, UserPopCommunautyFactory>();

            builder.Services.AddTransient<ITradCrepePartyDefault, LocalizedCrepePartySession>();
            builder.Services.AddTransient<ICrepePartyService, CrepePartyService>();


            builder.Services.AddTransient<UserFormResultMapper>();
            builder.Services.AddTransient<IUserFormResultMapper>(sp =>
            {
                var baseMapper = sp.GetRequiredService<UserFormResultMapper>();
                return new LocalizedUserFormResultMapper(baseMapper);
            });
         


            #region Infrastructure Update
        var app = builder.Build();

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
