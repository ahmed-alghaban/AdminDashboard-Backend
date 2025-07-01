using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.src.Dtos.Role
{
    public class RoleCreateDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}