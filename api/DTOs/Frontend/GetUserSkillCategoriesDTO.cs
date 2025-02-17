using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Frontend
{
    public class GetUserSkillCategoriesDTO
    {
        public int SkillCategory_ID { get; set; }
        public string? SkillCategory_Name { get; set; }
        public string? SkillCategory_IconUrl { get; set; }
    }
}
