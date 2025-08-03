using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Abstraction;
using AdminDashboard.src.Configs;
using AdminDashboard.src.Dtos.User;
using AdminDashboard.src.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashboard.src.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string searchTerm = null)
        {
            try
            {
                var paginationResult = await _userService.GetAllUsersAsync(pageNumber, pageSize, searchTerm);
                var result = new ApiResult<PaginationResult<UserDto>>(paginationResult, true, "Users fetched successfully");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                var result = new ApiResult<UserDto>(user, true, "User fetched successfully");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateDto user)
        {
            try
            {
                var newUser = await _userService.CreateUserAsync(user);
                var result = new ApiResult<UserDto>(newUser, true, "User created successfully");
                return CreatedAtAction(nameof(GetUserById), new { id = newUser.CreatedAt }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, UserUpdateDto user)
        {
            try
            {
                var updatedUser = await _userService.UpdateUserAsync(id, user);
                var result = new ApiResult<UserDto>(updatedUser, true, "User updated successfully");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ChangeUserStatus(Guid id, UserStatus status)
        {
            try
            {
                var isChanged = await _userService.ChangeUserStatusAsync(id, status);
                var result = new ApiResult<bool>(isChanged, true, "User status changed successfully");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                var isDeleted = await _userService.DeleteUserAsync(id);
                var result = new ApiResult<bool>(isDeleted, true, "User deleted successfully");
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } 
    }
}