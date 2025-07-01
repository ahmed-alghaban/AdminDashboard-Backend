using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.src.Abstraction
{
    public interface IAuthService
    {
        Task<string> LoginAsync(string email, string password);
        Task<string> LogoutAsync();
        Task<string> ForgotPasswordAsync(string email);
        Task<string> ResetPasswordAsync(string email, string token, string newPassword);
        Task<string> UpdateProfileAsync(string email, string newPassword, string newEmail);
    }
}