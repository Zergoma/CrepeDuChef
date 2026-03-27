using CrepeDuChef.Common.DTOs;

namespace CrepeDuChef.Application.Interfaces
{
    public interface ICrepePartyRepositoryApplication
    {
        Task<List<UserDto>> GetAllChefsAsync();
        Task<UserDto?> GetChefAsync(int id);
        Task AddChefAsync(UserDto userDto);
        Task UpdateChefAsync(UserDto userDto);

        Task AddCrepePartyAsync(CrepesPartyDto crepePartyDto);
        Task<List<CrepesPartyDto>> GetAllCrepePartiesAsync();
        Task<List<CrepesPartyDto>> GetCrepePartiesFromSessionAsync(int sessionNumber);
        Task<int> GetLastSessionNumberAsync();
    }
}
