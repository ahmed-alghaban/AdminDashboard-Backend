using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Abstraction;
using AdminDashboard.src.Dtos.Role;
using AdminDashboard.src.Dtos.User;
using AdminDashboard.src.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashboard.src.Controllers
{
    [ApiController]
    [Route("api/v1/roles")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            try
            {
                var roles = await _roleService.GetAllRolesAsync();
                var result = new ApiResult<IEnumerable<RoleDto>>(roles, true, "Roles fetched successfully");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] RoleCreateDto role)
        {
            try
            {
                var newRole = await _roleService.CreateRoleAsync(role);
                var result = new ApiResult<RoleDto>(newRole, true, "Role created successfully");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRoleToUser([FromBody] AssignRoleToUserDto assignRoleToUser)
        {
            try
            {
                var user = await _roleService.AssignRoleToUserAsync(assignRoleToUser);
                var result = new ApiResult<UserDto>(user, true, "Role assigned to user successfully");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}