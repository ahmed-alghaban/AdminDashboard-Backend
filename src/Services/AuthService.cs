using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Abstraction;
using AdminDashboard.src.Configs;
using AdminDashboard.src.Dtos.Auth;
using AdminDashboard.src.Utilities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace AdminDashboard.src.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly GenerateToken _generateToken;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(AppDbContext context, IMapper mapper, GenerateToken generateToken, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _generateToken = generateToken;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<string> LoginAsync(UserLoginDto userLoginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userLoginDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.PasswordHash))
            {
                throw new ArgumentException("Email or Password is incorrect");
            }
            var token = _generateToken.GenerateJwtToken(user).ToString();
            return token;
        }

    }
}