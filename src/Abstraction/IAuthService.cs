using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Dtos.Auth;
namespace AdminDashboard.src.Abstraction
{
    public interface IAuthService
    {
        Task<string> LoginAsync(UserLoginDto userLoginDto);
        
    }
}