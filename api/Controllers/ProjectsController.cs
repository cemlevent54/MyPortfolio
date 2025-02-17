using api.DTOs.Education;
using api.DTOs.Project;
using api.DTOs.Project.ProjectCategory;
using api.DTOs.Project.ProjectTechnology;
using api.Interfaces;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsInterface _projectRepository;
        private readonly IProjectCategoryInterface _projectCategoryRepository;
        private readonly ITechnologyInterface _technologyRepository;
        private readonly UserManager<AppUsers> _userManager;
        private readonly IPhotoService _photoService;
        private readonly ILogger<ProjectsController> _logger;

        public ProjectsController(IProjectsInterface projectRepository
            , IProjectCategoryInterface projectCategoryRepository, 
            UserManager<AppUsers> userManager,
            IPhotoService photoService,
            ILogger<ProjectsController> logger,
            ITechnologyInterface technologyRepository)
        {
            _projectRepository = projectRepository;
            _projectCategoryRepository = projectCategoryRepository;
            _userManager = userManager;
            _photoService=photoService;
            _logger=logger;
            _technologyRepository=technologyRepository;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            try
            {
                var projects = await _projectRepository.GetProjects();
                IEnumerable<ProjectDTO> projectDTOs = projects.Select(project => new ProjectDTO
                {
                    Project_ID = project.Project_ID,
                    Project_Title = project.Project_Title,
                    Project_About = project.Project_About,
                    Project_GithubUrl = project.Project_GithubUrl,
                    Project_LiveUrl = project.Project_LiveUrl,
                    Project_StartDate = project.Project_StartDate,
                    Project_EndDate = project.Project_EndDate,
                    Project_IsActive = project.Project_IsActive,
                    User_ID = project.User_ID,
                    ProjectCategory_ID = project.ProjectCategory_ID,
                    Project_Photo = project.Project_Photo,
                    ProjectTechIDDTOs = project.Technologies?.Select(x => new ProjectTechIDDTO
                    {
                        ProjectTech_ID = x.Technology_ID
                    }).ToList() ?? new List<ProjectTechIDDTO>()  // Null ise boş liste döndür

                });
                return Ok(projectDTOs);

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(int id)
        {
            try
            {
                var project = await _projectRepository.GetProjectById(id);
                if (project == null)
                {
                    return NotFound();
                }
                ProjectDTO projectDTO = new ProjectDTO
                {
                    Project_ID = project.Project_ID,
                    Project_Title = project.Project_Title,
                    Project_About = project.Project_About,
                    Project_GithubUrl = project.Project_GithubUrl,
                    Project_LiveUrl = project.Project_LiveUrl,
                    Project_StartDate = project.Project_StartDate,
                    Project_EndDate = project.Project_EndDate,
                    Project_IsActive = project.Project_IsActive,
                    User_ID = project.User_ID,
                    ProjectCategory_ID = project.ProjectCategory_ID,
                    Project_Photo = project.Project_Photo,
                    ProjectTechIDDTOs = project.Technologies?.Select(x => new ProjectTechIDDTO
                    {
                        ProjectTech_ID = x.Technology_ID
                    }).ToList() ?? new List<ProjectTechIDDTO>()  // Null ise boş liste döndür
                };
                return Ok(projectDTO);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // POST: api/Projects
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] ProjectDTO projectDTO)
        {
            try
            {
                // Proje başlığı zaten var mı?
                var existingProjects = await _projectRepository.GetProjects();
                if (existingProjects.Any(p => p.Project_Title == projectDTO.Project_Title))
                {
                    return BadRequest("A project with this title already exists.");
                }

                // Kullanıcı kontrolü
                if (string.IsNullOrEmpty(projectDTO.User_ID))
                {
                    return BadRequest("User ID is required.");
                }

                var user = await _userManager.FindByIdAsync(projectDTO.User_ID);
                if (user == null)
                {
                    return BadRequest("User not found.");
                }

                // Kategori kontrolü
                var category = await _projectCategoryRepository.GetProjectCategory(projectDTO.ProjectCategory_ID);
                if (category == null)
                {
                    return BadRequest("Category not found.");
                }

                // Tarih formatı kontrolü
                if (!string.IsNullOrEmpty(projectDTO.Project_StartDate) && !string.IsNullOrEmpty(projectDTO.Project_EndDate))
                {
                    if (!DateTime.TryParseExact(projectDTO.Project_StartDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)
                        || !DateTime.TryParseExact(projectDTO.Project_EndDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                    {
                        return BadRequest("Invalid date format. Use dd.MM.yyyy");
                    }
                }

                // Proje oluşturuluyor
                var project = new Projects
                {
                    Project_Title = projectDTO.Project_Title,
                    Project_About = projectDTO.Project_About,
                    Project_GithubUrl = projectDTO.Project_GithubUrl,
                    Project_LiveUrl = projectDTO.Project_LiveUrl,
                    Project_StartDate = projectDTO.Project_StartDate,
                    Project_EndDate = projectDTO.Project_EndDate,
                    Project_IsActive = projectDTO.Project_IsActive,
                    User_ID = projectDTO.User_ID,
                    ProjectCategory_ID = projectDTO.ProjectCategory_ID,
                    Project_Photo = projectDTO.Project_Photo,
                    Technologies = new List<Technologies>()  // Boş liste olarak başlat
                };

                // Projeyi kaydet
                await _projectRepository.AddProject(project);

                // Teknolojileri projeye ekle
                if (projectDTO.ProjectTechIDDTOs != null && projectDTO.ProjectTechIDDTOs.Any())
                {
                    foreach (var tech in projectDTO.ProjectTechIDDTOs)
                    {
                        var addedtech = await _technologyRepository.GetTechnology(tech.ProjectTech_ID);
                        if (addedtech != null)
                        {
                            await _projectRepository.AddTechnologiesToProject(project.Project_ID, addedtech.Technology_ID);
                        }
                    }
                }

                // DTO dönüşümü
                var returndto = new ProjectDTO
                {
                    Project_ID = project.Project_ID,
                    Project_Title = project.Project_Title,
                    Project_About = project.Project_About,
                    Project_GithubUrl = project.Project_GithubUrl,
                    Project_LiveUrl = project.Project_LiveUrl,
                    Project_StartDate = project.Project_StartDate,
                    Project_EndDate = project.Project_EndDate,
                    Project_IsActive = project.Project_IsActive,
                    User_ID = project.User_ID,
                    ProjectCategory_ID = project.ProjectCategory_ID,
                    Project_Photo = project.Project_Photo,
                    ProjectTechIDDTOs = project.Technologies.Select(t => new ProjectTechIDDTO
                    {
                        ProjectTech_ID = t.Technology_ID
                    }).ToList() 
                };

                return Ok(new
                {
                    Project = returndto,
                    Technologies = returndto.ProjectTechIDDTOs
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        // PUT: api/Projects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] ProjectDTO projectDTO)
        {
            try
            {
                // Parametre kontrolü
                if (projectDTO == null || id == 0)
                {
                    return BadRequest("Invalid request: DTO is null or ID is 0.");
                }

                var project = await _projectRepository.GetProjectById(id);
                if (project == null)
                {
                    return NotFound("Project not found.");
                }

                // Kullanıcı kontrolü
                var userId = projectDTO.User_ID;
                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest("User ID is required.");
                }

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return BadRequest("User not found.");
                }

                // Kategori kontrolü
                var category = await _projectCategoryRepository.GetProjectCategory(projectDTO.ProjectCategory_ID);
                if (category == null)
                {
                    return BadRequest("Category not found.");
                }

                // Tarih formatı kontrolü
                if (!string.IsNullOrEmpty(projectDTO.Project_StartDate) && !string.IsNullOrEmpty(projectDTO.Project_EndDate))
                {
                    if (!DateTime.TryParseExact(projectDTO.Project_StartDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)
                        || !DateTime.TryParseExact(projectDTO.Project_EndDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                    {
                        return BadRequest("Invalid date format. Use dd.MM.yyyy.");
                    }
                }

                // Proje bilgilerini güncelleme
                project.Project_Title = projectDTO.Project_Title;
                project.Project_About = projectDTO.Project_About;
                project.Project_GithubUrl = projectDTO.Project_GithubUrl;
                project.Project_LiveUrl = projectDTO.Project_LiveUrl;
                project.Project_StartDate = projectDTO.Project_StartDate;
                project.Project_EndDate = projectDTO.Project_EndDate;
                project.Project_IsActive = projectDTO.Project_IsActive;
                project.User_ID = projectDTO.User_ID;
                project.ProjectCategory_ID = projectDTO.ProjectCategory_ID;
                project.Project_Photo = projectDTO.Project_Photo;

                // Teknolojileri güncelleme
                if (projectDTO.ProjectTechIDDTOs != null)
                {
                    // Teknolojileri önce kaldır, sonra yenilerini ekle
                    foreach (var tech in project.Technologies.ToList())
                    {
                        await _projectRepository.RemoveTechnologiesFromProject(project.Project_ID, tech.Technology_ID);
                    }

                    foreach (var tech in projectDTO.ProjectTechIDDTOs)
                    {
                        var addedTech = await _technologyRepository.GetTechnology(tech.ProjectTech_ID);
                        if (addedTech != null)
                        {
                            await _projectRepository.AddTechnologiesToProject(project.Project_ID, addedTech.Technology_ID);
                        }
                    }
                }

                // Projeyi güncelle
                await _projectRepository.UpdateProject(project);

                // Güncellenmiş proje bilgilerini döndür
                var returnDto = new ProjectDTO
                {
                    Project_ID = project.Project_ID,
                    Project_Title = project.Project_Title,
                    Project_About = project.Project_About,
                    Project_GithubUrl = project.Project_GithubUrl,
                    Project_LiveUrl = project.Project_LiveUrl,
                    Project_StartDate = project.Project_StartDate,
                    Project_EndDate = project.Project_EndDate,
                    Project_IsActive = project.Project_IsActive,
                    User_ID = project.User_ID,
                    ProjectCategory_ID = project.ProjectCategory_ID,
                    Project_Photo = project.Project_Photo,
                    ProjectTechIDDTOs = project.Technologies.Select(x => new ProjectTechIDDTO
                    {
                        ProjectTech_ID = x.Technology_ID
                    }).ToList()
                };

                return Ok(returnDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            try
            {
                var project = await _projectRepository.GetProjectById(id);
                if (project == null)
                {
                    return NotFound("Project not found.");
                }

                // Projeyi sil
                await _projectRepository.DeleteProject(project);

                return Ok("Project successfully deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPost("upload-photo")]
        public async Task<IActionResult> UploadPhoto(IFormFile file)
        {
            try
            {
                if (file == null)
                {
                    _logger.LogWarning("No file was uploaded.");
                    return BadRequest("No file uploaded");
                }

                _logger.LogInformation("File upload started. File name: {FileName}", file.FileName);

                var photoUrl = await _photoService.SavePhotoAsync(file, "project_pictures");

                _logger.LogInformation("File uploaded successfully. Photo URL: {PhotoUrl}", photoUrl);

                return Ok(new { PhotoUrl = photoUrl });
            }
            catch (Exception ex)
            {
                // Critical log with exception details
                _logger.LogCritical(ex, "An error occurred while uploading the photo. Exception: {ExceptionMessage}", ex.Message);
                return StatusCode(500, new { Message = "An error occurred while uploading photo", Details = ex.Message });
            }
        }


        // GET: api/Projects/GetProjectCategories
        [HttpGet("projectcategory/{projectid}")]
        public async Task<IActionResult> GetCategoriyOfProject(int projectid)
        {
            try
            {
                var categories = await _projectCategoryRepository.GetProjectCategories();
                var project = await _projectRepository.GetProjectById(projectid);
                if (project == null)
                {
                    return NotFound("Project not found.");
                }

                var category = categories.FirstOrDefault(c => c.ProjectCategory_ID == project.ProjectCategory_ID);
                if (category == null)
                {
                    return NotFound("Category not found.");
                }

                var returndto = new ProjectCategoryDTO
                {
                    ProjectCategory_ID = category.ProjectCategory_ID,
                    ProjectCategory_Name = category.ProjectCategory_Name
                };

                return Ok(returndto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/Projects/GetProjectTechnologies
        [HttpGet("projecttechnology/{projectid}")]
        public async Task<IActionResult> GetTechnologiesOfProject(int projectid)
        {
            try
            {
                var project = await _projectRepository.GetProjectById(projectid);
                if (project == null)
                {
                    return NotFound("Project not found.");
                }

                var technologies = project.Technologies.Select(t => new ProjectTechDTO
                {
                    Technology_ID = t.Technology_ID,
                    Technology_Name = t.Technology_Name,
                    Technology_IconUrl = t.Technology_IconUrl
                });

                return Ok(technologies);



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }







    }
}
