using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tisa.Store.Models.Entities;

namespace Tisa.Store.Models.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(product => product.Id);

            builder.Property(product => product.Store)
                .IsRequired();

            builder.Property(product => product.Title)
                .IsRequired(false);

            builder.Property(product => product.Power)
                .IsRequired(false);

            builder.Property(product => product.Count)
                .IsRequired(false);

            builder.Property(product => product.Unit)
                .IsRequired(false);

            builder.Property(product => product.Description)
                .IsRequired(false);

            builder.Ignore(product => product.Valid);
        }
    }
}
