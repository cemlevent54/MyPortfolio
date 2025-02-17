﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api.Data;

#nullable disable

namespace api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250129103320_add_tables_db")]
    partial class add_tables_db
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.1");

            modelBuilder.Entity("AppUsersForeignLanguages", b =>
                {
                    b.Property<string>("AppUsersId")
                        .HasColumnType("TEXT");

                    b.Property<int>("ForeignLanguagesLanguage_ID")
                        .HasColumnType("INTEGER");

                    b.HasKey("AppUsersId", "ForeignLanguagesLanguage_ID");

                    b.HasIndex("ForeignLanguagesLanguage_ID");

                    b.ToTable("AppUsersForeignLanguages");
                });

            modelBuilder.Entity("AppUsersHobbies", b =>
                {
                    b.Property<string>("AppUsersId")
                        .HasColumnType("TEXT");

                    b.Property<int>("HobbiesHobby_ID")
                        .HasColumnType("INTEGER");

                    b.HasKey("AppUsersId", "HobbiesHobby_ID");

                    b.HasIndex("HobbiesHobby_ID");

                    b.ToTable("AppUsersHobbies");
                });

            modelBuilder.Entity("AppUsersSkills", b =>
                {
                    b.Property<string>("AppUsersId")
                        .HasColumnType("TEXT");

                    b.Property<int>("SkillsSkill_ID")
                        .HasColumnType("INTEGER");

                    b.HasKey("AppUsersId", "SkillsSkill_ID");

                    b.HasIndex("SkillsSkill_ID");

                    b.ToTable("AppUsersSkills");
                });

            modelBuilder.Entity("AppUsersSocialMedia", b =>
                {
                    b.Property<string>("AppUsersId")
                        .HasColumnType("TEXT");

                    b.Property<int>("SocialMedia_ID")
                        .HasColumnType("INTEGER");

                    b.HasKey("AppUsersId", "SocialMedia_ID");

                    b.HasIndex("SocialMedia_ID");

                    b.ToTable("AppUsersSocialMedia");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "2c0184bb-cd3c-4632-a039-56ab226796db",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "99bb1645-5437-48ff-9c06-c3a6b5548955",
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ProjectsTechnologies", b =>
                {
                    b.Property<int>("ProjectsProject_ID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TechnologiesTechnology_ID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ProjectsProject_ID", "TechnologiesTechnology_ID");

                    b.HasIndex("TechnologiesTechnology_ID");

                    b.ToTable("ProjectsTechnologies");
                });

            modelBuilder.Entity("api.Models.AppUsers", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("User_About")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("User_BirthDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("User_CvUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("User_Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("User_LivingCity")
                        .HasColumnType("TEXT");

                    b.Property<string>("User_Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("User_Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("User_PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("User_PhotoUrl")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("User_RegisteredAt")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("User_State")
                        .HasColumnType("INTEGER");

                    b.Property<string>("User_Surname")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("api.Models.EducationCategory", b =>
                {
                    b.Property<int>("EducationCategory_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("EducationCategory_Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("EducationCategory_ID");

                    b.ToTable("EducationCategories");
                });

            modelBuilder.Entity("api.Models.Educations", b =>
                {
                    b.Property<int>("Education_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EducationCategory_ID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Education_CertificationUrl")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Education_EndDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Education_Organization")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Education_StartDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Education_Subject")
                        .HasColumnType("TEXT");

                    b.Property<string>("Education_Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("User_ID")
                        .HasColumnType("TEXT");

                    b.HasKey("Education_ID");

                    b.HasIndex("EducationCategory_ID");

                    b.HasIndex("User_ID");

                    b.ToTable("Educations");
                });

            modelBuilder.Entity("api.Models.Experiences", b =>
                {
                    b.Property<int>("Experience_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Experience_About")
                        .HasMaxLength(400)
                        .HasColumnType("TEXT");

                    b.Property<string>("Experience_Company")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Experience_EndDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Experience_StartDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Experience_Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("User_ID")
                        .HasColumnType("TEXT");

                    b.HasKey("Experience_Id");

                    b.HasIndex("User_ID");

                    b.ToTable("Experiences");
                });

            modelBuilder.Entity("api.Models.ForeignLanguages", b =>
                {
                    b.Property<int>("Language_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Language_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Language_ID");

                    b.ToTable("ForeignLanguages");
                });

            modelBuilder.Entity("api.Models.Hobbies", b =>
                {
                    b.Property<int>("Hobby_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Hobby_IconUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("Hobby_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Hobby_ID");

                    b.ToTable("Hobbies");
                });

            modelBuilder.Entity("api.Models.Projects", b =>
                {
                    b.Property<int>("Project_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AppUsersId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Project_About")
                        .HasMaxLength(300)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Project_EndDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Project_GithubUrl")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Project_IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Project_LiveUrl")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Project_StartDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Project_Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Project_ID");

                    b.HasIndex("AppUsersId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("api.Models.SkillCategories", b =>
                {
                    b.Property<int>("SkillCategory_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("SkillCategory_IconUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("SkillCategory_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("SkillCategory_ID");

                    b.ToTable("SkillCategories");
                });

            modelBuilder.Entity("api.Models.Skills", b =>
                {
                    b.Property<int>("Skill_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("SkillCategory_ID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Skill_IconUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("Skill_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Skill_ID");

                    b.HasIndex("SkillCategory_ID");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("api.Models.SocialMedia", b =>
                {
                    b.Property<int>("SocialMedia_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("SocialMediaName_ID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SocialMedia_Url")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("SocialMedia_ID");

                    b.HasIndex("SocialMediaName_ID");

                    b.ToTable("SocialMedia");
                });

            modelBuilder.Entity("api.Models.SocialMediaName", b =>
                {
                    b.Property<int>("SocialMediaName_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("SocialMediaName_IconUrl")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("SocialMediaName_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("SocialMediaName_ID");

                    b.ToTable("SocialMediaName");
                });

            modelBuilder.Entity("api.Models.Technologies", b =>
                {
                    b.Property<int>("Technology_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Technology_IconUrl")
                        .HasColumnType("TEXT");

                    b.HasKey("Technology_ID");

                    b.ToTable("Technologies");
                });

            modelBuilder.Entity("AppUsersForeignLanguages", b =>
                {
                    b.HasOne("api.Models.AppUsers", null)
                        .WithMany()
                        .HasForeignKey("AppUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.ForeignLanguages", null)
                        .WithMany()
                        .HasForeignKey("ForeignLanguagesLanguage_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AppUsersHobbies", b =>
                {
                    b.HasOne("api.Models.AppUsers", null)
                        .WithMany()
                        .HasForeignKey("AppUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.Hobbies", null)
                        .WithMany()
                        .HasForeignKey("HobbiesHobby_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AppUsersSkills", b =>
                {
                    b.HasOne("api.Models.AppUsers", null)
                        .WithMany()
                        .HasForeignKey("AppUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.Skills", null)
                        .WithMany()
                        .HasForeignKey("SkillsSkill_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AppUsersSocialMedia", b =>
                {
                    b.HasOne("api.Models.AppUsers", null)
                        .WithMany()
                        .HasForeignKey("AppUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.SocialMedia", null)
                        .WithMany()
                        .HasForeignKey("SocialMedia_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("api.Models.AppUsers", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("api.Models.AppUsers", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.AppUsers", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("api.Models.AppUsers", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectsTechnologies", b =>
                {
                    b.HasOne("api.Models.Projects", null)
                        .WithMany()
                        .HasForeignKey("ProjectsProject_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.Technologies", null)
                        .WithMany()
                        .HasForeignKey("TechnologiesTechnology_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("api.Models.Educations", b =>
                {
                    b.HasOne("api.Models.EducationCategory", "EducationCategory")
                        .WithMany("Educations")
                        .HasForeignKey("EducationCategory_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.AppUsers", "AppUsers")
                        .WithMany("Educations")
                        .HasForeignKey("User_ID");

                    b.Navigation("AppUsers");

                    b.Navigation("EducationCategory");
                });

            modelBuilder.Entity("api.Models.Experiences", b =>
                {
                    b.HasOne("api.Models.AppUsers", "AppUsers")
                        .WithMany("Experiences")
                        .HasForeignKey("User_ID");

                    b.Navigation("AppUsers");
                });

            modelBuilder.Entity("api.Models.Projects", b =>
                {
                    b.HasOne("api.Models.AppUsers", "AppUsers")
                        .WithMany("Projects")
                        .HasForeignKey("AppUsersId");

                    b.Navigation("AppUsers");
                });

            modelBuilder.Entity("api.Models.Skills", b =>
                {
                    b.HasOne("api.Models.SkillCategories", "SkillCategories")
                        .WithMany("Skills")
                        .HasForeignKey("SkillCategory_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SkillCategories");
                });

            modelBuilder.Entity("api.Models.SocialMedia", b =>
                {
                    b.HasOne("api.Models.SocialMediaName", "SocialMediaName")
                        .WithMany("SocialMedia")
                        .HasForeignKey("SocialMediaName_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SocialMediaName");
                });

            modelBuilder.Entity("api.Models.AppUsers", b =>
                {
                    b.Navigation("Educations");

                    b.Navigation("Experiences");

                    b.Navigation("Projects");
                });

            modelBuilder.Entity("api.Models.EducationCategory", b =>
                {
                    b.Navigation("Educations");
                });

            modelBuilder.Entity("api.Models.SkillCategories", b =>
                {
                    b.Navigation("Skills");
                });

            modelBuilder.Entity("api.Models.SocialMediaName", b =>
                {
                    b.Navigation("SocialMedia");
                });
#pragma warning restore 612, 618
        }
    }
}
