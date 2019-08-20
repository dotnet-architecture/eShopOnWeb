using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.eShopWeb.Infrastructure.Data.Migrations
{
    public partial class UpdatedConstraints : Migration
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
