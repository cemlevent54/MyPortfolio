using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class addtechnologynameattribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01a96187-d60d-4c03-b108-e82641f46910");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35bb3458-9bc8-4385-9e9c-9086b8d880d2");

            migrationBuilder.AddColumn<string>(
                name: "Technology_Name",
                table: "Technologies",
                type: "TEXT",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "785a84e3-1df3-471c-ba27-a6daf9c0881e", null, "User", "USER" },
                    { "ecbd20ea-d340-4d3e-869e-a0197fec3b31", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "785a84e3-1df3-471c-ba27-a6daf9c0881e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ecbd20ea-d340-4d3e-869e-a0197fec3b31");

            migrationBuilder.DropColumn(
                name: "Technology_Name",
                table: "Technologies");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "01a96187-d60d-4c03-b108-e82641f46910", null, "Admin", "ADMIN" },
                    { "35bb3458-9bc8-4385-9e9c-9086b8d880d2", null, "User", "USER" }
                });
        }
    }
}
