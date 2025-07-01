using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Abstraction;
using AdminDashboard.src.Configs;
using AdminDashboard.src.Dtos.Role;
using AdminDashboard.src.Dtos.User;
using AdminDashboard.src.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AdminDashboard.src.Services
{
    public class RoleService : IRoleService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public RoleService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
        {
            var roles = await _context.Roles.ToListAsync();
            return _mapper.Map<IEnumerable<RoleDto>>(roles);
        }
        public async Task<UserDto> AssignRoleToUserAsync(Guid userId, Guid roleId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.UserId == userId) ?? throw new KeyNotFoundException("User not found");
            var role = await _context.Roles.FirstOrDefaultAsync(role => role.RoleId == roleId) ?? throw new KeyNotFoundException("Role not found");
            user.RoleId = roleId;
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDto>(user);
        }
        public async Task<RoleDto> CreateRoleAsync(RoleCreateDto role)
        {
            var newRole = _mapper.Map<Role>(role);
            await _context.Roles.AddAsync(newRole);
            await _context.SaveChangesAsync();
            return _mapper.Map<RoleDto>(newRole);
        }
    }
}