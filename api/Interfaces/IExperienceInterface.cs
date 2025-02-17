using api.Models;

namespace api.Interfaces
{
    public interface IExperienceInterface
    {
        Task<IEnumerable<Experiences>> GetExperiences();
        Task<Experiences> GetExperienceById(int id);
        Task<Experiences> CreateExperience(Experiences experience);
        Task<Experiences> UpdateExperience(Experiences experience);
        Task<Experiences> DeleteExperience(Experiences experience);

    }
}
