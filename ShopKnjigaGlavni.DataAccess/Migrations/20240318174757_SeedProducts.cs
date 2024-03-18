using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShopKnjigaGlavni.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Author", "Description", "ISBN", "ListPrice", "Price", "Price100", "Price50", "Title" },
                values: new object[,]
                {
                    { 1, "Nez ko je", "Djecak koji zivi u snu", "aSDNJfjabaA", 15.0, 13.0, 7.0, 10.0, "Petar Pan" },
                    { 2, "Rita Bullwinkel", "Zivot je borba", "agDNJfjfassaA", 18.0, 13.0, 10.0, 13.0, "Headshot" },
                    { 3, "Tracy Sierra", "Gledanje mraka", "giasdbfajfaS", 20.0, 13.0, 7.0, 10.0, "Nightwatching" }
                });
        }

        /// <inheritdoc />
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
