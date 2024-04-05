
using Microsoft.Extensions.DependencyInjection;
using Modules.Animal.Domain.Interfaces.Repositories;
using Modules.Animal.Infrastructure.Repositories;

namespace Modules.Animal.Infrastructure.Configurations
{
    public static class InfrastructureConfiguration
    {
        public static void AddInfrastructureConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IAnimalRepository, AnimalRepository>();
            services.AddScoped<IHuntingSeasonRepository, HuntingSeasonRepositpry>();
        }
    }
}
