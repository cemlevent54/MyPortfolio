using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class EfTalentsRepository : ITalentsInterface
    {
        private readonly ApplicationDbContext _context;
        public EfTalentsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Skills> AddSkill(Skills skill)
        {
            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();
            return skill;
        }

        public async Task<Skills> AddSkillToUser(int skill_id, string user_id)
        {
            var skill = await _context.Skills.Include(x => x.AppUsers).FirstOrDefaultAsync(x => x.Skill_ID == skill_id);
            var user = await _context.AppUsers.FindAsync(user_id);
            if (skill != null && user != null)
            {
                skill.AppUsers.Add(user);
                await _context.SaveChangesAsync();
            }
            return skill;
            
        }

        public async Task<Skills> DeleteSkill(Skills skill)
        {
            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();
            return skill;
        }

        public async Task<Skills> GetSkillById(int id)
        {
            return await _context.Skills
                                 .Include(s => s.SkillCategories)
                                 .FirstOrDefaultAsync(s => s.Skill_ID == id);
        }

        public async Task<IEnumerable<Skills>> GetSkills()
        {
            return await _context.Skills
                                 .Include(s => s.SkillCategories)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Skills>> GetSkillsOfUser(string user_id)
        {
            var user = await _context.AppUsers
                                     .Include(u => u.Skills)
                                     .ThenInclude(s => s.SkillCategories) // Include SkillCategories
                                     .FirstOrDefaultAsync(u => u.Id == user_id);
            if (user == null)
            {
                return Enumerable.Empty<Skills>();
            }
            return user.Skills;
        }

        public async Task<Skills> RemoveSkillFromUser(int skill_id, string user_id)
        {
            var skill = await _context.Skills.Include(x => x.AppUsers).FirstOrDefaultAsync(x => x.Skill_ID == skill_id);
            var user = await _context.AppUsers.FindAsync(user_id);
            if (skill != null && user != null)
            {
                skill.AppUsers.Remove(user);
                await _context.SaveChangesAsync();
            }
            return skill;
        }

        public async Task<Skills> UpdateSkill(Skills skill)
        {
            _context.Skills.Update(skill);
            await _context.SaveChangesAsync();
            return skill;
        }
    }
}
