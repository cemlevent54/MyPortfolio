using api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.DTOs.Languages;
using api.DTOs.Languages.LanguageUser;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly ILanguageInterface _languagerepository;
        public LanguagesController(ILanguageInterface languageRepository)
        {
            _languagerepository = languageRepository;
        }

        // POST: api/Languages
        [HttpPost]
        public async Task<IActionResult> CreateLanguage([FromBody] LanguageDTO languageDTO)
        {
            var language = new ForeignLanguages
            {
                Language_Name = languageDTO.Language_Name,
            };

            await _languagerepository.AddLanguage(language);

            var returndto = new LanguageDTO
            {
                Language_ID = language.Language_ID,
                Language_Name = language.Language_Name,
            };

            return Ok(returndto);
        }

        // GET: api/Languages
        [HttpGet]
        public async Task<IActionResult> GetLanguages()
        {
            var languages = await _languagerepository.GetLanguages();
            IEnumerable<LanguageDTO> languageDTOs = languages.Select(l => new LanguageDTO
            {
                Language_ID = l.Language_ID,
                Language_Name = l.Language_Name,
            });
            return Ok(languageDTOs);
        }

        // GET: api/Languages/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLanguage(int id)
        {
            var language = await _languagerepository.GetLanguageById(id);
            if (language == null)
            {
                return NotFound("Not found");
            }
            var languageDTO = new LanguageDTO
            {
                Language_ID = language.Language_ID,
                Language_Name = language.Language_Name,
            };
            return Ok(languageDTO);
        }

        // GET: api/Languages/language/userid
        [HttpGet("language/{userid}")]
        public async Task<IActionResult> GetLanguagesOfUser(string userid)
        {
            var languages = await _languagerepository.GetLanguagesOfUser(userid);
            if(languages == null)
            {
                return NotFound("Not found");
            }

            IEnumerable<LanguageDTO> languageDTOs = languages.Select(l => new LanguageDTO
            {
                Language_ID = l.Language_ID,
                Language_Name = l.Language_Name,
            });
            return Ok(languageDTOs);
        }

        // POST: api/Languages/language/userid
        [HttpPost("language/{userid}")]
        public async Task<IActionResult> AddUserToLanguage(string userid, [FromBody] LanguageModelDTO model)
        {
            if(model == null || model.LanguageItemsForUser == null || !model.LanguageItemsForUser.Any())
            {
                return BadRequest("Model is null or empty");
            }

            List<LanguageIDDTO> languageUsers = new List<LanguageIDDTO>();
            foreach(var item in model.LanguageItemsForUser)
            {
                var languageUser = new LanguageIDDTO
                {
                    Language_ID = item.Language_ID,
                };
                languageUsers.Add(languageUser);
            }
            
            foreach(var item in languageUsers)
            {
                var lang = await _languagerepository.AddUserToLanguage(item.Language_ID,userid);
                if(lang == null)
                {
                    return BadRequest("Language not found");
                }
            }

            var returndto = new
            {
                userid,
                languageUsers
            };

            return Ok(returndto);
        }

        // DELETE : api/languages/delete/userid
        [HttpDelete("delete/{userid}")]
        public async Task<IActionResult> RemoveUserFromLanguage(string userid, [FromBody] LanguageIDDTO langiddto)
        {
            if (langiddto.Language_ID == 0)
            {
                return BadRequest("Model is null or empty");
            }

            var lang = await _languagerepository.RemoveUserFromLanguage(langiddto.Language_ID, userid);

            if (lang == null)
            {
                return NotFound("Language not found");
            }

            var returndto = new
            {
                userid,
                langiddto
            };

            return Ok(returndto);
        }

        // PUT: api/Languages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLanguage(int id, [FromBody] LanguageDTO languageDTO)
        {
            var language = await _languagerepository.GetLanguageById(id);
            if (language == null)
            {
                return NotFound("Language not found");
            }

            language.Language_Name = languageDTO.Language_Name;

            await _languagerepository.UpdateLanguage(language);

            var returndto = new LanguageDTO
            {
                Language_ID = language.Language_ID,
                Language_Name = language.Language_Name,
            };

            return Ok(returndto);
        }

        // DELETE: api/Languages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLanguage(int id)
        {
            var language = await _languagerepository.GetLanguageById(id);
            if (language == null)
            {
                return NotFound("Language not found");
            }

            await _languagerepository.DeleteLanguage(language);

            return Ok("Language deleted");
        }
    }
}
