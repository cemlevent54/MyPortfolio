using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class addprojectcategoryclass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "785a84e3-1df3-471c-ba27-a6daf9c0881e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ecbd20ea-d340-4d3e-869e-a0197fec3b31");

            migrationBuilder.AddColumn<int>(
                name: "ProjectCategory_ID",
                table: "Projects",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProjectCategories",
                columns: table => new
                {
                    ProjectCategory_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectCategory_Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectCategories", x => x.ProjectCategory_ID);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0901974f-e5aa-4583-bb33-92df5ff66189", null, "Admin", "ADMIN" },
                    { "18a37dd2-da52-48d9-9c1c-b92efb03b04e", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectCategory_ID",
                table: "Projects",
                column: "ProjectCategory_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ProjectCategories_ProjectCategory_ID",
                table: "Projects",
                column: "ProjectCategory_ID",
                principalTable: "ProjectCategories",
                principalColumn: "ProjectCategory_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ProjectCategories_ProjectCategory_ID",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "ProjectCategories");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectCategory_ID",
                table: "Projects");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0901974f-e5aa-4583-bb33-92df5ff66189");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "18a37dd2-da52-48d9-9c1c-b92efb03b04e");

            migrationBuilder.DropColumn(
                name: "ProjectCategory_ID",
                table: "Projects");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "785a84e3-1df3-471c-ba27-a6daf9c0881e", null, "User", "USER" },
                    { "ecbd20ea-d340-4d3e-869e-a0197fec3b31", null, "Admin", "ADMIN" }
                });
        }
    }
}
