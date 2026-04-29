using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Application.Ochestrators;
using CrepeDuChef.Application.Services;
using CrepeDuChef.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CrepeDuChef.Application.Extensions
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddCrepeDuChefApplication(this IServiceCollection services)
        {
            services.AddTransient<IChefRotationService, ChefRotationService>();
            services.AddTransient<ICrepePartyService, CrepePartyService>();
            services.AddTransient<IChefManagementService, ChefManagementService>();

            services.AddTransient<IUserApplicationOrchestrator, UserApplicationOrchestrator>();
            
            services.AddTransient<IValidator<UserDtoAdd>, UserDtoAddValidator>();
            services.AddTransient<IValidator<UserDtoUpdate>, UserDtoUpdateValidator>();

            return services;
        }
    }
}
