using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AdminDashboard.src.Configs;
using AdminDashboard.src.Abstraction;

namespace AdminDashboard.src.Utilities
{
    public class GenerateToken
    {
        private readonly AppDbContext _appDbContext;
        public GenerateToken(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public string GenerateJwtToken(User user)
        {
            var jwtKey = Environment.GetEnvironmentVariable("JWT__KEY")
            ?? throw new InvalidOperationException("JWT Key is missing in environment variables.");
            var jwtIssuer = Environment.GetEnvironmentVariable("JWT__ISSUER")
            ?? throw new InvalidOperationException("JWT Issuer is missing in environment variables.");
            var jwtAudience = Environment.GetEnvironmentVariable("JWT__AUDIENCE")
            ?? throw new InvalidOperationException("JWT Audience is missing in environment variables.");
            var role = _appDbContext.Roles.Find(user.RoleId) ?? throw new Exception("Role not found");
            var roleName = role.Name;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()), // Often used to store a user ID, which is critical for identifying the user within your system.
                    new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"), // User's name.
                    new Claim(ClaimTypes.Role, roleName),// User's role, determining access level.
                    new Claim(ClaimTypes.Email, user.Email),
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = jwtIssuer,
                Audience = jwtAudience
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}