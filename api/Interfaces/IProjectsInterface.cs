using api.Models;

namespace api.Interfaces
{
    public interface IProjectsInterface
    {
        Task<IEnumerable<Projects>> GetProjects();
        Task<Projects> GetProjectById(int id);
        Task<Projects> AddProject(Projects project);
        Task<Projects> UpdateProject(Projects project);
        Task<Projects> DeleteProject(Projects project);

        Task<IEnumerable<Projects>> GetProjectsofUser(string user_id);

        Task<Projects> AddTechnologiesToProject(int project_id, int technology_id);

        Task<Projects> RemoveTechnologiesFromProject(int project_id, int technology_id);

        Task<IEnumerable<Technologies>> GetTechnologiesUsedinProject(int project_id);

    }
}
