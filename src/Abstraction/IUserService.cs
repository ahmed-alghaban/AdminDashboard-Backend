using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Configs;
using AdminDashboard.src.Dtos.User;
namespace AdminDashboard.src.Abstraction
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(Guid id);
        Task<UserDto> CreateUserAsync(UserCreateDto user);
        Task<UserDto> UpdateUserAsync(Guid id, UserUpdateDto user);
        Task<bool> DeleteUserAsync(Guid id);
        Task<bool> ChangeUserStatusAsync(Guid id, UserStatus status);
    }
}