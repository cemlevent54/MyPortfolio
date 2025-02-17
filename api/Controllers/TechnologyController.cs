using api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.DTOs.Technology;
using api.Models;


namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnologyController : ControllerBase
    {
        private readonly ITechnologyInterface _technologyRepository;
        public TechnologyController(ITechnologyInterface technologyRepo)
        {
            _technologyRepository = technologyRepo;
        }

        // GET: api/technology
        [HttpGet]
        public async Task<IActionResult> GetTechnologies()
        {
            try
            {
                var technologies = await _technologyRepository.GetTechnologies();
                IEnumerable<TechnologyDTO> technologyDTOs = technologies.Select(t => new TechnologyDTO
                {
                    Technology_ID = t.Technology_ID,
                    Technology_Name = t.Technology_Name,
                    Technology_IconUrl = t.Technology_IconUrl
                });

                return Ok(technologyDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET: api/technology/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTechnology(int id)
        {
            try
            {
                var technology = await _technologyRepository.GetTechnology(id);
                if (technology == null)
                {
                    return NotFound();
                }
                var returndto = new TechnologyDTO
                {
                    Technology_ID = technology.Technology_ID,
                    Technology_Name = technology.Technology_Name,
                    Technology_IconUrl = technology.Technology_IconUrl
                };

                return Ok(returndto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST: api/technology
        [HttpPost]
        public async Task<IActionResult> CreateTechnology([FromBody] TechnologyDTO technologyDTO)
        {
            try
            {
                var technology = new Technologies
                {
                    Technology_Name = technologyDTO.Technology_Name,
                    Technology_IconUrl = technologyDTO.Technology_IconUrl
                };

                await _technologyRepository.AddTechnology(technology);

                var returndto = new TechnologyDTO
                {
                    Technology_ID = technology.Technology_ID,
                    Technology_Name = technology.Technology_Name,
                    Technology_IconUrl = technology.Technology_IconUrl
                };



                return Ok(returndto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/technology/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTechnology(int id, [FromBody] TechnologyDTO technologyDTO)
        {
            try
            {
                var technology = await _technologyRepository.GetTechnology(id);
                if (technology == null)
                {
                    return NotFound();
                }

                technology.Technology_Name = technologyDTO.Technology_Name;
                technology.Technology_IconUrl = technologyDTO.Technology_IconUrl;

                await _technologyRepository.UpdateTechnology(technology);

                var returndto = new TechnologyDTO
                {
                    Technology_ID = technology.Technology_ID,
                    Technology_Name = technology.Technology_Name,
                    Technology_IconUrl = technology.Technology_IconUrl
                };

                return Ok(technology);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE: api/technology/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTechnology(int id)
        {
            try
            {
                var technology = await _technologyRepository.GetTechnology(id);
                if (technology == null)
                {
                    return NotFound("Silindi");
                }
                await _technologyRepository.DeleteTechnology(technology);
                return Ok("Silindi.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
