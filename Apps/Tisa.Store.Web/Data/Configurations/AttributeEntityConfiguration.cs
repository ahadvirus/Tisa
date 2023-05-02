using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tisa.Store.Web.Models.Entities;

namespace Tisa.Store.Web.Data.Configurations;

public class AttributeEntityConfiguration : IEntityTypeConfiguration<AttributeEntity>
{
    public void Configure(EntityTypeBuilder<AttributeEntity> builder)
    {
        builder.HasIndex(attribute => attribute.Id);

        builder.HasIndex(attribute => new { attribute.AttributeId, ProductId = attribute.EntityId })
            .IsUnique();

        builder.HasMany(attribute => attribute.Validators)
            .WithOne(validator => validator.AttributeEntity)
            .HasForeignKey(validator => validator.AttributeEntityId);
        
        builder.HasMany(attribute => attribute.Values)
            .WithOne(product => product.AttributeEntity)
            .HasForeignKey(product => product.AttributeEntityId);
    }
}