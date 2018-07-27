using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.eShopWeb.Infrastructure.Data.Migrations
{
    public partial class MaxLengthUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ShipToAddress_ZipCode",
                table: "Orders",
                maxLength: 9,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ShipToAddress_Street",
                table: "Orders",
                maxLength: 35,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ShipToAddress_State",
                table: "Orders",
                maxLength: 35,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ShipToAddress_Country",
                table: "Orders",
                maxLength: 70,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ShipToAddress_City",
                table: "Orders",
                maxLength: 35,
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
                name: "ItemOrdered_PictureUri",
                table: "OrderItems",
                maxLength: 2083,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PictureUri",
                table: "Catalog",
                maxLength: 2083,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Catalog",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ShipToAddress_ZipCode",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 9,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ShipToAddress_Street",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 35,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ShipToAddress_State",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 35,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ShipToAddress_Country",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 70,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ShipToAddress_City",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 35,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ItemOrdered_ProductName",
                table: "OrderItems",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ItemOrdered_PictureUri",
                table: "OrderItems",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2083,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PictureUri",
                table: "Catalog",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2083,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Catalog",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250,
                oldNullable: true);
        }
    }
}
