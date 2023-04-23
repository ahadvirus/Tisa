using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tisa.Store.Web.Models.Entities;

namespace Tisa.Store.Web.Data.Configurations;

public class AttributeConfiguration : IEntityTypeConfiguration<Attribute>
{
    public void Configure(EntityTypeBuilder<Attribute> builder)
    {
        builder.HasKey(attribute => attribute.Id);

        builder.Property(attribute => attribute.Name)
            .IsRequired();

        builder.Property(attribute => attribute.Title)
            .IsRequired();
    }
}