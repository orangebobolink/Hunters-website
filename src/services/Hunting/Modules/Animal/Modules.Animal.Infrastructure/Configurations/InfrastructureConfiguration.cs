
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Animal.Domain.Interfaces.Repositories;
using Modules.Animal.Infrastructure.Contexts;
using Modules.Animal.Infrastructure.Repositories;
using Shared.Infrastructure.Configurations;

namespace Modules.Animal.Infrastructure.Configurations
{
    public static class InfrastructureConfiguration
    {
        public static void AddInfrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabaseConfiguration<ApplicationDbContext>(configuration);
            services.AddScoped<IAnimalRepository, AnimalRepository>();
            services.AddScoped<IHuntingSeasonRepository, HuntingSeasonRepositpry>();
        }
    }
}
