using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Abstraction;
using AdminDashboard.src.Dtos.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AdminDashboard.src.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            try
            {
                var token = await _authService.LoginAsync(userLoginDto);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("test-admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult TestAdminAuth()
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var isAdmin = User.IsInRole("Admin");

            return Ok(new
            {
                message = "Admin authorization successful!",
                userRole,
                isAdmin,
                roleClaims = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList()
            });
        }

        [HttpGet("test-manager")]
        [Authorize(Roles = "Manager")]
        public IActionResult TestManagerAuth()
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var isManager = User.IsInRole("Manager");

            return Ok(new
            {
                message = "Manager authorization successful!",
                userRole,
                isManager,
                roleClaims = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList()
            });
        }
    }
}