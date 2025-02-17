using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    public class AppUsers : IdentityUser
    {
        
        public string? User_Name { get; set; }
        public string? User_Surname { get; set; }
        //public string? User_Username { get; set; }
        public string? User_Email { get; set; }
        public string? User_Password { get; set; }
        public string? User_PhoneNumber { get; set; }
        public string? User_About { get; set; }
        public DateTime User_BirthDate { get; set; }
        public DateTime User_RegisteredAt { get; set; }
        public string? User_PhotoUrl { get; set; }
        public bool? User_State { get; set; }
        public string? User_LivingCity { get; set; }
        public string? User_CvUrl { get; set; }
        public string? User_Job { get; set; }

        //public string? trying_sth { get; set; }

        // relationship between AppUsers and Hobbies will be many to many
        public ICollection<Hobbies> Hobbies { get; set; }
        // relationship between AppUsers and SocialMedia will be many to many
        //public ICollection<SocialMedia> SocialMedia { get; set; } 
        // relationship between AppUsers and ForeignLanguages will be many to many
        public ICollection<ForeignLanguages> ForeignLanguages { get; set; }
        // relationship between AppUsers and Skills will be many to many
        public ICollection<Skills> Skills { get; set; }
        // relationship between AppUsers and Educations will be one to many
        public ICollection<Educations> Educations { get; set; }
        // relationship between AppUsers and Experiences will be one to many
        public ICollection<Experiences> Experiences { get; set; }
        // relationship between AppUsers and Projects will be one to many
        public ICollection<Projects> Projects { get; set; }

    }
}
