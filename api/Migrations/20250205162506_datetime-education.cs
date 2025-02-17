using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class datetimeeducation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b1f98ef2-5e15-4e4b-bc33-cef1bf34e4fb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "db499f52-3d2c-47bb-bb79-4d69e252a831");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5e1d856d-e8ab-45b3-8cc1-6aac4cf1a6ec", null, "User", "USER" },
                    { "bbe9610c-db6f-4200-9618-634a45792568", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e1d856d-e8ab-45b3-8cc1-6aac4cf1a6ec");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bbe9610c-db6f-4200-9618-634a45792568");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b1f98ef2-5e15-4e4b-bc33-cef1bf34e4fb", null, "User", "USER" },
                    { "db499f52-3d2c-47bb-bb79-4d69e252a831", null, "Admin", "ADMIN" }
                });
        }
    }
}
