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
            services.AddValidatorsFromAssembly(typeof(RequestRegistrationUserDtoValidator).Assembly);
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
        }
    }
}
