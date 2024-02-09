using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreApp.Migrations
{
    public partial class ProductSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "PrdoductName", "Price" },
                values: new object[] { 1, "Computer", 17000m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "PrdoductName", "Price" },
                values: new object[] { 2, "Keyboard", 1000m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "PrdoductName", "Price" },
                values: new object[] { 3, "Mouse", 500m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
