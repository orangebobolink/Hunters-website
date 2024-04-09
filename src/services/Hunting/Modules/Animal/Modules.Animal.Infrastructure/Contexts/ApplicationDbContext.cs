using MassTransit;
using Microsoft.EntityFrameworkCore;
using Modules.Animal.Domain.Entities;
using Shared.Infrastructure.Contexts;
using System.Reflection;

namespace Modules.Animal.Infrastructure.Contexts
{
    internal class ApplicationDbContext(DbContextOptions options) : ModuleDbContext(options)
    {
        protected override string Schema => "Animal";

        public DbSet<AnimalInfo> Animals { get; set; }
        public DbSet<HuntingSeason> HuntingSeasons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
