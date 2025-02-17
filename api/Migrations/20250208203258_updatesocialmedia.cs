using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class updatesocialmedia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "bd44326a-cd64-41d0-a9bc-3827fa1f628a", null, "Admin", "ADMIN" },
                    { "f84981ef-cd01-41e1-b1e3-0b70f9692d51", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd44326a-cd64-41d0-a9bc-3827fa1f628a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f84981ef-cd01-41e1-b1e3-0b70f9692d51");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "642f1229-51dd-4e79-a9b7-7d34fe32466d", null, "User", "USER" },
                    { "ffc4fb67-6d71-43e5-8b51-37320e82930d", null, "Admin", "ADMIN" }
                });
        }
    }
}
