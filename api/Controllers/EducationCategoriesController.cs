using api.DTOs.EducationCategory;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationCategoriesController : ControllerBase
    {
        private readonly IEducationCategoryInterface _educationCategoryRepo;
        public EducationCategoriesController(IEducationCategoryInterface educationCategoryRepo)
        {
            _educationCategoryRepo = educationCategoryRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEducationCategory([FromBody] EducationCategoryDTO educationCategoryDTO)
        {
            var existingEducationCategory = await _educationCategoryRepo.GetEducationCategories();

            if (existingEducationCategory.Any(x => x.EducationCategory_Name == educationCategoryDTO.EducationCategory_Name))
            {
                return BadRequest("Education Category already exists");
            }
            else
            {
                var educationCategory = new EducationCategory
                {
                    EducationCategory_Name = educationCategoryDTO.EducationCategory_Name
                };

                var createdEducationCategory = await _educationCategoryRepo.CreateEducationCategory(educationCategory);
                return Ok(createdEducationCategory);
            }

            



        }

        [HttpGet]
        public async Task<IActionResult> GetEducationCategories()
        {
            var educationCategories = await _educationCategoryRepo.GetEducationCategories();
            IEnumerable<EducationCategoryDTO> getEducationCategoryDTOs = educationCategories.Select(x => new EducationCategoryDTO
            {
                EducationCategory_ID = x.EducationCategory_ID,
                EducationCategory_Name = x.EducationCategory_Name
            });

            return Ok(getEducationCategoryDTOs);

        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateEducationCategory(int categoryId, [FromBody] UpdateEducationCategoryDTO educationCategoryDto)
        {
            var updatedEducationCategory = await _educationCategoryRepo.GetEducationCategoryById(categoryId);

            if (updatedEducationCategory == null)
            {
                return NotFound();
            }
            else
            {
                updatedEducationCategory.EducationCategory_Name = educationCategoryDto.EducationCategory_Name;
                await _educationCategoryRepo.UpdateEducationCategory(updatedEducationCategory);
                return Ok(updatedEducationCategory);
            }

        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteEducationCategory(int categoryId)
        {
            var educationCategory = await _educationCategoryRepo.GetEducationCategoryById(categoryId);

            if (educationCategory == null)
            {
                return NotFound();
            }
            else
            {
                await _educationCategoryRepo.DeleteEducationCategory(educationCategory);
                return Ok(educationCategory);
            }
        }
        
    }
}
