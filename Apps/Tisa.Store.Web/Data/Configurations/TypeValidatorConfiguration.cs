using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tisa.Store.Web.Models.Entities;

namespace Tisa.Store.Web.Data.Configurations;

public class TypeValidatorConfiguration : IEntityTypeConfiguration<TypeValidator>
{
    public void Configure(EntityTypeBuilder<TypeValidator> builder)
    {
        builder.HasKey(typeValidator => typeValidator.Id);
        
        builder.HasIndex(typeValidator => new { typeValidator.TypeId, typeValidator.ValidatorId })
            .IsUnique();
        
        builder.HasMany(typeValidator => typeValidator.Claims)
            .WithOne(claim => claim.TypeValidator)
            .HasForeignKey(claim => claim.TypeValidatorId);
    }
}