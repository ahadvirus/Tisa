using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tisa.Store.Web.Models.Entities;

namespace Tisa.Store.Web.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(product => product.Id);
        
        builder.HasIndex(product => new { product.EntityId, product.AttributeEntityId })
            .IsUnique();
        
        builder.Property(product => product.Value)
            .IsRequired();
    }
}