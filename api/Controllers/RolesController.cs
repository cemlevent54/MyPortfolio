using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using api.DTOs;
using api.DTOs.Roles;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = _roleManager.Roles.ToList();

            if (roles == null)
            {
                return NotFound();
            }

            // return Id and Name

            return Ok(roles);

        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRolesDTO role)
        {
            if (string.IsNullOrEmpty(role.roleName))
            {
                return BadRequest("Role name is required.");
            }

            var roleExists = await _roleManager.RoleExistsAsync(role.roleName);
            if (roleExists)
            {
                return BadRequest("Role already exists");

            }
            var identityRole = new IdentityRole(role.roleName);

            var result = await _roleManager.CreateAsync(new IdentityRole(role.roleName));
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { id = identityRole.Id, name = identityRole.Name });
        }

        [HttpPut("{roleId}")]
        public async Task<IActionResult> UpdateRole(string roleId, [FromBody] UpdateRolesDTO updaterole)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return NotFound("Role not found");
            }

            role.Name = updaterole.roleName;

            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { id = role.Id, name = role.Name });
        }

        [HttpDelete("{roleId}")]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return NotFound("Role not found");
            }

            var result = await _roleManager.DeleteAsync(role);

            if(!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Role deleted successfully");

        }



    }
}
