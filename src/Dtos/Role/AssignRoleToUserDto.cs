using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.src.Dtos.Role
{
    public class AssignRoleToUserDto
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}