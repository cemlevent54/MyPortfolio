using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class EfProjectCategoryRepository : IProjectCategoryInterface
    {
        private readonly ApplicationDbContext _context;

        public EfProjectCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProjectCategory> AddProjectCategory(ProjectCategory projectCategory)
        {
            _context.ProjectCategories.Add(projectCategory);
            await _context.SaveChangesAsync();
            return projectCategory;
        }

        public async Task<ProjectCategory> DeleteProjectCategory(ProjectCategory projectCategory)
        {
            _context.ProjectCategories.Remove(projectCategory);
            await _context.SaveChangesAsync();
            return projectCategory;

        }

        public async Task<IEnumerable<ProjectCategory>> GetProjectCategories()
        {
            return await _context.ProjectCategories.ToListAsync();
        }

        public async Task<ProjectCategory> GetProjectCategory(int id)
        {
            return await _context.ProjectCategories.FindAsync(id);
        }

        public async Task<IEnumerable<Projects>> GetProjectsofProjectCategory(int id)
        {
            var projects = _context.Projects.Where(p => p.ProjectCategory_ID == id);
            return await projects.ToListAsync();
        }

        public async Task<ProjectCategory> UpdateProjectCategory(ProjectCategory projectCategory)
        {
            _context.ProjectCategories.Update(projectCategory);
            await _context.SaveChangesAsync();
            return projectCategory;
        }
    }
}
