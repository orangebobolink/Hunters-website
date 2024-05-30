using Microsoft.Extensions.DependencyInjection;
using Payment.Application.Configurations;

namespace Payment.API.Configurations
{
    public static class PaymentModuleConfiguration
    {
        public static void AddPaymentModuleConfiguration(this IServiceCollection services)
        {
            services.AddApplicationConfiguration();
        }
    }
}
