using api.DTOs.Talent;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TalentsController : ControllerBase
    {
        private readonly ITalentsInterface _talentRepository;
        private readonly ITalentCategoryInterface _talentCategoryRepository;
        public TalentsController(ITalentsInterface talentRepository, ITalentCategoryInterface talentCategoryRepository)
        {
            _talentRepository = talentRepository;
            _talentCategoryRepository=talentCategoryRepository;
        }

        // GET: api/Talents
        [HttpGet]
        public async Task<IActionResult> GetSkills()
        {
            try
            {
                var skills = await _talentRepository.GetSkills();
                IEnumerable<TalentsDTO> talentsDTOs = skills.Select(s => new TalentsDTO
                {
                    Skill_ID = s.Skill_ID,
                    Skill_Name = s.Skill_Name,
                    Skill_IconUrl = s.Skill_IconUrl,
                    SkillCategory_ID = s.SkillCategories.SkillCategory_ID
                });

                return Ok(talentsDTOs);




            }catch (Exception ex)
            {
               throw new Exception(ex.Message);
            }
        }

        // GET: api/Talents/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSkill(int id)
        {
            try
            {
                var skill = await _talentRepository.GetSkillById(id);
                TalentsDTO talentsDTO = new TalentsDTO
                {
                    Skill_ID = skill.Skill_ID,
                    Skill_Name = skill.Skill_Name,
                    Skill_IconUrl = skill.Skill_IconUrl,
                    SkillCategory_ID = skill.SkillCategories.SkillCategory_ID
                };

                return Ok(talentsDTO);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // POST: api/Talents
        [HttpPost]
        public async Task<IActionResult> CreateSkill([FromBody] TalentsDTO talentsDTO)
        {
            try
            {
                var existingTalents = await _talentRepository.GetSkills();
                if (existingTalents.Any(t => t.Skill_Name == talentsDTO.Skill_Name))
                {
                    return BadRequest("Skill already exists");
                }

                var category = await _talentCategoryRepository.GetSkillCategory(talentsDTO.SkillCategory_ID);
                if (category == null)
                {
                    return BadRequest("Category does not exist");
                }

                var skill = new Skills
                {
                    Skill_Name = talentsDTO.Skill_Name,
                    Skill_IconUrl = talentsDTO.Skill_IconUrl,
                    SkillCategory_ID = talentsDTO.SkillCategory_ID
                };

                await _talentRepository.AddSkill(skill);

                var returnDto = new TalentsDTO
                {
                    Skill_ID = skill.Skill_ID,
                    Skill_Name = skill.Skill_Name,
                    Skill_IconUrl = skill.Skill_IconUrl,
                    SkillCategory_ID = skill.SkillCategory_ID
                };

                return Ok(returnDto);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/Talents/5

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTalent(int id, [FromBody] TalentsDTO talentsDTO)
        {
            try
            {
                if(talentsDTO == null || id == 0)
                {
                    return BadRequest("Invalid request");
                }

                var skill = await _talentRepository.GetSkillById(id);
                if (skill == null)
                {
                    return NotFound("Talent not found");
                }

                var category = await _talentCategoryRepository.GetSkillCategory(talentsDTO.SkillCategory_ID);
                if (category == null)
                {
                    return BadRequest("Category does not exist");
                }

                skill.Skill_Name = talentsDTO.Skill_Name;
                skill.Skill_IconUrl = talentsDTO.Skill_IconUrl;
                skill.SkillCategory_ID = talentsDTO.SkillCategory_ID;

                await _talentRepository.UpdateSkill(skill);

                var returnDto = new TalentsDTO
                {
                    Skill_ID = skill.Skill_ID,
                    Skill_Name = skill.Skill_Name,
                    Skill_IconUrl = skill.Skill_IconUrl,
                    SkillCategory_ID = skill.SkillCategory_ID
                };

                return Ok(returnDto);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // DELETE: api/Talents/5

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkill(int id)
        {
            try
            {
                var skill = await _talentRepository.GetSkillById(id);
                if (skill == null)
                {
                    return NotFound("Skill not found");
                }

                await _talentRepository.DeleteSkill(skill);

                return Ok("Skill deleted");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/Talents/talent/userid
        [HttpGet("talent/{userid}")]
        public async Task<IActionResult> GetSkillsOfUser(string userid)
        {
            try
            {
                var skills = await _talentRepository.GetSkillsOfUser(userid);
                if( skills == null || !skills.Any())
                {
                    return NotFound("Skills not found for the user.");
                }
                IEnumerable<TalentsDTO> talentsDTOs = skills.Select(s => new TalentsDTO
                {
                    Skill_ID = s.Skill_ID,
                    Skill_Name = s.Skill_Name,
                    Skill_IconUrl = s.Skill_IconUrl,
                    SkillCategory_ID = s.SkillCategories.SkillCategory_ID
                });
                return Ok(talentsDTOs);

            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // POST: api/Talents/talent/userid
        [HttpPost("talent/{userid}")]
        public async Task<IActionResult> AddSkillToUser(string userid, [FromBody] TalentsModelDTO model)
        {
            if (model == null || model.talentItemsForUser == null || !model.talentItemsForUser.Any())
            {
                return BadRequest("Model is null or empty");
            }

            List<TalentsIDDTO> talentUsers = new List<TalentsIDDTO>();
            foreach (var talent in model.talentItemsForUser)
            {
                var skill = await _talentRepository.GetSkillById(talent.Skill_ID);
                if (skill == null)
                {
                    return NotFound("Skill not found");
                }
                var user = await _talentRepository.AddSkillToUser(talent.Skill_ID, userid);
                if (user == null)
                {
                    return NotFound("User not found");
                }
                talentUsers.Add(new TalentsIDDTO
                {
                    Skill_ID = skill.Skill_ID,
                });
            }

            foreach (var item in talentUsers)
            {
                var skillitem = await _talentRepository.AddSkillToUser(item.Skill_ID, userid);
                if (skillitem == null)
                {
                    return BadRequest("Skill not found");
                }
            }

            var returndto = new
            {
                userid,
                talentUsers
            };

            return Ok(returndto);
        }

        // DELETE: api/Talents/delete/userid
        [HttpDelete("delete/{userid}")]
        public async Task<IActionResult> RemoveSkillFromUser(string userid, [FromBody] TalentsIDDTO talentsDTO)
        {
            if (talentsDTO.Skill_ID == 0)
            {
                return BadRequest("Model is null or empty");
            }

            var skill = await _talentRepository.RemoveSkillFromUser(talentsDTO.Skill_ID, userid);

            if (skill == null)
            {
                return NotFound("Skill not found");
            }

            return Ok("Skill removed");
        }



    }
}
