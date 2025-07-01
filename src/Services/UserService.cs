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

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetUserByIdAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id) ?? throw new Exception("User not found");
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> CreateUserAsync(UserCreateDto user)
        {
            var newUser = _mapper.Map<User>(user);
            newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDto>(newUser);
        }

        public async Task<UserDto> UpdateUserAsync(Guid id, UserUpdateDto user)
        {
            var existingUser = await _context.Users.FindAsync(id) ?? throw new Exception("User not found");
            existingUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash) ?? existingUser.PasswordHash;
            _context.Update(_mapper.Map(user, existingUser));
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