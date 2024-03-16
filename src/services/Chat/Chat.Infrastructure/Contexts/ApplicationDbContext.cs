using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Chat.Infrastructure.Contexts
{
    internal class ApplicationDbContext
        : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
