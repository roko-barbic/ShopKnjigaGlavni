using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopKnjigaGlavni.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "City", "Name", "PhoneNumber", "PostalCode", "State", "StreetAddress" },
                values: new object[] { 1, "Osijek", "IVS", "0191412412", "31000", "HRV", "Vukovarska" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
