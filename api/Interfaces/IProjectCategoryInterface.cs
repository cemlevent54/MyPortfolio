using api.Models;

namespace api.Interfaces
{
    public interface IProjectCategoryInterface
    {
        Task<IEnumerable<ProjectCategory>> GetProjectCategories();
        Task<ProjectCategory> GetProjectCategory(int id);
        Task<ProjectCategory> AddProjectCategory(ProjectCategory projectCategory);
        Task<ProjectCategory> UpdateProjectCategory(ProjectCategory projectCategory);
        Task<ProjectCategory> DeleteProjectCategory(ProjectCategory projectCategory);

        Task<IEnumerable<Projects>> GetProjectsofProjectCategory(int id);


    }
}
