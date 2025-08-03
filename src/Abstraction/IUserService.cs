using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.src.Configs;
using AdminDashboard.src.Dtos.User;
using AdminDashboard.src.Utilities;
namespace AdminDashboard.src.Abstraction
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<PaginationResult<UserDto>> GetAllUsersAsync(int pageNumber = 1, int pageSize = 10);
        Task<UserDto> GetUserByIdAsync(Guid id);
        Task<UserDto> CreateUserAsync(UserCreateDto user);
        Task<UserDto> UpdateUserAsync(Guid id, UserUpdateDto user);
        Task<bool> DeleteUserAsync(Guid id);
        Task<bool> ChangeUserStatusAsync(Guid id, UserStatus status);
    }
}