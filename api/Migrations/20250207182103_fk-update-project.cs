using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class fkupdateproject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_AppUsersId",
                table: "Projects");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6221b911-6411-49d7-977f-3e3410eda5df");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ec0011d-71eb-4387-91a9-a1744019b93e");

            migrationBuilder.RenameColumn(
                name: "AppUsersId",
                table: "Projects",
                newName: "User_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_AppUsersId",
                table: "Projects",
                newName: "IX_Projects_User_ID");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6f1ba0e9-6f6a-4ff5-973e-cfdcc3a5153a", null, "Admin", "ADMIN" },
                    { "9a92c450-4c8e-40a1-97ae-611f95656980", null, "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_User_ID",
                table: "Projects",
                column: "User_ID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_User_ID",
                table: "Projects");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f1ba0e9-6f6a-4ff5-973e-cfdcc3a5153a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9a92c450-4c8e-40a1-97ae-611f95656980");

            migrationBuilder.RenameColumn(
                name: "User_ID",
                table: "Projects",
                newName: "AppUsersId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_User_ID",
                table: "Projects",
                newName: "IX_Projects_AppUsersId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6221b911-6411-49d7-977f-3e3410eda5df", null, "User", "USER" },
                    { "9ec0011d-71eb-4387-91a9-a1744019b93e", null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_AppUsersId",
                table: "Projects",
                column: "AppUsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
