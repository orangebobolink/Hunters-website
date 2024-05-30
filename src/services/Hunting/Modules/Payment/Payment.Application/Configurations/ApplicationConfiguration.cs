using Microsoft.Extensions.DependencyInjection;
using Payment.Application.Interfaces;
using Payment.Application.Services;

namespace Payment.Application.Configurations
{
    public static class ApplicationConfiguration
    {
        public static void AddApplicationConfiguration(this IServiceCollection services)
        {
            services.AddScoped<ITripPaymentService, TripPaymentService>();
            services.AddScoped<IHuntingLicensePaymentService, HuntingLicensePaymentService>();
        }
    }
}
