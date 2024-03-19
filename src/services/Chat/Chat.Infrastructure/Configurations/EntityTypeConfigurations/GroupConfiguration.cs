using Chat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infrastructure.Configurations.EntityTypeConfigurations
{
    internal class GroupConfiguration
        : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.IsDeleted)
                .IsRequired();

            builder.Property(x => x.DeleteTime)
                .IsRequired();

            builder.Property(x => x.AverageRating)
                .IsRequired();
        }
    }
}
