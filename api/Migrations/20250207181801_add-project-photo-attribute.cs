using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class addprojectphotoattribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0901974f-e5aa-4583-bb33-92df5ff66189");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "18a37dd2-da52-48d9-9c1c-b92efb03b04e");

            migrationBuilder.AddColumn<string>(
                name: "Project_Photos",
                table: "Projects",
                type: "TEXT",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3547b94f-eb7a-4c86-8eb4-12e4209d2421", null, "User", "USER" },
                    { "a1039011-f1fc-45bf-ba08-1fb03750225c", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3547b94f-eb7a-4c86-8eb4-12e4209d2421");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1039011-f1fc-45bf-ba08-1fb03750225c");

            migrationBuilder.DropColumn(
                name: "Project_Photos",
                table: "Projects");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0901974f-e5aa-4583-bb33-92df5ff66189", null, "Admin", "ADMIN" },
                    { "18a37dd2-da52-48d9-9c1c-b92efb03b04e", null, "User", "USER" }
                });
        }
    }
}
