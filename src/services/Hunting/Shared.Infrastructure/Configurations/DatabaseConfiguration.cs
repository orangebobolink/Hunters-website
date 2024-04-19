using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Infrastructure.Configurations
{
    public static class DatabaseConfiguration
    {
        public static void AddDatabaseConfiguration<T>(this IServiceCollection services, IConfiguration configuration)
            where T : DbContext
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection")!;
            services.AddDbContext<T>(options =>
                options.UseSqlServer(connectionString, e => e.MigrationsAssembly(typeof(T).Assembly.FullName)),
                ServiceLifetime.Scoped);
        }
    }
}
