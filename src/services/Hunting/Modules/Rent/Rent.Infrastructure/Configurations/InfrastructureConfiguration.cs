using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Document.Infrastructure.Contexts;
using Rent.Domain.Interfaces;
using Rent.Infrastructure.Repositories;
using Shared.Infrastructure.Configurations;

namespace Rent.Infrastructure.Configurations
{
    public static class InfrastructureConfiguration
    {
        public static void AddInfrastructureConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDatabaseConfiguration<RentDbContext>(configuration);

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRentProductRepository, RentProductRepository>();
        }
    }
}
