using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class quickchangesforproject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3547b94f-eb7a-4c86-8eb4-12e4209d2421");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1039011-f1fc-45bf-ba08-1fb03750225c");

            migrationBuilder.RenameColumn(
                name: "Project_Photos",
                table: "Projects",
                newName: "Project_Photo");

            migrationBuilder.AlterColumn<string>(
                name: "Project_StartDate",
                table: "Projects",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6221b911-6411-49d7-977f-3e3410eda5df", null, "User", "USER" },
                    { "9ec0011d-71eb-4387-91a9-a1744019b93e", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6221b911-6411-49d7-977f-3e3410eda5df");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ec0011d-71eb-4387-91a9-a1744019b93e");

            migrationBuilder.RenameColumn(
                name: "Project_Photo",
                table: "Projects",
                newName: "Project_Photos");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Project_StartDate",
                table: "Projects",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3547b94f-eb7a-4c86-8eb4-12e4209d2421", null, "User", "USER" },
                    { "a1039011-f1fc-45bf-ba08-1fb03750225c", null, "Admin", "ADMIN" }
                });
        }
    }
}
