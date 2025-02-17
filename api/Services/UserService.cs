using api.Controllers;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;

namespace api.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUsers> _userManager;
        private readonly ILogger<UserService> _logger;
        private readonly SignInManager<AppUsers> _signInManager;
        public UserService(UserManager<AppUsers> userManager,ILogger<UserService> userService, SignInManager<AppUsers> signInManager)
        {
            _userManager = userManager;
            _logger = userService;
            _signInManager = signInManager;
        }

        // get user role with id
        public async Task<string?> GetUserRole(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning($"User with ID {id} not found.");
                return null; // Kullanıcı bulunamazsa null dön
            }

            var roles = await _userManager.GetRolesAsync(user);
            if (roles == null || roles.Count == 0)
            {
                _logger.LogWarning($"User with ID {id} has no assigned roles.");
                return null; // Kullanıcının rolü yoksa null dön
            }

            return roles[0]; // İlk rolü döndür
        }


    }
}
