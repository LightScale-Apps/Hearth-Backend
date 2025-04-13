using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class PropertyValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1576e644-1e0c-4f0e-bfd7-068e10ab9b00");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8eb6351d-a2eb-469c-9b61-d01c76d09703");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "PatientData",
                newName: "Value");

            migrationBuilder.AddColumn<string>(
                name: "Property",
                table: "PatientData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0e38d9c9-842b-4736-87c5-c3f7e11e063f", null, "Admin", "ADMIN" },
                    { "f9d09726-ac16-4c72-b8f9-a7a5d1521661", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e38d9c9-842b-4736-87c5-c3f7e11e063f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f9d09726-ac16-4c72-b8f9-a7a5d1521661");

            migrationBuilder.DropColumn(
                name: "Property",
                table: "PatientData");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "PatientData",
                newName: "Data");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1576e644-1e0c-4f0e-bfd7-068e10ab9b00", null, "Admin", "ADMIN" },
                    { "8eb6351d-a2eb-469c-9b61-d01c76d09703", null, "User", "USER" }
                });
        }
    }
}
