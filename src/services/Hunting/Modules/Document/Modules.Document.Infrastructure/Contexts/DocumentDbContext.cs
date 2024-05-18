using Microsoft.EntityFrameworkCore;
using Modules.Document.Domain.Entities;
using Shared.Infrastructure.Contexts;

namespace Modules.Document.Infrastructure.Contexts
{
    internal class DocumentDbContext(
        DbContextOptions<DocumentDbContext> options)
        : ModuleDbContext(options)
    {
        protected override string Schema => "Document";

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Raid> Raids { get; set; }
        public DbSet<HuntingLicense> HuntingLicenses { get; set; }
        public DbSet<Land> Lands { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Feeding> Feedings { get; set; }
        public DbSet<FeedingProduct> FeedingProducts { get; set; }
        public DbSet<TripParticipant> TripParticipants { get; set; }
        public DbSet<PermissionForExtractionOfHuntingAnimal> PermissionForExtractionOfHuntingAnimals { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
