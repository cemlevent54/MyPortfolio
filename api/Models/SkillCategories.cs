using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class SkillCategories
    {
        [Key]
        public int SkillCategory_ID { get; set; }
        [Required]
        [StringLength(50)]
        public string SkillCategory_Name { get; set; }
        public string? SkillCategory_IconUrl { get; set; }
        
        // relation type will be one-to-many
        // which means one SkillCategory can have many Skills, but one Skill can only have one SkillCategory

        public ICollection<Skills> Skills { get; set; }
    }
}
