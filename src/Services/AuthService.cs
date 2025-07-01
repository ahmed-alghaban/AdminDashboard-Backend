using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Abstraction;
using AdminDashboard.src.Configs;

namespace AdminDashboard.src.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        public AuthService(AppDbContext context)
        {
            _context = context;
        }
        public Task<string> LoginAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<string> LogoutAsync()
        {
            throw new NotImplementedException();
        }

        public Task<string> ForgotPasswordAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<string> ResetPasswordAsync(string email, string token, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateProfileAsync(string email, string newPassword, string newEmail)
        {
            throw new NotImplementedException();
        }
    }
}