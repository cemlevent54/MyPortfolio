using api.DTOs.Account;
using api.Interfaces;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUsers> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<AppUsers> _signInManager;
        private readonly IPhotoService _photoService;

        public AccountController(UserManager<AppUsers> userManager, ILogger<AccountController> logger,
            SignInManager<AppUsers> appUsers, IPhotoService photoService)
        {
            _userManager = userManager;
            _logger = logger;
            _signInManager = appUsers;
            _photoService = photoService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            _logger.LogInformation("Register endpoint called with data: {@RegisterDTO}", registerDto);

            try
            {
                // Model state kontrolü
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for RegisterDTO.");
                    return BadRequest(ModelState);
                }

                // Kullanıcı adı ve e-posta mevcut mu kontrolü
                var existingUser = await _userManager.FindByNameAsync(registerDto.User_Username);
                if (existingUser != null)
                {
                    _logger.LogWarning("The username {UserName} is already taken.", registerDto.User_Username);
                    return BadRequest("Bu kullanıcı adı zaten kullanılıyor.");
                }

                var existingEmail = await _userManager.FindByEmailAsync(registerDto.User_Email);
                if (existingEmail != null)
                {
                    _logger.LogWarning("The email {Email} is already registered.", registerDto.User_Email);
                    return BadRequest("Bu e-posta zaten kullanılıyor.");
                }

                // Yeni Kullanıcı Oluşturma
                var appUser = new AppUsers
                {
                    UserName = registerDto.User_Username,
                    Email = registerDto.User_Email,
                    User_Name = registerDto.User_Name,
                    User_Surname = registerDto.User_Surname,
                    User_PhoneNumber = registerDto.User_PhoneNumber,
                    User_About = registerDto.User_About,
                    User_BirthDate = registerDto.User_BirthDate,
                    User_RegisteredAt = DateTime.UtcNow,
                    User_PhotoUrl = registerDto.User_PhotoUrl,
                    User_LivingCity = registerDto.User_LivingCity,
                    User_CvUrl = registerDto.User_CvUrl,
                    User_State = true
                };
                // do birthdate and registeredat DD/MM/YYYY
                // first convert to string
                var birthdate = registerDto.User_BirthDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                var registeredat = DateTime.UtcNow.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                // then convert to datetime
                appUser.User_BirthDate = DateTime.ParseExact(birthdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                appUser.User_RegisteredAt = DateTime.ParseExact(registeredat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                appUser.User_Email = registerDto.User_Email;
                appUser.User_Password = registerDto.User_Password; // şifreyi görmek için

                _logger.LogInformation("Creating user with username: {UserName}", registerDto.User_Username);

                // Kullanıcıyı oluştur
                var result = await _userManager.CreateAsync(appUser, registerDto.User_Password);

                if (!result.Succeeded)
                {
                    _logger.LogError("Failed to create user with username {UserName}. Errors: {Errors}",
                        registerDto.User_Username, string.Join(", ", result.Errors.Select(e => e.Description)));
                    return StatusCode(500, new { Message = "Kullanıcı oluşturulurken bir hata oluştu.", Errors = result.Errors });
                }

                _logger.LogInformation("User created successfully. Assigning role 'User' to: {UserName}", registerDto.User_Username);

                // Kullanıcıya "User" rolünü ata
                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");

                if (!roleResult.Succeeded)
                {
                    _logger.LogError("Failed to assign role 'User' to {UserName}. Errors: {Errors}",
                        registerDto.User_Username, string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                    return StatusCode(500, new { Message = "Rol eklenirken bir hata oluştu.", Errors = roleResult.Errors });
                }

                _logger.LogInformation("Role 'User' assigned successfully to {UserName}", registerDto.User_Username);

                // Kullanıcıyı başarıyla oluşturduktan sonra, session'da kullanıcı bilgilerini sakla
                await _signInManager.SignInAsync(appUser, isPersistent: false);
                HttpContext.Session.SetString("UserId", appUser.Id);

                // Kullanıcı başarıyla oluşturuldu, session'da kullanıcı bilgileri saklanarak geri dönülür
                return Ok(new NewUserDTO
                {
                    UserName = appUser.UserName,
                    Email = appUser.Email
                });
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "An exception occurred while registering user: {Message}", ex.Message);
                return StatusCode(500, new { Message = "Beklenmeyen bir hata oluştu.", Details = ex.Message });
            }
        }





        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                HttpContext.Session.SetString("UserId", user.Id);
                return Ok("Login successful");
            }
            return Unauthorized();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Session.Clear();
            return Ok("Logout successful");
        }

        [Authorize]
        [HttpGet("GetUserInfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            try
            {
                // Login olmuş kullanıcının username'ini alıyoruz
                var username = User.Identity.Name;

                // Kullanıcıyı veritabanından alıyoruz
                var user = await _userManager.FindByNameAsync(username);

                if (user == null)
                {
                    return NotFound("Kullanıcı bulunamadı.");
                }

                // Kullanıcı bilgilerini döndürüyoruz
                var userInfo = new
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    FullName = $"{user.User_Name} {user.User_Surname}",
                    PhoneNumber = user.User_PhoneNumber,
                    About = user.User_About,
                    BirthDate = user.User_BirthDate,
                    LivingCity = user.User_LivingCity,
                    PhotoUrl = user.User_PhotoUrl,
                    CvUrl = user.User_CvUrl,
                    RegisteredAt = user.User_RegisteredAt
                };

                return Ok(userInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı bilgileri alınırken hata oluştu.");
                return StatusCode(500, new { Message = "Bir hata oluştu.", Details = ex.Message });
            }
        }


        

        // upload-photo endpoint
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


    }
}
