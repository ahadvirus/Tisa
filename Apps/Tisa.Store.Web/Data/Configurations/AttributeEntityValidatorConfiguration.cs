using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tisa.Store.Web.Models.Entities;

namespace Tisa.Store.Web.Data.Configurations;

public class AttributeEntityValidatorConfiguration : IEntityTypeConfiguration<AttributeEntityValidator>
{
    public void Configure(EntityTypeBuilder<AttributeEntityValidator> builder)
    {
        builder.HasKey(validator => validator.Id);

        builder.HasMany(validator => validator.Claims)
            .WithOne(claim => claim.AttributeEntityValidation)
            .HasForeignKey(claim => claim.AttributeEntityValidationId);
    }
}