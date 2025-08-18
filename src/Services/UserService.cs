using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Configs;
using AdminDashboard.src.Abstraction;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using AdminDashboard.src.Dtos.User;
using AdminDashboard.src.Entities;
using AdminDashboard.src.Configs.Exceptions;
using AdminDashboard.src.Utilities;

namespace AdminDashboard.src.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginationResult<UserDto>> GetAllUsersAsync(int pageNumber = 1, int pageSize = 10, string? searchTerm = null)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTermLower = searchTerm.ToLower();
                query = query.Where(u => u.FirstName.ToLower().Contains(searchTermLower) ||
                                       u.LastName.ToLower().Contains(searchTermLower) ||
                                       (u.FirstName.ToLower() + " " + u.LastName.ToLower()).Contains(searchTermLower));
            }

            var users = await query.Include(u => u.Role).ToListAsync();
            var mappedUsers = _mapper.Map<List<UserDto>>(users);
            return await PaginationSearch.PaginationAsync(mappedUsers, pageNumber, pageSize);
        }

        public async Task<UserDto> GetUserByIdAsync(Guid id)
        {
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.UserId == id) ?? throw new Exception("User not found");
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> CreateUserAsync(UserCreateDto user)
        {
            await _context.EnsureUniqueAsync<User>(u => u.Email == user.Email || u.PhoneNumber == user.PhoneNumber, "User already exists");

            var newUser = _mapper.Map<User>(user);
            newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDto>(newUser);
        }

        public async Task<UserDto> UpdateUserAsync(Guid id, UserUpdateDto user)
        {
            var existingUser = await _context.Users.FindAsync(id) ?? throw new Exception("User not found");

            // Check for duplicate email or phone number, excluding the current user
            if (!string.IsNullOrWhiteSpace(user.Email) && user.Email != existingUser.Email)
            {
                var emailExists = await _context.Users.AnyAsync(u => u.Email == user.Email && u.UserId != id);
                if (emailExists)
                {
                    throw new Exception("Email already exists");
                }
            }

            if (!string.IsNullOrWhiteSpace(user.PhoneNumber) && user.PhoneNumber != existingUser.PhoneNumber)
            {
                var phoneExists = await _context.Users.AnyAsync(u => u.PhoneNumber == user.PhoneNumber && u.UserId != id);
                if (phoneExists)
                {
                    throw new Exception("Phone number already exists");
                }
            }

            // Validate RoleId if provided
            if (user.RoleId.HasValue)
            {
                var roleExists = await _context.Roles.AnyAsync(r => r.RoleId == user.RoleId.Value);
                if (!roleExists)
                {
                    throw new Exception("Role not found");
                }
            }

            // Only update password if a new one is provided
            if (!string.IsNullOrWhiteSpace(user.PasswordHash))
            {
                existingUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            }

            // Update other properties using AutoMapper
            _mapper.Map(user, existingUser);
            _context.Update(existingUser);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDto>(existingUser);
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id) ?? throw new Exception("User not found");
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeUserStatusAsync(Guid id, UserStatus status)
        {
            var user = await _context.Users.FindAsync(id) ?? throw new Exception("User not found");
            user.Status = status;
            _context.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}