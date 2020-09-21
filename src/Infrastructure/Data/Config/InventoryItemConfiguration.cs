using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.eShopWeb.ApplicationCore.Entities.InventoryAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.eShopWeb.Infrastructure.Data.Config
{
    public class InventoryItemConfiguration : IEntityTypeConfiguration<InventoryItem>
    {
        public void Configure(EntityTypeBuilder<InventoryItem> builder)
        {
            builder.Property(ci => ci.Id)
               .UseHiLo("inventory_hilo")
               .IsRequired();

            builder.Property(ci => ci.CatalogItemId)
                 .IsRequired(true)
                 .HasColumnType("int");

            builder.Property(ci => ci.Quantity)
                .IsRequired(true)
                .HasColumnType("int");

            builder.Property(ci => ci.CreatedDate)
                .IsRequired(true);                

            builder.Property(ci => ci.ModifiedDate)
                .IsRequired(true);            

        }
    }
}
