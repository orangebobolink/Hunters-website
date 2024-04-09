using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.API.Configurations
{
    public static class MigrationManager
    {
        public static void MigrateDatabase<T>(this IApplicationBuilder applicationBuilder)
             where T : DbContext
        {
            using(IServiceScope scope = applicationBuilder.ApplicationServices.CreateScope())
            {
                using(T appContext = scope.ServiceProvider.GetRequiredService<T>())
                {
                    try
                    {
                        appContext.Database.Migrate();
                        //IDataSeeder dataSeed = scope.ServiceProvider.GetRequiredService<IDataSeeder>();

                        //dataSeed.SeedAsync().Wait();
                        //dataSeed.SeedMessageAsync().Wait();
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
