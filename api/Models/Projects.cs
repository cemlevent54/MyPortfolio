using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema; // ICollection için

namespace api.Models
{
    public class Projects
    {
        [Key]
        public int Project_ID { get; set; }
        [Required]
        [StringLength(100)]
        public string? Project_Title { get; set; }
        
        [StringLength(300)]
        public string? Project_About { get; set; }
        public string? Project_GithubUrl { get; set; }
        
        public string? Project_LiveUrl { get; set; }
        public string? Project_StartDate { get; set; }
        public string? Project_EndDate { get; set; }
        public bool Project_IsActive { get; set; }

        public ICollection<Technologies> Technologies { get; set; }

        [Required]
        public string User_ID { get; set; }
        [ForeignKey("User_ID")]
        public AppUsers AppUsers { get; set; }

        public int ProjectCategory_ID { get; set; }
        [ForeignKey("ProjectCategory_ID")]
        public ProjectCategory ProjectCategory { get; set; }

        public string? Project_Photo { get; set; }




    }
}
