using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tisa.Store.Web.Data.Converters;
using Tisa.Store.Web.Infrastructures.Contracts.Validator;
using Tisa.Store.Web.Models.Entities;

namespace Tisa.Store.Web.Data.Configurations;

public class ValidatorConfiguration : IEntityTypeConfiguration<Validator>
{
    public void Configure(EntityTypeBuilder<Validator> builder)
    {
        builder.HasKey(validator => validator.Id);

        builder.Property(validator => validator.Name)
            .IsRequired();

        builder.Property(validator => validator.Description)
            .IsRequired();
        
        builder.HasMany(validator => validator.Types)
            .WithOne(type => type.Validator)
            .HasForeignKey(type => type.ValidatorId);

        builder.HasMany(validator => validator.Claims)
            .WithOne(claim => claim.Validator)
            .HasForeignKey(claim => claim.ValidatorId);
    }
}