using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class addskillcategoryidtoskills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f1ba0e9-6f6a-4ff5-973e-cfdcc3a5153a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9a92c450-4c8e-40a1-97ae-611f95656980");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "642f1229-51dd-4e79-a9b7-7d34fe32466d", null, "User", "USER" },
                    { "ffc4fb67-6d71-43e5-8b51-37320e82930d", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "642f1229-51dd-4e79-a9b7-7d34fe32466d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ffc4fb67-6d71-43e5-8b51-37320e82930d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6f1ba0e9-6f6a-4ff5-973e-cfdcc3a5153a", null, "Admin", "ADMIN" },
                    { "9a92c450-4c8e-40a1-97ae-611f95656980", null, "User", "USER" }
                });
        }
    }
}
