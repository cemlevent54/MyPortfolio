using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class deneme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educations_AspNetUsers_User_ID",
                table: "Educations");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e1d856d-e8ab-45b3-8cc1-6aac4cf1a6ec");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bbe9610c-db6f-4200-9618-634a45792568");

            migrationBuilder.AlterColumn<string>(
                name: "User_ID",
                table: "Educations",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4e566c5b-0689-4a5e-ad75-0e1f8d14ee2b", null, "User", "USER" },
                    { "c95fca55-133e-410c-8385-a09922534add", null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_AspNetUsers_User_ID",
                table: "Educations",
                column: "User_ID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educations_AspNetUsers_User_ID",
                table: "Educations");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4e566c5b-0689-4a5e-ad75-0e1f8d14ee2b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c95fca55-133e-410c-8385-a09922534add");

            migrationBuilder.AlterColumn<string>(
                name: "User_ID",
                table: "Educations",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5e1d856d-e8ab-45b3-8cc1-6aac4cf1a6ec", null, "User", "USER" },
                    { "bbe9610c-db6f-4200-9618-634a45792568", null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_AspNetUsers_User_ID",
                table: "Educations",
                column: "User_ID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
