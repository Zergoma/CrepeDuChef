using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Application.Services;
using CrepeDuChef.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CrepeDuChef.Application.DI
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddCrepeDuChefApplication(this IServiceCollection services)
        {
            services.AddTransient<IChefService, ChefService>();
            services.AddTransient<IChefRotationService, ChefRotationService>();
            services.AddTransient<ICrepePartyService, CrepePartyService>();

            services.AddTransient<IValidator<AddChefDto>, AddChefDtoValidator>();
            services.AddTransient<IValidator<UpdateChefDto>, UpdateChefDtoValidator>();

            return services;
        }
    }
}
