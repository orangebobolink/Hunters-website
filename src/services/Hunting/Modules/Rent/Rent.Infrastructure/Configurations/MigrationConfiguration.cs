using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Modules.Document.Infrastructure.Contexts;

namespace Modules.Document.Infrastructure.Configurations
{
    public static class MigrationManager
    {
        public static void MigrateDatabase(this IApplicationBuilder applicationBuilder)
        {
            using (IServiceScope scope = applicationBuilder.ApplicationServices.CreateScope())
            {
                using (RentDbContext appContext
                    = scope.ServiceProvider.GetRequiredService<RentDbContext>())
                {
                    try
                    {
                        appContext.Database.Migrate();
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
