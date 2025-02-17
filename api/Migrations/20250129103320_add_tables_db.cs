using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class add_tables_db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    User_Name = table.Column<string>(type: "TEXT", nullable: true),
                    User_Surname = table.Column<string>(type: "TEXT", nullable: true),
                    User_Email = table.Column<string>(type: "TEXT", nullable: true),
                    User_Password = table.Column<string>(type: "TEXT", nullable: true),
                    User_PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    User_About = table.Column<string>(type: "TEXT", nullable: true),
                    User_BirthDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    User_RegisteredAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    User_PhotoUrl = table.Column<string>(type: "TEXT", nullable: true),
                    User_State = table.Column<bool>(type: "INTEGER", nullable: true),
                    User_LivingCity = table.Column<string>(type: "TEXT", nullable: true),
                    User_CvUrl = table.Column<string>(type: "TEXT", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationCategories",
                columns: table => new
                {
                    EducationCategory_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EducationCategory_Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationCategories", x => x.EducationCategory_ID);
                });

            migrationBuilder.CreateTable(
                name: "ForeignLanguages",
                columns: table => new
                {
                    Language_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Language_Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForeignLanguages", x => x.Language_ID);
                });

            migrationBuilder.CreateTable(
                name: "Hobbies",
                columns: table => new
                {
                    Hobby_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Hobby_Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Hobby_IconUrl = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hobbies", x => x.Hobby_ID);
                });

            migrationBuilder.CreateTable(
                name: "SkillCategories",
                columns: table => new
                {
                    SkillCategory_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SkillCategory_Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    SkillCategory_IconUrl = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillCategories", x => x.SkillCategory_ID);
                });

            migrationBuilder.CreateTable(
                name: "SocialMediaName",
                columns: table => new
                {
                    SocialMediaName_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SocialMediaName_Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    SocialMediaName_IconUrl = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMediaName", x => x.SocialMediaName_ID);
                });

            migrationBuilder.CreateTable(
                name: "Technologies",
                columns: table => new
                {
                    Technology_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Technology_IconUrl = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technologies", x => x.Technology_ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    Experience_Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Experience_Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Experience_Company = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Experience_About = table.Column<string>(type: "TEXT", maxLength: 400, nullable: true),
                    Experience_StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Experience_EndDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    User_ID = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.Experience_Id);
                    table.ForeignKey(
                        name: "FK_Experiences_AspNetUsers_User_ID",
                        column: x => x.User_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Project_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Project_Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Project_About = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true),
                    Project_GithubUrl = table.Column<string>(type: "TEXT", nullable: true),
                    Project_LiveUrl = table.Column<string>(type: "TEXT", nullable: true),
                    Project_StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Project_EndDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Project_IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    AppUsersId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Project_ID);
                    table.ForeignKey(
                        name: "FK_Projects_AspNetUsers_AppUsersId",
                        column: x => x.AppUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    Education_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Education_Title = table.Column<string>(type: "TEXT", nullable: false),
                    Education_Organization = table.Column<string>(type: "TEXT", nullable: true),
                    Education_Subject = table.Column<string>(type: "TEXT", nullable: true),
                    Education_StartDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Education_EndDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Education_CertificationUrl = table.Column<string>(type: "TEXT", nullable: true),
                    User_ID = table.Column<string>(type: "TEXT", nullable: true),
                    EducationCategory_ID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.Education_ID);
                    table.ForeignKey(
                        name: "FK_Educations_AspNetUsers_User_ID",
                        column: x => x.User_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Educations_EducationCategories_EducationCategory_ID",
                        column: x => x.EducationCategory_ID,
                        principalTable: "EducationCategories",
                        principalColumn: "EducationCategory_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUsersForeignLanguages",
                columns: table => new
                {
                    AppUsersId = table.Column<string>(type: "TEXT", nullable: false),
                    ForeignLanguagesLanguage_ID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsersForeignLanguages", x => new { x.AppUsersId, x.ForeignLanguagesLanguage_ID });
                    table.ForeignKey(
                        name: "FK_AppUsersForeignLanguages_AspNetUsers_AppUsersId",
                        column: x => x.AppUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUsersForeignLanguages_ForeignLanguages_ForeignLanguagesLanguage_ID",
                        column: x => x.ForeignLanguagesLanguage_ID,
                        principalTable: "ForeignLanguages",
                        principalColumn: "Language_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUsersHobbies",
                columns: table => new
                {
                    AppUsersId = table.Column<string>(type: "TEXT", nullable: false),
                    HobbiesHobby_ID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsersHobbies", x => new { x.AppUsersId, x.HobbiesHobby_ID });
                    table.ForeignKey(
                        name: "FK_AppUsersHobbies_AspNetUsers_AppUsersId",
                        column: x => x.AppUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUsersHobbies_Hobbies_HobbiesHobby_ID",
                        column: x => x.HobbiesHobby_ID,
                        principalTable: "Hobbies",
                        principalColumn: "Hobby_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Skill_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Skill_Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Skill_IconUrl = table.Column<string>(type: "TEXT", nullable: true),
                    SkillCategory_ID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Skill_ID);
                    table.ForeignKey(
                        name: "FK_Skills_SkillCategories_SkillCategory_ID",
                        column: x => x.SkillCategory_ID,
                        principalTable: "SkillCategories",
                        principalColumn: "SkillCategory_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SocialMedia",
                columns: table => new
                {
                    SocialMedia_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SocialMedia_Url = table.Column<string>(type: "TEXT", nullable: false),
                    SocialMediaName_ID = table.Column<int>(type: "INTEGER", nullable: false)
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
                name: "ProjectsTechnologies",
                columns: table => new
                {
                    ProjectsProject_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    TechnologiesTechnology_ID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectsTechnologies", x => new { x.ProjectsProject_ID, x.TechnologiesTechnology_ID });
                    table.ForeignKey(
                        name: "FK_ProjectsTechnologies_Projects_ProjectsProject_ID",
                        column: x => x.ProjectsProject_ID,
                        principalTable: "Projects",
                        principalColumn: "Project_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectsTechnologies_Technologies_TechnologiesTechnology_ID",
                        column: x => x.TechnologiesTechnology_ID,
                        principalTable: "Technologies",
                        principalColumn: "Technology_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUsersSkills",
                columns: table => new
                {
                    AppUsersId = table.Column<string>(type: "TEXT", nullable: false),
                    SkillsSkill_ID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsersSkills", x => new { x.AppUsersId, x.SkillsSkill_ID });
                    table.ForeignKey(
                        name: "FK_AppUsersSkills_AspNetUsers_AppUsersId",
                        column: x => x.AppUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUsersSkills_Skills_SkillsSkill_ID",
                        column: x => x.SkillsSkill_ID,
                        principalTable: "Skills",
                        principalColumn: "Skill_ID",
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
                    { "2c0184bb-cd3c-4632-a039-56ab226796db", null, "Admin", "ADMIN" },
                    { "99bb1645-5437-48ff-9c06-c3a6b5548955", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUsersForeignLanguages_ForeignLanguagesLanguage_ID",
                table: "AppUsersForeignLanguages",
                column: "ForeignLanguagesLanguage_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsersHobbies_HobbiesHobby_ID",
                table: "AppUsersHobbies",
                column: "HobbiesHobby_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsersSkills_SkillsSkill_ID",
                table: "AppUsersSkills",
                column: "SkillsSkill_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsersSocialMedia_SocialMedia_ID",
                table: "AppUsersSocialMedia",
                column: "SocialMedia_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Educations_EducationCategory_ID",
                table: "Educations",
                column: "EducationCategory_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Educations_User_ID",
                table: "Educations",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_User_ID",
                table: "Experiences",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_AppUsersId",
                table: "Projects",
                column: "AppUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectsTechnologies_TechnologiesTechnology_ID",
                table: "ProjectsTechnologies",
                column: "TechnologiesTechnology_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_SkillCategory_ID",
                table: "Skills",
                column: "SkillCategory_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SocialMedia_SocialMediaName_ID",
                table: "SocialMedia",
                column: "SocialMediaName_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUsersForeignLanguages");

            migrationBuilder.DropTable(
                name: "AppUsersHobbies");

            migrationBuilder.DropTable(
                name: "AppUsersSkills");

            migrationBuilder.DropTable(
                name: "AppUsersSocialMedia");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropTable(
                name: "Experiences");

            migrationBuilder.DropTable(
                name: "ProjectsTechnologies");

            migrationBuilder.DropTable(
                name: "ForeignLanguages");

            migrationBuilder.DropTable(
                name: "Hobbies");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "SocialMedia");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "EducationCategories");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Technologies");

            migrationBuilder.DropTable(
                name: "SkillCategories");

            migrationBuilder.DropTable(
                name: "SocialMediaName");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
