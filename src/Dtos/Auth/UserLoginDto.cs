using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.src.Dtos.Auth
{
    public class UserLoginDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}