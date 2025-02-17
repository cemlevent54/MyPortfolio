using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class EfProjectsRepository : IProjectsInterface
    {
        private readonly ApplicationDbContext _context;
        public EfProjectsRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<Projects> AddProject(Projects project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<Projects> DeleteProject(Projects project)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<Projects> GetProjectById(int id)
        {
            return await _context.Projects
               .Include(p => p.Technologies)  // Teknolojileri dahil et
               .FirstOrDefaultAsync(p => p.Project_ID == id);
        }

        public async Task<IEnumerable<Projects>> GetProjects()
        {
            return await _context.Projects
                .Include(p => p.Technologies)  // Teknolojileri dahil et
                .ToListAsync();

        }

        public async Task<Projects> UpdateProject(Projects project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<IEnumerable<Projects>> GetProjectsofUser(string user_id)
        {
            throw new NotImplementedException();
        }

        public async Task<Projects> AddTechnologiesToProject(int project_id, int technology_id)
        {
            var project = await _context.Projects
                .Include(p => p.Technologies)
                .FirstOrDefaultAsync(p => p.Project_ID == project_id);  // Filtreleme eklendi
            var technology = await _context.Technologies.FindAsync(technology_id);
            if (project != null && technology != null)
            {
                project.Technologies.Add(technology);
                await _context.SaveChangesAsync();
            }
            return project;
        }

        public async Task<Projects> RemoveTechnologiesFromProject(int project_id, int technology_id)
        {
            var project = await _context.Projects
            .Include(p => p.Technologies)
            .FirstOrDefaultAsync(p => p.Project_ID == project_id);  // Filtreleme eklendi
            var technology = await _context.Technologies.FindAsync(technology_id);
            if (project != null && technology != null)
            {
                project.Technologies.Remove(technology);
                await _context.SaveChangesAsync();
            }
            return project;
        }

        public async Task<IEnumerable<Technologies>> GetTechnologiesUsedinProject(int project_id)
        {
            var project = await _context.Projects
                .Include(p => p.Technologies)
                .FirstOrDefaultAsync(p => p.Project_ID == project_id);

            if (project != null)
            {
                return project.Technologies.AsEnumerable();
            }
            else
            {
                return Enumerable.Empty<Technologies>();
            }
        }
    }
}
