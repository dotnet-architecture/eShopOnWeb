using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace Microsoft.eShopWeb.Infrastructure.Data.Config;

public class CatalogStockConfiguration : IEntityTypeConfiguration<CatalogStock>
{
    public void Configure(EntityTypeBuilder<CatalogStock> builder)
    {
        builder.ToTable("CatalogStock");

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

    }
}
