using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Models.Requests.Chefs;

namespace CrepeDuChef.Application.Interfaces
{
    public interface IChefDataService
    {
        Task<List<UserDto>> GetAllChefsAsync();
        Task<UserDto> AddChefAsync(AddChefClientRequest userAddRequest);
        Task<UserDto?> UpdateChefAsync(Guid userId, UpdateChefClientRequest userUpdateRequest);
    }
}
