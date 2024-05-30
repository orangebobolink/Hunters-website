using Microsoft.EntityFrameworkCore;
using Rent.Domain.Entities;
using Shared.Infrastructure.Contexts;

namespace Modules.Document.Infrastructure.Contexts
{
    internal class RentDbContext(
        DbContextOptions<RentDbContext> options)
        : ModuleDbContext(options)
    {
        protected override string Schema => "Rent";

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
