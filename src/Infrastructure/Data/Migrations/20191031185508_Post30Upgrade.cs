using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.eShopWeb.Infrastructure.Data.Migrations
{
    public partial class Post30Upgrade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Catalog_CatalogBrand_CatalogBrandId",
                table: "Catalog");

            migrationBuilder.DropForeignKey(
                name: "FK_Catalog_CatalogType_CatalogTypeId",
                table: "Catalog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CatalogType",
                table: "CatalogType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CatalogBrand",
                table: "CatalogBrand");

            migrationBuilder.RenameTable(
                name: "CatalogType",
                newName: "CatalogTypes");

            migrationBuilder.RenameTable(
                name: "CatalogBrand",
                newName: "CatalogBrands");

            migrationBuilder.AlterColumn<string>(
                name: "ShipToAddress_ZipCode",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 18,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ShipToAddress_Street",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 180,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ShipToAddress_State",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ShipToAddress_Country",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 90,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ShipToAddress_City",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ItemOrdered_ProductName",
                table: "OrderItems",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BuyerId",
                table: "Baskets",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CatalogTypes",
                table: "CatalogTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CatalogBrands",
                table: "CatalogBrands",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Catalog_CatalogBrands_CatalogBrandId",
                table: "Catalog",
                column: "CatalogBrandId",
                principalTable: "CatalogBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Catalog_CatalogTypes_CatalogTypeId",
                table: "Catalog",
                column: "CatalogTypeId",
                principalTable: "CatalogTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Catalog_CatalogBrands_CatalogBrandId",
                table: "Catalog");

            migrationBuilder.DropForeignKey(
                name: "FK_Catalog_CatalogTypes_CatalogTypeId",
                table: "Catalog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CatalogTypes",
                table: "CatalogTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CatalogBrands",
                table: "CatalogBrands");

            migrationBuilder.RenameTable(
                name: "CatalogTypes",
                newName: "CatalogType");

            migrationBuilder.RenameTable(
                name: "CatalogBrands",
                newName: "CatalogBrand");

            migrationBuilder.AlterColumn<string>(
                name: "ShipToAddress_ZipCode",
                table: "Orders",
                maxLength: 18,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ShipToAddress_Street",
                table: "Orders",
                maxLength: 180,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ShipToAddress_State",
                table: "Orders",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ShipToAddress_Country",
                table: "Orders",
                maxLength: 90,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ShipToAddress_City",
                table: "Orders",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ItemOrdered_ProductName",
                table: "OrderItems",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BuyerId",
                table: "Baskets",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 40);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CatalogType",
                table: "CatalogType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CatalogBrand",
                table: "CatalogBrand",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Catalog_CatalogBrand_CatalogBrandId",
                table: "Catalog",
                column: "CatalogBrandId",
                principalTable: "CatalogBrand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Catalog_CatalogType_CatalogTypeId",
                table: "Catalog",
                column: "CatalogTypeId",
                principalTable: "CatalogType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
