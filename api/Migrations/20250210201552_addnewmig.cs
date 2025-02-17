using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class addnewmig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUsersSocialMedia");

            migrationBuilder.DropTable(
                name: "SocialMedia");

            migrationBuilder.DropTable(
                name: "SocialMediaName");

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
                    { "9d12e731-9d2d-4409-b618-e2fd4bbdf1ab", null, "User", "USER" },
                    { "b088ad6a-2090-4357-938b-7aa7a5bcd6f3", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9d12e731-9d2d-4409-b618-e2fd4bbdf1ab");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b088ad6a-2090-4357-938b-7aa7a5bcd6f3");

            migrationBuilder.CreateTable(
                name: "SocialMediaName",
                columns: table => new
                {
                    SocialMediaName_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SocialMediaName_IconUrl = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    SocialMediaName_Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMediaName", x => x.SocialMediaName_ID);
                });

            migrationBuilder.CreateTable(
                name: "SocialMedia",
                columns: table => new
                {
                    SocialMedia_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SocialMediaName_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    SocialMedia_Url = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMedia", x => x.SocialMedia_ID);
                    table.ForeignKey(
                        name: "FK_SocialMedia_SocialMediaName_SocialMediaName_ID",
                        column: x => x.SocialMediaName_ID,
                        principalTable: "SocialMediaName",
                        principalColumn: "SocialMediaName_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUsersSocialMedia",
                columns: table => new
                {
                    AppUsersId = table.Column<string>(type: "TEXT", nullable: false),
                    SocialMedia_ID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsersSocialMedia", x => new { x.AppUsersId, x.SocialMedia_ID });
                    table.ForeignKey(
                        name: "FK_AppUsersSocialMedia_AspNetUsers_AppUsersId",
                        column: x => x.AppUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUsersSocialMedia_SocialMedia_SocialMedia_ID",
                        column: x => x.SocialMedia_ID,
                        principalTable: "SocialMedia",
                        principalColumn: "SocialMedia_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "bd44326a-cd64-41d0-a9bc-3827fa1f628a", null, "Admin", "ADMIN" },
                    { "f84981ef-cd01-41e1-b1e3-0b70f9692d51", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUsersSocialMedia_SocialMedia_ID",
                table: "AppUsersSocialMedia",
                column: "SocialMedia_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SocialMedia_SocialMediaName_ID",
                table: "SocialMedia",
                column: "SocialMediaName_ID");
        }
    }
}
