using FluentValidation;
using FluentValidation.AspNetCore;
using Identity.Services.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Services.Configurations
{
    public static class FluentValidationConfiguration
    {
        public static void AddFluentValidationConfiguration(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssembly(typeof(RequestUserDtoValidator).Assembly);
        }
    }
}
