using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class Refined : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "62564457-1b9d-47f8-b95a-478f2fbbd402");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7404f951-4c09-4a1d-b001-71deab2a9cf7");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "LastDiv",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "MarketCap",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "Purchase",
                table: "Stocks");

            migrationBuilder.RenameColumn(
                name: "Symbol",
                table: "Stocks",
                newName: "OwnedBy");

            migrationBuilder.RenameColumn(
                name: "Industry",
                table: "Stocks",
                newName: "Data");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "63f5b396-fd4d-4dbc-a803-01319fbc88ac", null, "User", "USER" },
                    { "70f29eb8-90db-439a-9d6e-4156d4411466", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "63f5b396-fd4d-4dbc-a803-01319fbc88ac");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "70f29eb8-90db-439a-9d6e-4156d4411466");

            migrationBuilder.RenameColumn(
                name: "OwnedBy",
                table: "Stocks",
                newName: "Symbol");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "Stocks",
                newName: "Industry");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Stocks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "LastDiv",
                table: "Stocks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "MarketCap",
                table: "Stocks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<decimal>(
                name: "Purchase",
                table: "Stocks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "62564457-1b9d-47f8-b95a-478f2fbbd402", null, "User", "USER" },
                    { "7404f951-4c09-4a1d-b001-71deab2a9cf7", null, "Admin", "ADMIN" }
                });
        }
    }
}
