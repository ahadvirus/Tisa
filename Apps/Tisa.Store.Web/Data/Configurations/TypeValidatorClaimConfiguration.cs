using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tisa.Store.Web.Models.Entities;

namespace Tisa.Store.Web.Data.Configurations;

public class TypeValidatorClaimConfiguration : IEntityTypeConfiguration<TypeValidatorClaim>
{
    public void Configure(EntityTypeBuilder<TypeValidatorClaim> builder)
    {
        builder.HasKey(claim => claim.Id);

        builder.Property(claim => claim.Key)
            .IsRequired();

        builder.Property(claim => claim.Value)
            .IsRequired();

        builder.HasIndex(claim => new { claim.Key, claim.TypeValidatorId })
            .IsUnique();
    }
}