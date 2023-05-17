using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tisa.Store.Web.Ui.Data.Configurations;

public class TypeConfiguration : IEntityTypeConfiguration<Models.Entities.Type>
{
    public void Configure(EntityTypeBuilder<Models.Entities.Type> builder)
    {
        builder.HasKey(keyExpression: type => type.Id);

        builder.HasIndex(indexExpression: type => type.TypeId)
            .IsUnique();

        builder.Property(propertyExpression: type => type.Name)
            .IsRequired();
    }
}