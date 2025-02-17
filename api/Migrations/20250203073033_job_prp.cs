using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class job_prp : Migration
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
                keyValue: "2c0184bb-cd3c-4632-a039-56ab226796db");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "99bb1645-5437-48ff-9c06-c3a6b5548955");

            migrationBuilder.AlterColumn<string>(
                name: "AppUsersId",
                table: "Projects",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "User_Job",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b1f98ef2-5e15-4e4b-bc33-cef1bf34e4fb", null, "User", "USER" },
                    { "db499f52-3d2c-47bb-bb79-4d69e252a831", null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_AppUsersId",
                table: "Projects",
                column: "AppUsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_AppUsersId",
                table: "Projects");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b1f98ef2-5e15-4e4b-bc33-cef1bf34e4fb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "db499f52-3d2c-47bb-bb79-4d69e252a831");

            migrationBuilder.DropColumn(
                name: "User_Job",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "AppUsersId",
                table: "Projects",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2c0184bb-cd3c-4632-a039-56ab226796db", null, "Admin", "ADMIN" },
                    { "99bb1645-5437-48ff-9c06-c3a6b5548955", null, "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_AppUsersId",
                table: "Projects",
                column: "AppUsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
