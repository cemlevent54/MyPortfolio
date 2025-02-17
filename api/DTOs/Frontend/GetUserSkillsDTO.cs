using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Frontend
{
    public class GetUserSkillsDTO
    {
        public int Skill_ID { get; set; }
        public string? Skill_Name { get; set; }
        public string? Skill_IconUrl { get; set; }
        public int SkillCategory_ID { get; set; }
    }
}
