using Identity.Infrastructure.Contexts;
using Identity.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.API.Configurations
{
    public static class MigrationManager
    {
        public static void MigrateDatabase(this IApplicationBuilder applicationBuilder)
        {
            using(IServiceScope scope = applicationBuilder.ApplicationServices.CreateScope())
            {
                using(ApplicationDbContext appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
                {
                    try
                    {
                        if(!appContext.Users.Any())
                        {
                            appContext.Database.Migrate();
                        }

                        IDataSeeder dataSeed = scope.ServiceProvider.GetRequiredService<IDataSeeder>();

                        dataSeed.SeedAsync().Wait();
                        dataSeed.SeedMessageAsync().Wait();
                    }
                    catch 
                    {
                        throw;
                    }
                }
            }
        }
    }
}
