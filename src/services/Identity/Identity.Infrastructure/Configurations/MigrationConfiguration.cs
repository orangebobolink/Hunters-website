using Identity.Infrastructure.Contexts;
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
                        appContext.Database.Migrate();
                    }
                    catch(Exception ex)
                    {
                        //Log errors or do anything you think it's needed
                        throw;
                    }
                }
            }
        }
    }
}
