using Chat.Infrastructure.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Infrastructure.Configurations
{
    public static class MigrationConfiguration
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
