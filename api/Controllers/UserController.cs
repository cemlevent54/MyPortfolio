using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using api.DTOs.Account;
using api.DTOs.Users;
using api.Services;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // ctor
        private readonly UserManager<AppUsers> _userManager;
        private readonly ILogger<UserController> _logger;
        private readonly SignInManager<AppUsers> _signInManager;
        private readonly IUserInterface _userRepository;
        private readonly IUserService _userService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPhotoService _photoService;
        public UserController(UserManager<AppUsers> userManager, ILogger<UserController> logger, 
            SignInManager<AppUsers> appUsers, IUserInterface userRepository
            ,IUserService userService, RoleManager<IdentityRole> roleManager,
            IPhotoService photoService)
        {
            _userManager = userManager;
            _logger = logger;
            _signInManager = appUsers;
            _userRepository = userRepository;
            _userService = userService;
            _roleManager = roleManager;
            _photoService = photoService;
        }
        
        
        // create user


        // get all users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepository.GetUsers();
            var usersDto = new List<UsersDTO>();

            // Use a loop to process all users asynchronously
            foreach (var user in users)
            {
                // Await the GetUserRole method to fetch the role asynchronously
                var userRole = await _userService.GetUserRole(user.Id);

                usersDto.Add(new UsersDTO()
                {
                    id = user.Id,
                    Email = user.Email,
                    userName = user.UserName,
                    user_Name = user.User_Name,
                    user_Surname = user.User_Surname,
                    user_Email = user.User_Email,
                    user_Password = user.User_Password,
                    user_PhoneNumber = user.User_PhoneNumber,
                    user_About = user.User_About,
                    user_BirthDate = user.User_BirthDate,
                    user_RegisteredAt = user.User_RegisteredAt,
                    user_PhotoUrl = user.User_PhotoUrl,
                    user_State = (bool)user.User_State,
                    user_LivingCity = user.User_LivingCity,
                    user_CvUrl = user.User_CvUrl,
                    user_Job = user.User_Job,
                    userRole = userRole // Assign the fetched role here
                });
            }

            return Ok(usersDto);
        }

        // get user by username
        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            // first, get user id, and get by id
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound();
            }
            var userinformations = _userRepository.GetUserById(user);
            UserInformationsDTO userDto = new UserInformationsDTO()
            {
                id = userinformations.Result.Id,
                user_Name = userinformations.Result.User_Name,
                user_Surname = userinformations.Result.User_Surname,
                user_Email = userinformations.Result.User_Email,
                user_PhoneNumber = userinformations.Result.User_PhoneNumber,
                userName = userinformations.Result.UserName,
                user_Role ="user",
                user_BirthDate = userinformations.Result.User_BirthDate,
                user_RegisteredAt = userinformations.Result.User_RegisteredAt,
                user_PhotoUrl = userinformations.Result.User_PhotoUrl,
                user_CvUrl = userinformations.Result.User_CvUrl,
                user_LivingCity = userinformations.Result.User_LivingCity,
                user_Job = userinformations.Result.User_Job,

            };

            // get user role
            var roles = await _userManager.GetRolesAsync(user);
            userDto.user_Role = roles.FirstOrDefault(); // Set the role from the user's assigned roles
            




            return Ok(userDto);
        }


        // create user
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] AddUserDTO addUserDto)
        {
            if (addUserDto == null || !ModelState.IsValid)
            {
                return BadRequest("Geçersiz kullanıcı verisi.");
            }

            var user = new AppUsers
            {
                Email = addUserDto.User_Email,
                UserName = addUserDto.User_Username,
                User_Name = addUserDto.User_Name,
                User_Surname = addUserDto.User_Surname,
                User_BirthDate = addUserDto.User_BirthDate,
                User_PhoneNumber = addUserDto.User_PhoneNumber,
                User_Job = addUserDto.User_Job,
                User_LivingCity = addUserDto.User_LivingCity,
                User_CvUrl = addUserDto.User_CvUrl,
                User_About = addUserDto.User_About,
                User_PhotoUrl = addUserDto.User_PhotoUrl,
                User_RegisteredAt = DateTime.Now,
                User_State = true
            };
            
            user.User_Password = addUserDto.User_Password;
            user.User_Email = addUserDto.User_Email;

            // Kullanıcı oluştur
            var createResult = await _userManager.CreateAsync(user, addUserDto.User_Password);
            if (!createResult.Succeeded)
            {
                return BadRequest("Kullanıcı oluşturulamadı.");
            }

            // Rol ataması
            if (!string.IsNullOrEmpty(addUserDto.User_Role))
            {
                var roleExists = await _roleManager.RoleExistsAsync(addUserDto.User_Role);
                if (!roleExists)
                {
                    return BadRequest($"Belirtilen rol ({addUserDto.User_Role}) sistemde mevcut değil.");
                }

                var roleResult = await _userManager.AddToRoleAsync(user, addUserDto.User_Role);
                if (!roleResult.Succeeded)
                {
                    return BadRequest("Rol atanırken hata oluştu.");
                }
            }

            return Ok(new
            {
                id = user.Id,
                addUserDto
            }
            );
        }



        // update user

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] EditUserDTO editUserDTO)
        {
            if (string.IsNullOrEmpty(id) || editUserDTO == null)
            {
                return BadRequest("Geçersiz kullanıcı ID veya veri.");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return BadRequest("Kullanıcı bulunamadı.");
            }


            // Kullanıcı bilgilerini güncelle
            user.UserName = editUserDTO.User_Username;
            user.Email = editUserDTO.User_Email;
            user.User_Name = editUserDTO.User_Name;
            user.User_Surname = editUserDTO.User_Surname;
            //user.User_BirthDate = editUserDTO.User_BirthDate;
            user.User_PhoneNumber = editUserDTO.User_PhoneNumber;
            user.User_Job = editUserDTO.User_Job;
            user.User_CvUrl = editUserDTO.User_CvUrl;
            user.User_About = editUserDTO.User_About;
            user.User_PhotoUrl = editUserDTO.User_PhotoUrl;
            user.User_LivingCity = editUserDTO.User_LivingCity;

            var resultUpdate = await _userManager.UpdateAsync(user);
            if (!resultUpdate.Succeeded)
            {
                return BadRequest("Kullanıcı güncellenemedi.");
            }

            

            // Kullanıcının önceki rolünü kaldır
            var existingRoles = await _userManager.GetRolesAsync(user);
            if (existingRoles.Any())
            {
                await _userManager.RemoveFromRolesAsync(user, existingRoles);
            }

            // Yeni rolü eklemeden önce sistemde olup olmadığını kontrol et
            if (!string.IsNullOrEmpty(editUserDTO.User_Role))
            {
                var roleExists = await _roleManager.RoleExistsAsync(editUserDTO.User_Role);
                if (!roleExists)
                {
                    return BadRequest($"Belirtilen rol ({editUserDTO.User_Role}) sistemde mevcut değil.");
                }

                var resultRole = await _userManager.AddToRoleAsync(user, editUserDTO.User_Role);
                if (!resultRole.Succeeded)
                {
                    return BadRequest("Rol güncellenemedi.");
                }
            }

            return Ok(new
            {
                id = user.Id,
                editUserDTO
            });
        }



        // delete user by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok("User Deleted.");
            }
            return BadRequest("User can't deleted.");

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

                var photoUrl = await _photoService.SavePhotoAsync(file, "profile_pictures");

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





        // delete user

    }
}
