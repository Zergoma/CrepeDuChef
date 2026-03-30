using CommunityToolkit.Maui;
using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Application.Ochestrators;
using CrepeDuChef.Application.Services;
using CrepeDuChef.Application.Validation;
using CrepeDuChef.Domain.Interfaces;
using CrepeDuChef.Infrastructure;
using CrepeDuChef.Infrastructure.Extensions;
using CrepeDuChef.Infrastructure.Services;
using CrepeDuChef.Maui.MVVM.ViewModels;
using CrepeDuChef.Maui.MVVM.Views;
using CrepeDuChef.Maui.UI.Dialogs;
using CrepeDuChef.Maui.UI.Popups.Factories;
using CrepeDuChef.Maui.UI.Popups.Presenters;
using FluentValidation;
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



            builder.Services.AddTransient<App>();
            builder.Services.AddTransient<AppShell>();
            builder.Services.AddTransient<CrepeDuChefViewModel>();
            builder.Services.AddTransient<CrepeDuChefView>();

            builder.Services.AddTransient<UserSettingViewModel>();
            builder.Services.AddTransient<UserSettingView>();

            string dbPath = 
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "CrepeDuChef.db3"
                    );
            builder.Services.AddCrepeDuChefInfrastructure(dbPath);

            builder.Services.AddTransient<IRandomProvider, DefaultRandomProvider>();
            builder.Services.AddTransient<IChefRotationService, ChefRotationService>();

            builder.Services.AddSingleton<IDialogPresenter, DialogPresenter>();
            builder.Services.AddSingleton<IUserPopupPresenter, UserPopupPresenter>();

            builder.Services.AddTransient<IUserFormPopupFactory, UserPopCommunautyFactory>();

            builder.Services.AddTransient<ICrepePartyService, CrepePartyService>();

            builder.Services.AddTransient<IChefManagementService, ChefManagementService>();
            builder.Services.AddTransient<IUserApplicationOrchestrator, UserApplicationOrchestrator>();


            builder.Services.AddTransient<IValidator<UserDtoAdd>, UserDtoAddValidator>();
            builder.Services.AddTransient<IValidator<UserDtoUpdate>, UserDtoUpdateValidator>();




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
