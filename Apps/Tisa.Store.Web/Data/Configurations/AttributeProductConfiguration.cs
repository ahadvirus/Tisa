using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tisa.Store.Web.Models.Entities;

namespace Tisa.Store.Web.Data.Configurations
{
    public class AttributeProductConfiguration : IEntityTypeConfiguration<AttributeProduct>
    {
        public void Configure(EntityTypeBuilder<AttributeProduct> builder)
        {
            builder.HasIndex(attribute => attribute.Id);

            builder.HasIndex(attribute => new { attribute.AttributeId, attribute.ProductId })
                .IsUnique(true);
        }
    }
}
