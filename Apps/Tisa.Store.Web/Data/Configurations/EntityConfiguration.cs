using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tisa.Store.Web.Models.Entities;

namespace Tisa.Store.Web.Data.Configurations;

public class EntityConfiguration : IEntityTypeConfiguration<Entity>
{
    public void Configure(EntityTypeBuilder<Entity> builder)
    {
        builder.HasKey(product => product.Id);

        builder.Property(product => product.Name)
            .IsRequired();

        builder.HasIndex(product => product.Name)
            .IsUnique();

        builder.HasMany(product => product.Attributes)
            .WithOne(attribute => attribute.Entity)
            .HasForeignKey(attribute => attribute.EntityId);
    }
}