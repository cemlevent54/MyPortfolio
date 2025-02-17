using api.DTOs.Education;
using api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using Microsoft.AspNetCore.Identity;
using System.Globalization;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationsController : ControllerBase
    {
        private readonly IEducationInterface _educationRepository;
        private readonly IEducationCategoryInterface _educationCategoryRepository;
        private readonly UserManager<AppUsers> _userManager;
        public EducationsController(IEducationInterface educationInterface
            , IEducationCategoryInterface educationCategoryRepository,UserManager<AppUsers> userManager
            )
        {
            _educationRepository = educationInterface;
            _educationCategoryRepository = educationCategoryRepository;
            _userManager = userManager;
        }

        // GET: api/Educations
        [HttpGet]
        public async Task<IActionResult> GetEducations()
        {
            var educations = await _educationRepository.GetEducations();

            IEnumerable<EducationDto> educationdtos = educations.Select(e => new EducationDto
            {
                Education_ID = e.Education_ID,
                Education_Title = e.Education_Title,
                Education_Organization = e.Education_Organization,
                Education_Subject = e.Education_Subject,
                Education_StartDate = e.Education_StartDate,
                Education_EndDate = e.Education_EndDate,
                Education_CertificationUrl = e.Education_CertificationUrl,
                User_ID = e.AppUsers.Id,
                EducationCategory_ID = e.EducationCategory.EducationCategory_ID,
            });

            return Ok(educationdtos);
        }

        

        // POST : api/Educations
        [HttpPost]
        public async Task<IActionResult> CreateEducation([FromBody] EducationDto educationDto)
        {
            var existingEducations = await _educationRepository.GetEducations();

            if (existingEducations.Any(x => x.Education_ID == educationDto.Education_ID))
            {
                return BadRequest("Education already exists");
            }

            else
            {
                //education ekle

                // user id
                var userId = educationDto.User_ID;
                if( userId == null)
                {
                    return BadRequest("User ID is required");
                }

                var user = await _userManager.FindByIdAsync(userId);
                if(user == null)
                {
                    return BadRequest("User not found");
                }



                // education category id
                var category = await _educationCategoryRepository.GetEducationCategoryById(educationDto.EducationCategory_ID);
                if(category == null)
                {
                    return BadRequest("Category not found");
                }

                

                // validate dates for dd.mm.yyyy format
                if (educationDto.Education_StartDate != null && educationDto.Education_EndDate != null)
                {
                    if (!DateTime.TryParseExact(educationDto.Education_StartDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)
                        || !DateTime.TryParseExact(educationDto.Education_EndDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)
                        )
                    {
                        return BadRequest("Invalid date format. Use dd.MM.yyyy");
                    }
                }



                var newEducation = new Educations
                {
                    Education_ID = educationDto.Education_ID,
                    Education_Title = educationDto.Education_Title,
                    Education_Organization = educationDto.Education_Organization,
                    Education_Subject = educationDto.Education_Subject,
                    Education_StartDate = educationDto.Education_StartDate,
                    Education_EndDate = educationDto.Education_EndDate,
                    Education_CertificationUrl = educationDto.Education_CertificationUrl,
                    User_ID = educationDto.User_ID,
                    EducationCategory_ID = educationDto.EducationCategory_ID
                };

                await _educationRepository.CreateEducation(newEducation);

                var returndto = new EducationDto
                {
                    Education_ID = newEducation.Education_ID,
                    Education_Title = newEducation.Education_Title,
                    Education_Organization = newEducation.Education_Organization,
                    Education_Subject = newEducation.Education_Subject,
                    Education_StartDate = newEducation.Education_StartDate,
                    Education_EndDate = newEducation.Education_EndDate,
                    Education_CertificationUrl = newEducation.Education_CertificationUrl,
                    User_ID = newEducation.AppUsers.Id,
                    EducationCategory_ID = newEducation.EducationCategory.EducationCategory_ID
                };

                return Ok(returndto);

            }


        }

        // PUT : api/Educations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEducation(int id, [FromBody] UpdateEducationDTO updateEducationDto)
        {

            if(updateEducationDto == null || id == 0)
            {
                return BadRequest($"Invalid request. DTO or id is invalid {id}");
            }

            var education = await _educationRepository.GetEducationById(id);
            if (education == null)
            {
                return BadRequest("Education not found");
            }

            else
            {
                // user id
                var userId = updateEducationDto.User_ID;
                if (userId == null)
                {
                    return BadRequest("User ID is required");
                }

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return BadRequest("User not found");
                }

                // education category id
                var category = await _educationCategoryRepository.GetEducationCategoryById(updateEducationDto.EducationCategory_ID);
                if (category == null)
                {
                    return BadRequest("Category not found");
                }

                // validate dates for dd.mm.yyyy format
                if (updateEducationDto.Education_StartDate != null && updateEducationDto.Education_EndDate != null)
                {
                    if (!DateTime.TryParseExact(updateEducationDto.Education_StartDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)
                        || !DateTime.TryParseExact(updateEducationDto.Education_EndDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _)
                        )
                    {
                        return BadRequest("Invalid date format. Use dd.MM.yyyy");
                    }
                }

                education.Education_Title = updateEducationDto.Education_Title;
                education.Education_Organization = updateEducationDto.Education_Organization;
                education.Education_Subject = updateEducationDto.Education_Subject;
                education.Education_StartDate = updateEducationDto.Education_StartDate;
                education.Education_EndDate = updateEducationDto.Education_EndDate;
                education.Education_CertificationUrl = updateEducationDto.Education_CertificationUrl;
                education.User_ID = updateEducationDto.User_ID;
                education.EducationCategory_ID = updateEducationDto.EducationCategory_ID;

                await _educationRepository.UpdateEducation(education);

                var returndto = new EducationDto
                {
                    Education_ID = education.Education_ID,
                    Education_Title = education.Education_Title,
                    Education_Organization = education.Education_Organization,
                    Education_Subject = education.Education_Subject,
                    Education_StartDate = education.Education_StartDate,
                    Education_EndDate = education.Education_EndDate,
                    Education_CertificationUrl = education.Education_CertificationUrl,
                    User_ID = education.AppUsers.Id,
                    EducationCategory_ID = education.EducationCategory.EducationCategory_ID
                };

                return Ok(returndto);
            }
        }

        // DELETE : api/Educations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEducation(int id)
        {
            var existingEducations = await _educationRepository.GetEducations();
            if (existingEducations != null)
            {
                if (existingEducations.Any(x => x.Education_ID == id))
                {
                    var education = await _educationRepository.GetEducationById(id);
                    await _educationRepository.DeleteEducation(education);
                    // education to educationdto
                    EducationDto educationDto = new EducationDto
                    {
                        Education_ID = education.Education_ID,
                        Education_Title = education.Education_Title,
                        Education_Organization = education.Education_Organization,
                        Education_Subject = education.Education_Subject,
                        Education_StartDate = education.Education_StartDate,
                        Education_EndDate = education.Education_EndDate,
                        Education_CertificationUrl = education.Education_CertificationUrl,
                        User_ID = education.AppUsers.Id,
                        EducationCategory_ID = education.EducationCategory.EducationCategory_ID
                    };
                    return Ok(educationDto);
                }
                else
                {
                    return BadRequest("Education not found");
                }
            }

            else
            {
                return BadRequest("Education not found");
            }

        }


    }
}
