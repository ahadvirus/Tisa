using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tisa.Store.Web.Ui.Data.Configurations;

public class AttributeConfiguration : IEntityTypeConfiguration<Models.Entities.Attribute>
{
    public void Configure(EntityTypeBuilder<Models.Entities.Attribute> builder)
    {
        builder.HasKey(keyExpression: attribute => attribute.Id);

        builder.HasIndex(indexExpression: attribute => attribute.AttributeId)
            .IsUnique();

        builder.Property(propertyExpression: attribute => attribute.Name)
            .IsRequired();
    }
}