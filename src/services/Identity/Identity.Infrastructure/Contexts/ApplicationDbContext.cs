using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using MassTransit;

namespace Identity.Infrastructure.Contexts
{
    public class ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options, 
        IDataSeeder dataSeeder)
        : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
    {
        private IDataSeeder _dataSeeder = dataSeeder;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _dataSeeder.SeedAsync(modelBuilder).Wait();
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);

            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();
        }
    }
}
