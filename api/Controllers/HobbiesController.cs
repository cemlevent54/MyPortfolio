using api.DTOs.Hobby;
using api.DTOs.Hobby.HobbyUser;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HobbiesController : ControllerBase
    {
        private readonly IHobbyInterface _hobbyrepository;

        public HobbiesController(IHobbyInterface hobbyrepository)
        {
            _hobbyrepository = hobbyrepository;
        }

        // POST: api/Hobbies
        [HttpPost]
        public async Task<IActionResult> CreateHobby([FromBody] HobbyDTO hobbyDTO)
        {
            var hobby = new Hobbies
            {

                Hobby_Name = hobbyDTO.Hobby_Name,
                Hobby_IconUrl = hobbyDTO.Hobby_IconUrl
            };

            await _hobbyrepository.AddHobby(hobby);

            var returndto = new HobbyDTO
            {
                Hobby_ID = hobby.Hobby_ID,
                Hobby_Name = hobby.Hobby_Name,
                Hobby_IconUrl = hobby.Hobby_IconUrl
            };


            return Ok(returndto);
        }

        // GET: api/Hobbies
        [HttpGet]
        public async Task<IActionResult> GetHobbies()
        {
            var hobbies = await _hobbyrepository.GetHobbies();
            IEnumerable<HobbyDTO> hobbyDTOs = hobbies.Select(h => new HobbyDTO
            {
                Hobby_ID = h.Hobby_ID,
                Hobby_Name = h.Hobby_Name,
                Hobby_IconUrl = h.Hobby_IconUrl
            });
            return Ok(hobbyDTOs);
        }

        // GET: api/Hobbies/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHobby(int id)
        {
            var hobby = await _hobbyrepository.GetHobby(id);
            if (hobby == null)
            {
                return NotFound();
            }
            var hobbyDTO = new HobbyDTO
            {
                Hobby_ID = hobby.Hobby_ID,
                Hobby_Name = hobby.Hobby_Name,
                Hobby_IconUrl = hobby.Hobby_IconUrl
            };
            return Ok(hobbyDTO);
        }

        // get hobbies of an user
        // GET: api/hobbies/hobbie/userid
        [HttpGet("hobbie/{id}")]
        public async Task<IActionResult> GetHobbiesOfUser(string id)
        {
            var hobbies = await _hobbyrepository.GetHobbiesOfUser(id);
            if (hobbies == null)
            {
                return NotFound();
            }
            IEnumerable<HobbyDTO> hobbyDTOs = hobbies.Select(h => new HobbyDTO
            {
                Hobby_ID = h.Hobby_ID,
                Hobby_Name = h.Hobby_Name,
                Hobby_IconUrl = h.Hobby_IconUrl
            });
            return Ok(hobbyDTOs);
        }

        // add user to hobby
        // POST: api/hobbies/hobbie/userid
        [HttpPost("{user_id}")]
        public async Task<IActionResult> AddUserToHobby(string user_id, [FromBody] HobbyModelDTO model)
        {
            if (model == null || model.HobbyItemsForUser == null || !model.HobbyItemsForUser.Any())
            {
                return BadRequest(" Model is null or empty.");
            }

            // model degiskeninden verileri listeye atmamız gerekiyor
            List<HobbyIDDTO> hobbyUsers = new List<HobbyIDDTO>();
            foreach (var item in model.HobbyItemsForUser)
            {
                hobbyUsers.Add(new HobbyIDDTO
                {
                    Hobby_ID = item.Hobby_ID,
                });
            }

            foreach (var item in hobbyUsers)
            {
                var hobby = await _hobbyrepository.AddUserToHobby(item.Hobby_ID, user_id);
                if (hobby == null)
                {
                    return NotFound();
                }


            }

            var returndto = new
            {
                user_id,
                hobbyUsers
            };


            return Ok(returndto);


            

        }

        // remove user from hobby
        // DELETE: api/hobbies/delete/userid
        [HttpDelete("delete/{user_id}")]
        public async Task<IActionResult> RemoveUserFromHobby(string user_id, [FromBody] HobbyIDDTO hobbyiddto)
        {
            if (hobbyiddto.Hobby_ID == 0)
            {
                return BadRequest("Hobby id is null or empty.");
            }

            var hobby = await _hobbyrepository.RemoveUserFromHobby(hobbyiddto.Hobby_ID, user_id);
            if (hobby == null)
            {
                return NotFound();
            }

            var returndto = new
            {
                user_id,
                hobbyiddto
            };

            return Ok(returndto);

        }


        // PUT: api/Hobbies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHobby(int id,[FromBody] HobbyDTO hobbyDTO)
        {
            
            var hobby = await _hobbyrepository.GetHobby(id);
            if (hobby == null)
            {
                return NotFound("Not found.");
            }
            //hobby.Hobby_ID = id;
            hobby.Hobby_Name = hobbyDTO.Hobby_Name;
            hobby.Hobby_IconUrl = hobbyDTO.Hobby_IconUrl;

            await _hobbyrepository.UpdateHobby(hobby);


            var returndto = new HobbyDTO
            {
                Hobby_ID = hobby.Hobby_ID,
                Hobby_Name = hobby.Hobby_Name,
                Hobby_IconUrl = hobby.Hobby_IconUrl
            };

            return Ok(returndto);

        }

        // DELETE: api/Hobbies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHobby(int id)
        {
            var hobby = await _hobbyrepository.GetHobby(id);
            if (hobby == null)
            {
                return NotFound();
            }
            await _hobbyrepository.DeleteHobby(hobby);
            return Ok("Silindi");
        }

    }
}
