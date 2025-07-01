using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Dtos.Role;
using AdminDashboard.src.Dtos.User;

namespace AdminDashboard.src.Abstraction
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAllRolesAsync();
        Task<UserDto> AssignRoleToUserAsync(Guid userId, Guid roleId);
        Task<RoleDto> CreateRoleAsync(RoleCreateDto role);
    }
}