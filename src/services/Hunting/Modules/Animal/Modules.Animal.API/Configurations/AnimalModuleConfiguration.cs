using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Animal.Infrastructure.Configurations;
using Modules.Animal.Application.Configurations;

namespace Modules.Animal.API.Configurations
{
    public static class AnimalModuleConfiguration
    {
        public static void AddAnimalModuleConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabaseConfiguration(configuration);
            services.AddMediatRConfiguration();
            services.AddInfrastructureConfiguration();
        }
    }
}
