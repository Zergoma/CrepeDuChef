using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Application.Ochestrators;
using CrepeDuChef.Application.Services;
using CrepeDuChef.Application.Validation;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

namespace CrepeDuChef.Application.DI
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddCrepeDuChefApplication(this IServiceCollection services)
        {
            services.AddTransient<IChefOrchestrator, ChefOrchestrator>();
            services.AddTransient<IUserOrchestrator, UserOrchestrator>();

            services.AddTransient<IChefLocalService, ChefLocalService>();
            services.AddTransient<ICrepePartyLocalService, CrepePartyLocalService>();
            services.AddTransient<ICrepePartyDomainLocalService, CrepePartyDomainLocalService>();


            services.AddTransient<IValidator<UserDtoAdd>, UserDtoAddValidator>();
            services.AddTransient<IValidator<UserDtoUpdate>, UserDtoUpdateValidator>();

            return services;
        }
    }
}
