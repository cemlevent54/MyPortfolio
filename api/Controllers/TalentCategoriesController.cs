using api.DTOs.Talent.TalentCategory;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TalentCategoriesController : ControllerBase
    {
        private readonly ITalentCategoryInterface _talentCategoryRepository;
        public TalentCategoriesController(ITalentCategoryInterface talentCategoryRepository)
        {
            _talentCategoryRepository = talentCategoryRepository;
        }

        // GET: api/TalentCategories
        [HttpGet]
        public async Task<IActionResult> GetSkillCategories()
        {
            try
            {
                var skillCategories = await _talentCategoryRepository.GetSkillCategories();
                IEnumerable<TalentCategoryDTO> skillsCategoryDTOs = skillCategories.Select(x => new TalentCategoryDTO
                {
                    SkillCategory_ID = x.SkillCategory_ID,
                    SkillCategory_Name = x.SkillCategory_Name,
                    SkillCategory_IconUrl = x.SkillCategory_IconUrl
                });

                return Ok(skillsCategoryDTOs);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/TalentCategories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSkillCategory(int id)
        {
            try
            {
                var skillCategory = await _talentCategoryRepository.GetSkillCategory(id);
                if (skillCategory == null)
                {
                    return NotFound();
                }

                var skillCategoryDTO = new TalentCategoryDTO
                {
                    SkillCategory_ID = skillCategory.SkillCategory_ID,
                    SkillCategory_Name = skillCategory.SkillCategory_Name,
                    SkillCategory_IconUrl = skillCategory.SkillCategory_IconUrl
                };

                return Ok(skillCategoryDTO);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // POST: api/TalentCategories
        [HttpPost]
        public async Task<IActionResult> CreateTalentCategories([FromBody] TalentCategoryDTO skillsCategoryDTO)
        {
            try
            {
                var existingSkillCategory = await _talentCategoryRepository.GetSkillCategories();

                if (existingSkillCategory.Any(x => x.SkillCategory_Name == skillsCategoryDTO.SkillCategory_Name))
                {
                    return BadRequest("Skill Category already exists");
                }
                else
                {
                    var skillCategory = new SkillCategories
                    {
                        SkillCategory_Name = skillsCategoryDTO.SkillCategory_Name,
                        SkillCategory_IconUrl = skillsCategoryDTO.SkillCategory_IconUrl
                    };

                    var createdSkillCategory = await _talentCategoryRepository.AddSkillCategory(skillCategory);

                    var returndto = new TalentCategoryDTO
                    {
                        SkillCategory_ID = createdSkillCategory.SkillCategory_ID,
                        SkillCategory_Name = createdSkillCategory.SkillCategory_Name,
                        SkillCategory_IconUrl = createdSkillCategory.SkillCategory_IconUrl
                    };

                    return Ok(returndto);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/TalentCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSkillCategory(int id, [FromBody] TalentCategoryDTO skillsCategoryDTO)
        {
            try
            {
                var updatedSkillCategory = await _talentCategoryRepository.GetSkillCategory(id);

                if (updatedSkillCategory == null)
                {
                    return NotFound();
                }
                else
                {
                    updatedSkillCategory.SkillCategory_Name = skillsCategoryDTO.SkillCategory_Name;
                    updatedSkillCategory.SkillCategory_IconUrl = skillsCategoryDTO.SkillCategory_IconUrl;

                    await _talentCategoryRepository.UpdateSkillCategory(updatedSkillCategory);

                    var returndto = new TalentCategoryDTO
                    {
                        SkillCategory_ID = updatedSkillCategory.SkillCategory_ID,
                        SkillCategory_Name = updatedSkillCategory.SkillCategory_Name,
                        SkillCategory_IconUrl = updatedSkillCategory.SkillCategory_IconUrl
                    };

                    return Ok(returndto);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/TalentCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkillCategory(int id)
        {
            try
            {
                var skillCategory = await _talentCategoryRepository.GetSkillCategory(id);

                if (skillCategory == null)
                {
                    return NotFound();
                }
                else
                {
                    await _talentCategoryRepository.DeleteSkillCategory(skillCategory);

                    var returndto = new TalentCategoryDTO
                    {
                        SkillCategory_ID = skillCategory.SkillCategory_ID,
                        SkillCategory_Name = skillCategory.SkillCategory_Name,
                        SkillCategory_IconUrl = skillCategory.SkillCategory_IconUrl
                    };

                    return Ok(returndto);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
