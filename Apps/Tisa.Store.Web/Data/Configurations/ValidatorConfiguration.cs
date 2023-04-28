using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tisa.Store.Web.Models.Entities;

namespace Tisa.Store.Web.Data.Configurations;

public class ValidatorConfiguration : IEntityTypeConfiguration<Validator>
{
    public void Configure(EntityTypeBuilder<Validator> builder)
    {
        builder.HasKey(validator => validator.Id);
        
        builder.Property(validator => validator.Name)
            .IsRequired();

        builder.Property(validator => validator.Title)
            .IsRequired();

        builder.HasMany(validator => validator.Types)
            .WithOne(type => type.Validator)
            .HasForeignKey(type => type.ValidatorId);

    }
}