using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class NoUsername : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0c0d48c2-e425-447b-8419-89295d3f8fe7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8badb5a1-0792-4736-bac5-ea0472adb7dc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1576e644-1e0c-4f0e-bfd7-068e10ab9b00", null, "Admin", "ADMIN" },
                    { "8eb6351d-a2eb-469c-9b61-d01c76d09703", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1576e644-1e0c-4f0e-bfd7-068e10ab9b00");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8eb6351d-a2eb-469c-9b61-d01c76d09703");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0c0d48c2-e425-447b-8419-89295d3f8fe7", null, "User", "USER" },
                    { "8badb5a1-0792-4736-bac5-ea0472adb7dc", null, "Admin", "ADMIN" }
                });
        }
    }
}
