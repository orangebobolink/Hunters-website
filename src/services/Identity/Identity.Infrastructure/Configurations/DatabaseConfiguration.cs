using Identity.Domain.Entities;
using Identity.Domain.Interfaces;
using Identity.Infrastructure.Contexts;
using Identity.Infrastructure.DataSeed;
using Identity.Infrastructure.Interfaces;
using Identity.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure.Configurations
{
    public static class DatabaseConfiguration
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection")!;
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString), ServiceLifetime.Scoped);

            services.AddIdentity<User, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<IDataSeeder, DataSeeder>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IHyntingLicenseRepository, HuntingLicenseRepository>();
        }
    }
}
