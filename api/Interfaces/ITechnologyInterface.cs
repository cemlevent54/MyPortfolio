using api.Models;

namespace api.Interfaces
{
    public interface ITechnologyInterface
    {
        Task<IEnumerable<Technologies>> GetTechnologies();
        Task<Technologies> GetTechnology(int id);
        Task<Technologies> AddTechnology(Technologies technology);
        Task<Technologies> UpdateTechnology(Technologies technology);
        Task<Technologies> DeleteTechnology(Technologies technology);

        


    }
}
