using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class EfTalentCategoriesRepository : ITalentCategoryInterface
    {
        private readonly ApplicationDbContext _context;
        public EfTalentCategoriesRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<SkillCategories> AddSkillCategory(SkillCategories skillCategory)
        {
            _context.SkillCategories.Add(skillCategory);
            await _context.SaveChangesAsync();
            return skillCategory;
        }

        public async Task<SkillCategories> DeleteSkillCategory(SkillCategories skillCategory)
        {
            _context.SkillCategories.Remove(skillCategory);
            await _context.SaveChangesAsync();
            return skillCategory;
        }

        public async Task<IEnumerable<SkillCategories>> GetSkillCategories()
        {
            var skillCategories = await _context.SkillCategories.ToListAsync();
            return skillCategories;
        }

        public async Task<SkillCategories> GetSkillCategory(int id)
        {
            return await _context.SkillCategories.FindAsync(id);
        }

        public async Task<SkillCategories> UpdateSkillCategory(SkillCategories skillCategory)
        {
            _context.SkillCategories.Update(skillCategory);
            await _context.SaveChangesAsync();
            return skillCategory;
        }
    }
}
