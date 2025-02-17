using api.DTOs.Project.ProjectCategory;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectCategoryController : ControllerBase
    {
        private readonly IProjectCategoryInterface _projectCategoryRepository;
        public ProjectCategoryController(IProjectCategoryInterface projectCategoryRepo)
        {
            _projectCategoryRepository = projectCategoryRepo;
        }

        // GET: api/ProjectCategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectCategory>>> GetProjectCategories()
        {
            try
            {
                var projectCategories = await _projectCategoryRepository.GetProjectCategories();
                IEnumerable<ProjectCategoryDTO> projectCategoryDTOs = projectCategories.Select(pc => new ProjectCategoryDTO
                {
                    ProjectCategory_ID = pc.ProjectCategory_ID,
                    ProjectCategory_Name = pc.ProjectCategory_Name
                });
                return Ok(projectCategoryDTOs);

            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/ProjectCategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectCategory>> GetProjectCategory(int id)
        {
            try
            {
                var projectCategory = await _projectCategoryRepository.GetProjectCategory(id);
                if (projectCategory == null)
                {
                    return NotFound();
                }
                var returndto = new ProjectCategoryDTO
                {
                    ProjectCategory_ID = projectCategory.ProjectCategory_ID,
                    ProjectCategory_Name = projectCategory.ProjectCategory_Name
                };
                return Ok(returndto);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // POST: api/ProjectCategory
        [HttpPost]
        public async Task<ActionResult<ProjectCategory>> CreateProjectCategory([FromBody] ProjectCategoryDTO projectCategorydto)
        {
            try
            {
                var projectCategory = new ProjectCategory
                {
                    ProjectCategory_Name = projectCategorydto.ProjectCategory_Name
                };
                await _projectCategoryRepository.AddProjectCategory(projectCategory);

                var returndto = new ProjectCategoryDTO
                {
                    ProjectCategory_ID = projectCategory.ProjectCategory_ID,
                    ProjectCategory_Name = projectCategory.ProjectCategory_Name
                };

                return Ok(returndto);
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/ProjectCategory/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ProjectCategory>> UpdateProjectCategory(int id, [FromBody] ProjectCategoryDTO projectCategorydto)
        {
            try
            {
                var projectCategory = new ProjectCategory
                {
                    ProjectCategory_ID = id,
                    ProjectCategory_Name = projectCategorydto.ProjectCategory_Name
                };
                await _projectCategoryRepository.UpdateProjectCategory(projectCategory);

                var returndto = new ProjectCategoryDTO
                {
                    ProjectCategory_ID = projectCategory.ProjectCategory_ID,
                    ProjectCategory_Name = projectCategory.ProjectCategory_Name
                };

                return Ok(returndto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/ProjectCategory/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProjectCategory>> DeleteProjectCategory(int id)
        {
            try
            {
                var projectCategory = await _projectCategoryRepository.GetProjectCategory(id);
                if (projectCategory == null)
                {
                    return NotFound("Silindi");
                }
                await _projectCategoryRepository.DeleteProjectCategory(projectCategory);
                return Ok("Silindi.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/ProjectCategory/5/Projects
        // projects of selected project category
    }
}
