using CrepeDuChef.Common.DTOs;
//using CrepeDuChef.EntityModels;

namespace CrepeDuChef.Common.Interfaces
{
    public interface ICrepePartyRepository
    {
        Task AddChefAsync(UserDto user);
        Task UpdateChefAsync(UserDto user);
        Task AddCrepePartyAsync(CrepesPartyDto crepeParty);
        Task CommitAsync();
        //void ExecuteInTransaction(Action<CrepePartyRepository> action);
        Task<List<UserDto>> GetAllChefsAsync();
        Task<UserDto?> GetChefAsync(int id);
        Task<List<CrepesPartyDto>> GetAllCrepePartiesAsync();
        Task<List<CrepesPartyDto>> GetCrepePartiesFromSessionAsync(int SessionNumber);
        Task<int> GetLastSessionNumberAsync();
    }
}