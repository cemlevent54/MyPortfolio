// for identity dbcontext
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace api.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUsers>
    {
        // DbContext'i configure etmek için constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }





        // DbSet tanýmlamalarý
        //public DbSet<MyFirstSampleClass> MyFirstSampleClasses { get; set; }
        public DbSet<Hobbies> Hobbies { get; set; }
        public DbSet<ForeignLanguages> ForeignLanguages { get; set; }
        //public DbSet<SocialMedia> SocialMedia { get; set; }
        //public DbSet<SocialMediaName> SocialMediaName { get; set; }
        public DbSet<SkillCategories> SkillCategories { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<Educations> Educations { get; set; }
        public DbSet<Experiences> Experiences { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<ProjectCategory> ProjectCategories { get; set; }
        public DbSet<Technologies> Technologies { get; set; }
        public DbSet<EducationCategory> EducationCategories { get; set; }

        public DbSet<AppUsers> AppUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Define roles as static, predefined values
            var roles = new List<IdentityRole>()
            {
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Name = "User", NormalizedName = "USER" }
            };

            // Ensure roles are seeded only if they don't already exist in the database
            builder.Entity<IdentityRole>().HasData(roles.ToArray());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

    }
}
