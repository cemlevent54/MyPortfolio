using api.DTOs.Frontend;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FrontEndController : ControllerBase
    {
        private readonly IEducationCategoryInterface _educationCategoryRepository;
        private readonly IEducationInterface _educationRepository;
        private readonly IExperienceInterface _experienceRepository;
        private readonly IHobbyInterface _hobbyRepository;
        private readonly IProjectCategoryInterface _projectCategoryRepository;
        private readonly IProjectsInterface _projectRepository;
        private readonly ITalentCategoryInterface _talentCategoryRepository;
        private readonly ITalentsInterface _talentsRepository;
        private readonly ITechnologyInterface _technologyRepository;
        private readonly IUserInterface _userRepository;
        private readonly UserManager<AppUsers> _userManager;
        //configuration 
        private readonly IConfiguration _configuration;

        public FrontEndController(
            IEducationCategoryInterface educationCategoryRepository,
            IEducationInterface educationRepository,
            IExperienceInterface experienceRepository,
            IHobbyInterface hobbyRepository,
            IProjectCategoryInterface projectCategoryRepository,
            IProjectsInterface projectRepository,
            ITalentCategoryInterface talentCategoryRepository,
            ITalentsInterface talentsRepository,
            ITechnologyInterface technologyRepository,
            IUserInterface userRepository,
            IConfiguration configuration)
        {
            _educationCategoryRepository = educationCategoryRepository;
            _educationRepository = educationRepository;
            _experienceRepository = experienceRepository;
            _hobbyRepository = hobbyRepository;
            _projectCategoryRepository = projectCategoryRepository;
            _projectRepository = projectRepository;
            _talentCategoryRepository = talentCategoryRepository;
            _talentsRepository = talentsRepository;
            _technologyRepository = technologyRepository;
            _userRepository = userRepository;
            _configuration=configuration;
        }

        [HttpGet("GetAbout")]
        public async Task<IActionResult> GetUsersAbout()
        {
            try
            {
                var userid = _configuration["UserSettings:Userid"];
                var user = await _userRepository.GetUserByFId(userid);

                GetUserInfoDTO userDTO = new GetUserInfoDTO
                {
                    User_Id = user.Id,
                    User_Name = user.User_Name,
                    User_Surname = user.User_Surname,
                    User_Email = user.Email,
                    User_Phone = user.User_PhoneNumber,
                    User_LivingCity = user.User_LivingCity,
                    User_About = user.User_About,
                    User_BirthDate = (user.User_BirthDate).ToString("dd.MM.yyyy"),
                    User_Job = user.User_Job,

                };

                return Ok(userDTO);
                

            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetEducations")]
        public async Task<IActionResult> GetUserEducations()
        {
            try
            {
                var userid = _configuration["UserSettings:Userid"];
                if (string.IsNullOrEmpty(userid))
                {
                    return BadRequest("User ID is missing in configuration.");
                }

                var user = await _userRepository.GetUserByFId(userid);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                var userEducations = await _educationRepository.GetEducations();
                var filteredEducations = userEducations.Where(x => x.User_ID == user.Id).ToList();

                var getUserEducationsDTOs = filteredEducations.Select(h => new GetUserEducationsDTO
                {
                    Education_ID = h.Education_ID,
                    Education_Title = h.Education_Title,
                    Education_Organization = h.Education_Organization,
                    Education_Subject = h.Education_Subject,
                    Education_StartDate = h.Education_StartDate,
                    Education_EndDate = h.Education_EndDate,
                    Education_CertificationUrl = h.Education_CertificationUrl,
                    User_ID = userid,
                    EducationCategory_ID = h.EducationCategory_ID,
                }).ToList();

                return Ok(getUserEducationsDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetEducationCategories")]
        public async Task<IActionResult> GetEducationCategories()
        {
            try
            {
                var educationcategories = await _educationCategoryRepository.GetEducationCategories();

                var educationcategoriesdto = educationcategories.Select(h => new GetUserEducationCategories
                {
                    EducationCategory_ID =h.EducationCategory_ID,
                    EducationCategory_Name =h.EducationCategory_Name,
                });

                return Ok(educationcategoriesdto);


            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetSkills")]
        public async Task<IActionResult> GetUsersSkills()
        {
            try
            {
                var userid = _configuration["UserSettings:Userid"];
                if (string.IsNullOrEmpty(userid))
                {
                    return BadRequest("User ID is missing in configuration.");
                }

                var user = await _userRepository.GetUserByFId(userid);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                var userSkills = await _talentsRepository.GetSkillsOfUser(user.Id);
                var getUserSkillsDTOs = userSkills.Select(h => new GetUserSkillsDTO
                {
                    Skill_ID = h.Skill_ID,
                    Skill_Name = h.Skill_Name,
                    Skill_IconUrl = h.Skill_IconUrl,
                    SkillCategory_ID = h.SkillCategory_ID,
                }).ToList();

                return Ok(getUserSkillsDTOs);





            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetSkillCategories")]
        public async Task<IActionResult> GetSkillCategories()
        {
            try
            {
                var skillcategories = await _talentCategoryRepository.GetSkillCategories();
                if (skillcategories == null)
                {
                    return NotFound("Skill Categories not found.");
                }

                var skillcategoriesdto = skillcategories.Select(h => new GetUserSkillCategoriesDTO
                {
                    SkillCategory_ID = h.SkillCategory_ID,
                    SkillCategory_Name = h.SkillCategory_Name,
                    SkillCategory_IconUrl = h.SkillCategory_IconUrl,
                });

                return Ok(skillcategoriesdto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetProjects")]
        public async Task<IActionResult> GetUserProjects()
        {
            try
            {
                var userid = _configuration["UserSettings:Userid"];
                if (string.IsNullOrEmpty(userid))
                {
                    return BadRequest("User ID is missing in configuration.");
                }

                var user = await _userRepository.GetUserByFId(userid);
                if (user == null)
                {
                    return NotFound("User not found.");
                }
                var projects = await _projectRepository.GetProjects();

                
                
                var getUserProjectsDTOs = projects
                    .Where(h => h.User_ID == user.Id)
                    .Select(h => new GetProjectsDTO
                {
                    Project_ID = h.Project_ID,
                    Project_Title = h.Project_Title,
                    Project_About = h.Project_About,
                    Project_GithubUrl = h.Project_GithubUrl,
                    Project_LiveUrl = h.Project_LiveUrl,
                    Project_StartDate = h.Project_StartDate,
                    Project_EndDate = h.Project_EndDate,
                    Project_IsActive = h.Project_IsActive,
                    User_ID = userid,
                    ProjectCategory_ID = h.ProjectCategory_ID,
                    Project_Photo = h.Project_Photo,
                }).ToList();

                return Ok(getUserProjectsDTOs);




            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetProjectCategories")]
        public async Task<IActionResult> GetProjectCategories()
        {
            try
            {
                var projectcategories = await _projectCategoryRepository.GetProjectCategories();
                if (projectcategories == null)
                {
                    return NotFound("Project Categories not found.");
                }

                var projectcategoriesdto = projectcategories.Select(h => new GetProjectCategoryDTO
                {
                    ProjectCategory_ID = h.ProjectCategory_ID,
                    ProjectCategory_Name = h.ProjectCategory_Name,
                });

                return Ok(projectcategoriesdto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetProjectTechnologies")]
        public async Task<IActionResult> GetProjectTechnologies()
        {
            try
            {
                var projecttechnologies = await _technologyRepository.GetTechnologies();
                if (projecttechnologies == null)
                {
                    return NotFound("Project Technologies not found.");
                }

                var projecttechnologiesdto = projecttechnologies.Select(h => new GetProjectTechnologiesDTO
                {
                    Technology_ID = h.Technology_ID,
                    Technology_Name = h.Technology_Name,
                    Technology_IconUrl = h.Technology_IconUrl,
                });

                return Ok(projecttechnologiesdto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetProjectTechnologies/{project_id}")]
        public async Task<IActionResult> GetProjectTechnologies(int project_id)
        {
            try
            {
                var projecttechnologies = await _projectRepository.GetTechnologiesUsedinProject(project_id);
                if (projecttechnologies == null)
                {
                    return NotFound("Project Technologies not found.");
                }

                var projecttechnologiesdto = projecttechnologies.Select(h => new GetProjectTechnologiesDTO
                {
                    Technology_ID = h.Technology_ID,
                    Technology_Name = h.Technology_Name,
                    Technology_IconUrl = h.Technology_IconUrl,
                });

                return Ok(projecttechnologiesdto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
