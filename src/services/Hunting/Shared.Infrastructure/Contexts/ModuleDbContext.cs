using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Shared.Infrastructure.Contexts
{
    public abstract class ModuleDbContext(DbContextOptions options) 
        : DbContext(options)
    {
        protected abstract string Schema { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if(!string.IsNullOrWhiteSpace(Schema))
            {
                modelBuilder.HasDefaultSchema(Schema);
            }
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();
        }
    }
}
