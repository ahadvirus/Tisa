using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tisa.Store.Web.Models.Entities;

namespace Tisa.Store.Web.Data.Configurations;

public class AttributeEntityValidatorClaimConfiguration : IEntityTypeConfiguration<AttributeEntityValidatorClaim>
{
    public void Configure(EntityTypeBuilder<AttributeEntityValidatorClaim> builder)
    {
        builder.HasIndex(claim => claim.Id);

        builder.Property(claim => claim.Key)
            .IsRequired();
        
        builder.Property(claim => claim.Value)
            .IsRequired();
        
        builder.HasIndex(claim => new { claim.AttributeEntityValidationId, claim.Key })
            .IsUnique();
    }
}