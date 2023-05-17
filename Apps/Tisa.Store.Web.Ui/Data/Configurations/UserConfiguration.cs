using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tisa.Store.Web.Ui.Models.Entities;

namespace Tisa.Store.Web.Ui.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(keyExpression: user => user.Id);

        builder.Property(propertyExpression: user => user.Username)
            .IsRequired();

        builder.Property(propertyExpression: user => user.Password)
            .IsRequired();

        builder.HasIndex(indexExpression: user => user.Username)
            .IsUnique();

        builder.HasMany(navigationExpression: user => user.Roles)
            .WithOne(navigationExpression: role => role.User)
            .HasForeignKey(foreignKeyExpression: role => role.UserId);
    }
}