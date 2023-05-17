using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tisa.Store.Web.Ui.Models.Entities;

namespace Tisa.Store.Web.Ui.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(keyExpression: role => role.Id);

        builder.Property(propertyExpression: role => role.Name)
            .IsRequired();

        builder.HasIndex(indexExpression: role => role.Name)
            .IsUnique();

        builder.HasMany(navigationExpression: role => role.Users)
            .WithOne(navigationExpression: user => user.Role)
            .HasForeignKey(foreignKeyExpression: user => user.UserId);
    }
}