using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tisa.Store.Web.Models.Entities;

namespace Tisa.Store.Web.Data.Configurations;

public class ValidatorClaimConfiguration : IEntityTypeConfiguration<ValidatorClaim>
{
    public void Configure(EntityTypeBuilder<ValidatorClaim> builder)
    {
        builder.HasKey(claim => claim.Id);

        builder.Property(claim => claim.Key)
            .IsRequired();
        
        builder.Property(claim => claim.Value)
            .IsRequired();

        builder.HasIndex(claim => new { claim.Key, claim.ValidatorId })
            .IsUnique();
    }
}