using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class ForeignLanguages
    {
        [Key]
        public int Language_ID { get; set; }
        [Required]
        [StringLength(50)]
        public string Language_Name { get; set; }

        // many to many with AppUsers
        public ICollection<AppUsers> AppUsers { get; set; }
    }
}
