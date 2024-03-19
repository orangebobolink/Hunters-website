using Chat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Chat.Infrastructure.Contexts
{
    internal class ApplicationDbContext
        : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
