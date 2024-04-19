using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Animal.Domain.Entities;

namespace Modules.Animal.Infrastructure.Configurations.EntityTypeConfigurations
{
    internal class AnimalInfoConfiguration
        : IEntityTypeConfiguration<AnimalInfo>
    {
        public void Configure(EntityTypeBuilder<AnimalInfo> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(x => x.Name)
                .IsUnique();

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.Type)
                .IsRequired();

            builder.Property(x => x.ImageUrl)
                .HasMaxLength(200);
        }
    }
}
