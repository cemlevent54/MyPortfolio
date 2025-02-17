using api.Models;

namespace api.Interfaces
{
    public interface ITalentsInterface
    {
        Task<IEnumerable<Skills>> GetSkills();
        Task<Skills> GetSkillById(int id);

        Task<Skills> AddSkill(Skills skill);
        Task<Skills> UpdateSkill(Skills skill);
        Task<Skills> DeleteSkill(Skills skill);

        Task<IEnumerable<Skills>> GetSkillsOfUser(string user_id);
        Task<Skills> AddSkillToUser(int skill_id, string user_id);
        Task<Skills> RemoveSkillFromUser(int skill_id, string user_id);

        

        
    }
}
