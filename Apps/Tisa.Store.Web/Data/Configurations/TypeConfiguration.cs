using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tisa.Store.Web.Models.Entities;

namespace Tisa.Store.Web.Data.Configurations;

public class TypeConfiguration : IEntityTypeConfiguration<Type>
{
    public void Configure(EntityTypeBuilder<Type> builder)
    {
        builder.HasKey(type => type.Id);

        builder.Property(type => type.Kind)
            .IsRequired();

        builder.HasIndex(type => type.Kind)
            .IsUnique();

        builder.HasMany(type => type.Attributes)
            .WithOne(attribute => attribute.Type)
            .HasForeignKey(attribute => attribute.TypeId);
        
        builder.HasMany(type => type.Validators)
            .WithOne(validator => validator.Type)
            .HasForeignKey(validator => validator.TypeId);
    }
}