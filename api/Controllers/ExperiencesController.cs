using api.DTOs.Education;
using api.DTOs.Experiences;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperiencesController : ControllerBase
    {
        private readonly UserManager<AppUsers> _userManager;
        private readonly IExperienceInterface _experiencerepo;
        public ExperiencesController(IExperienceInterface experiencerepo, UserManager<AppUsers> userManager)
        {
            _experiencerepo = experiencerepo;
            _userManager=userManager;
        }

        // GET : api/Experiences
        [HttpGet]
        public async Task<IActionResult> GetExperiences()
        {
            var experiences = await _experiencerepo.GetExperiences();

            IEnumerable<ExperiencesDTO> experiencesDTOs = experiences.Select(experience => new ExperiencesDTO
            {
                Experience_Id = experience.Experience_Id,
                Experience_Title = experience.Experience_Title,
                Experience_Company = experience.Experience_Company,
                Experience_About = experience.Experience_About,
                Experience_StartDate = experience.Experience_StartDate,
                Experience_EndDate = experience.Experience_EndDate,
                User_ID = experience.User_ID
            });
            return Ok(experiencesDTOs);

        }

        // GET : api/Experiences/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExperienceById(int id)
        {
            var experience = await _experiencerepo.GetExperienceById(id);
            if (experience == null)
            {
                return NotFound();
            }
            var experienceDTO = new ExperiencesDTO
            {
                Experience_Id = experience.Experience_Id,
                Experience_Title = experience.Experience_Title,
                Experience_Company = experience.Experience_Company,
                Experience_About = experience.Experience_About,
                Experience_StartDate = experience.Experience_StartDate,
                Experience_EndDate = experience.Experience_EndDate,
                User_ID = experience.User_ID
            };
            return Ok(experienceDTO);
        }

        // POST : api/Experiences
        [HttpPost]
        public async Task<IActionResult> PostExperience(ExperiencesDTO experienceDTO)
        {
            var existingExperiences = await _experiencerepo.GetExperiences();
            if (existingExperiences.Any(x => x.Experience_Id == experienceDTO.Experience_Id))
            {
                return BadRequest("Experience with this ID already exists");
            }
            else
            {
                // user id
                var userId = experienceDTO.User_ID;
                if (userId == null)
                {
                    return BadRequest("User ID is required");
                }

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return BadRequest("User not found");
                }

                // validate dates for dd.mm.yyyy format
                if (experienceDTO.Experience_StartDate != null && experienceDTO.Experience_EndDate != null)
                {
                    if (!DateTime.TryParseExact(experienceDTO.Experience_StartDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)
                        || !DateTime.TryParseExact(experienceDTO.Experience_EndDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)
                        )
                    {
                        return BadRequest("Invalid date format. Use dd.MM.yyyy");
                    }
                }

                var experience = new Experiences
                {
                    Experience_Id = experienceDTO.Experience_Id,
                    Experience_Title = experienceDTO.Experience_Title,
                    Experience_Company = experienceDTO.Experience_Company,
                    Experience_About = experienceDTO.Experience_About,
                    Experience_StartDate = experienceDTO.Experience_StartDate,
                    Experience_EndDate = experienceDTO.Experience_EndDate,
                    User_ID = experienceDTO.User_ID,
                };
                await _experiencerepo.CreateExperience(experience);

                var returndto = new ExperiencesDTO
                {
                    Experience_Id = experience.Experience_Id,
                    Experience_Title = experience.Experience_Title,
                    Experience_Company = experience.Experience_Company,
                    Experience_StartDate = experience.Experience_StartDate,
                    Experience_EndDate = experience.Experience_EndDate,
                    Experience_About = experience.Experience_About,
                    User_ID = experience.AppUsers.Id,
                };
                return Ok(returndto);
            }
        }


        // PUT : api/Experiences/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExperience(int id, [FromBody] ExperiencesDTO experienceDTO)
        {
            var existingExperience = await _experiencerepo.GetExperienceById(id);
            if (existingExperience == null)
            {
                return NotFound();
            }
            else
            {
                // user id
                var userId = experienceDTO.User_ID;
                if (userId == null)
                {
                    return BadRequest("User ID is required");
                }

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return BadRequest("User not found");
                }

                // validate dates for dd.mm.yyyy format
                if (experienceDTO.Experience_StartDate != null && experienceDTO.Experience_EndDate != null)
                {
                    if (!DateTime.TryParseExact(experienceDTO.Experience_StartDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)
                        || !DateTime.TryParseExact(experienceDTO.Experience_EndDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)
                        )
                    {
                        return BadRequest("Invalid date format. Use dd.MM.yyyy");
                    }
                }

                //existingExperience.Experience_Id = experienceDTO.Experience_Id;
                existingExperience.Experience_Title = experienceDTO.Experience_Title;
                existingExperience.Experience_Company = experienceDTO.Experience_Company;
                existingExperience.Experience_About = experienceDTO.Experience_About;
                existingExperience.Experience_StartDate = experienceDTO.Experience_StartDate;
                existingExperience.Experience_EndDate = experienceDTO.Experience_EndDate;
                existingExperience.User_ID = experienceDTO.User_ID;

                await _experiencerepo.UpdateExperience(existingExperience);

                var returndto = new ExperiencesDTO
                {
                    Experience_Id = experienceDTO.Experience_Id,
                    Experience_Title = experienceDTO.Experience_Title,
                    Experience_Company = experienceDTO.Experience_Company,
                    Experience_About = experienceDTO.Experience_About,
                    Experience_StartDate = experienceDTO.Experience_StartDate,
                    Experience_EndDate = experienceDTO.Experience_EndDate,
                    User_ID = experienceDTO.User_ID,
                };
                return Ok(returndto);
            }
        }

        // DELETE : api/Experiences/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExperience(int id)
        {
            if (id == 0)
            {
                return BadRequest("Invalid id");
            }

            var existingExperiences = await _experiencerepo.GetExperiences();
            if(existingExperiences != null)
            {
                if(existingExperiences.Any(x => x.Experience_Id == id))
                {
                    var experience = await _experiencerepo.GetExperienceById(id);
                    await _experiencerepo.DeleteExperience(experience);

                    ExperiencesDTO experiencesDTO = new ExperiencesDTO
                    {
                        Experience_Id = experience.Experience_Id,
                        Experience_Title = experience.Experience_Title,
                        Experience_Company = experience.Experience_Company,
                        Experience_About = experience.Experience_About,
                        Experience_StartDate = experience.Experience_StartDate,
                        Experience_EndDate = experience.Experience_EndDate,
                        User_ID = experience.User_ID,
                    };

                    return Ok("Silindi.");
                }
                else
                {
                    return Ok("Silindi.");
                }
            }
            else
            {
                return Ok("Silindi.");
            }
        }
    }
}
