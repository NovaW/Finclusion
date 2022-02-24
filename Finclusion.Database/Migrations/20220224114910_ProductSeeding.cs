using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finclusion.Database.Migrations
{
    public partial class ProductSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "ProductName", "Quantity" },
                values: new object[] { 1, 50, "Chair", 7 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "ProductName", "Quantity" },
                values: new object[] { 2, 150, "Table", 4 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "ProductName", "Quantity" },
                values: new object[] { 3, 230, "Fridge", 2 });
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
