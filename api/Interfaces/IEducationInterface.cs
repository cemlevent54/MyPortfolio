using api.Models;

namespace api.Interfaces
{
    public interface IEducationInterface
    {
        Task<Educations> CreateEducation(Educations education);
        Task<IEnumerable<Educations>> GetEducations();
        Task<Educations> UpdateEducation(Educations education);
        Task<Educations> DeleteEducation(Educations education);

        Task<Educations> GetEducationById(int id);
    }
}
