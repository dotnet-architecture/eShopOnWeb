using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.Infrastructure.Data
{

    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {
        }

        public DbSet<Basket> Baskets { get; set; }
        public DbSet<CatalogItem> CatalogItems { get; set; }
        public DbSet<CatalogBrand> CatalogBrands { get; set; }
        public DbSet<CatalogType> CatalogTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Basket>(ConfigureBasket);
            builder.Entity<CatalogBrand>(ConfigureCatalogBrand);
            builder.Entity<CatalogType>(ConfigureCatalogType);
            builder.Entity<CatalogItem>(ConfigureCatalogItem);
            builder.Entity<Order>(ConfigureOrder);
            builder.Entity<OrderItem>(ConfigureOrderItem);
            builder.Entity<Address>(ConfigureAddress);
            builder.Entity<CatalogItemOrdered>(ConfigureItemOrdered);
        }

        private void ConfigureItemOrdered(EntityTypeBuilder<CatalogItemOrdered> builder)
        {
            //https://stackoverflow.com/questions/417142/what-is-the-maximum-length-of-a-url-in-different-browsers/417184#417184
            builder.Property(ci => ci.PictureUri)
                   .HasMaxLength(2083);
            builder.Property(ci => ci.ProductName)
                  .HasMaxLength(50);
        }

        private void ConfigureAddress(EntityTypeBuilder<Address> builder)
        {
            //https://ss64.com/sql/syntax-field-sizes.html
            builder.Property(ci => ci.City)
                .HasMaxLength(35);
            builder.Property(ci => ci.Country)
                .HasMaxLength(70);
            builder.Property(ci => ci.State)
                .HasMaxLength(35);
            builder.Property(ci => ci.Street)
                    .HasMaxLength(35);
            builder.Property(ci => ci.ZipCode)
                   .HasMaxLength(9);
        }

        private void ConfigureBasket(EntityTypeBuilder<Basket> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(Basket.Items));

            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        private void ConfigureCatalogItem(EntityTypeBuilder<CatalogItem> builder)
        {
            builder.ToTable("Catalog");

            builder.Property(ci => ci.Id)
                .ForSqlServerUseSequenceHiLo("catalog_hilo")
                .IsRequired();

            builder.Property(ci => ci.Description)
                // Description should not be large.
                .HasMaxLength(250);

            builder.Property(ci => ci.Name)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(ci => ci.Price)
                .IsRequired(true);

            builder.Property(ci => ci.PictureUri)
                //https://stackoverflow.com/questions/417142/what-is-the-maximum-length-of-a-url-in-different-browsers/417184#417184
                .HasMaxLength(2083)
                .IsRequired(false);

            builder.HasOne(ci => ci.CatalogBrand)
                .WithMany()
                .HasForeignKey(ci => ci.CatalogBrandId);

            builder.HasOne(ci => ci.CatalogType)
                .WithMany()
                .HasForeignKey(ci => ci.CatalogTypeId);
        }

        private void ConfigureCatalogBrand(EntityTypeBuilder<CatalogBrand> builder)
        {
            builder.ToTable("CatalogBrand");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
               .ForSqlServerUseSequenceHiLo("catalog_brand_hilo")
               .IsRequired();

            builder.Property(cb => cb.Brand)
                .IsRequired()
                .HasMaxLength(100);
        }

        private void ConfigureCatalogType(EntityTypeBuilder<CatalogType> builder)
        {
            builder.ToTable("CatalogType");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
               .ForSqlServerUseSequenceHiLo("catalog_type_hilo")
               .IsRequired();

            builder.Property(cb => cb.Type)
                .IsRequired()
                .HasMaxLength(100);
        }

        private void ConfigureOrder(EntityTypeBuilder<Order> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(Order.OrderItems));

            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.OwnsOne(o => o.ShipToAddress);
        }

        private void ConfigureOrderItem(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(i => i.ItemOrdered);

        }
    }
}
