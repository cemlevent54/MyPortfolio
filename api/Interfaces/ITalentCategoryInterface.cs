using api.Models;

namespace api.Interfaces
{
    public interface ITalentCategoryInterface
    {
        Task<IEnumerable<SkillCategories>> GetSkillCategories();
        Task<SkillCategories> GetSkillCategory(int id);
        Task<SkillCategories> AddSkillCategory(SkillCategories skillCategory);
        Task<SkillCategories> UpdateSkillCategory(SkillCategories skillCategory);
        Task<SkillCategories> DeleteSkillCategory(SkillCategories skillCategory);
    }
}
