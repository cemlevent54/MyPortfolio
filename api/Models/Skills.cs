using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class Skills
    {
        [Key]
        public int Skill_ID { get; set; }
        [Required]
        [StringLength(50)]
        public string Skill_Name { get; set; }
        public string? Skill_IconUrl { get; set; }

        // relation type will be one-to-many
        // which means one SkillCategory can have many Skills, but one Skill can only have one SkillCategory
        public int SkillCategory_ID { get; set; }
        [ForeignKey("SkillCategory_ID")]
        public SkillCategories SkillCategories { get; set; }

        // many to many with AppUsers
        public ICollection<AppUsers> AppUsers { get; set; }

    }
}
