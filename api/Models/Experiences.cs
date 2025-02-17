using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class Experiences
    {
        [Key]
        public int Experience_Id { get; set; }
        
        public string? Experience_Title { get; set; }
        
        public string? Experience_Company { get; set; }
        
        
        public string? Experience_About { get; set; }

        
        public string? Experience_StartDate { get; set; }
        
        public string? Experience_EndDate { get; set; }
        // relationship between Experiences and AppUsers will be one to many
        public string User_ID { get; set; }
        [ForeignKey("User_ID")]
        public AppUsers AppUsers { get; set; }

    }
}
