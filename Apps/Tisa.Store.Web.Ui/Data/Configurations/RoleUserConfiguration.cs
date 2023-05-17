using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tisa.Store.Web.Ui.Models.Entities;

namespace Tisa.Store.Web.Ui.Data.Configurations;

public class RoleUserConfiguration : IEntityTypeConfiguration<RoleUser>
{
    public void Configure(EntityTypeBuilder<RoleUser> builder)
    {
        builder.HasKey(keyExpression: roleUser => roleUser.Id);
    }
}