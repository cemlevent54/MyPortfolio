using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class Educations
    {
        [Key]
        public int Education_ID { get; set; }
        
        public string Education_Title { get; set; }
        
        public string? Education_Organization { get; set; }
        
        public string? Education_Subject { get; set; }
        public string? Education_StartDate { get; set; }
        public string? Education_EndDate { get; set; }
        public string? Education_CertificationUrl { get; set; }
        // one user has many educations, but one education belongs to one user
        
        [Required]
        public string User_ID { get; set; }
        [ForeignKey("User_ID")]
        public AppUsers AppUsers { get; set; }

        
        [Required]
        public int EducationCategory_ID { get; set; }
        [ForeignKey("EducationCategory_ID")]
        public EducationCategory EducationCategory { get; set; }
    }
}
