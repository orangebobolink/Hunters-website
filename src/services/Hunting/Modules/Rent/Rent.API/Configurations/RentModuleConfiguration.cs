using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rent.Application.Configurations;
using Rent.Infrastructure.Configurations;

namespace Rent.API.Configurations
{
    public static class RentModuleConfiguration
    {
        public static void AddRentModuleConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddApplicationConfiguration();
            services.AddInfrastructureConfiguration(configuration);
        }
    }
}
