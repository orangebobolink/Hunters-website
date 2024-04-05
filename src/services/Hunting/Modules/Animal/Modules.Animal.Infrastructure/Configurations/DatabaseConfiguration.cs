using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Animal.Infrastructure.Contexts;

namespace Modules.Animal.Infrastructure.Configurations
{
    public static class DatabaseConfiguration
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection")!;
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString), ServiceLifetime.Scoped);
        }
    }
}
