using MassTransit;
using Microsoft.EntityFrameworkCore;
using Modules.Animal.Domain.Entities;
using System.Reflection;

namespace Modules.Animal.Infrastructure.Contexts
{
    internal class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : DbContext(options)
    {
        public DbSet<AnimalInfo> Animals { get; set; }
        public DbSet<HuntingSeason> HuntingSeasons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);

            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();
        }
    }
}
