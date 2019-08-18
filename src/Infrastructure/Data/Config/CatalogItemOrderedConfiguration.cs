using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.Infrastructure.Data.Config
{
    public class CatalogItemOrderedConfiguration : IEntityTypeConfiguration<CatalogItemOrdered>
    {
        public void Configure(EntityTypeBuilder<CatalogItemOrdered> builder)
        {
            builder.Property(cio => cio.ProductName)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
