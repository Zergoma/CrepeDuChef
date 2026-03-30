using CrepeDuChef.Application.DTOs;

namespace CrepeDuChef.Application.Interfaces
{
    public interface IChefManagementService
    {
        Task<UserDto?> AddUserAsync(UserDtoAdd userDtoAdd);
        Task<List<UserDto>> GetAllUsersAsync();
        Task<UserDto?> UpdateUserAsync(UserDto user, UserDtoUpdate result);
    }
}