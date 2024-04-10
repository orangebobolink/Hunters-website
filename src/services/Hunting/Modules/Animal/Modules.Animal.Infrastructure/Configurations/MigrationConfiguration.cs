using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Modules.Animal.Infrastructure.Contexts;
using Modules.Animal.Infrastructure.Interfaces;
using Shared.Infrastructure.Interfaces;

namespace Modules.Animal.Infrastructure.Configurations
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
                        if(!appContext.Animals.Any())
                        {
                            appContext.Database.Migrate();
                        }

                        var dataSeed = scope.ServiceProvider.GetRequiredService<IAnimalDataSeeder>();

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
