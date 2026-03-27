using CrepeDuChef.Domain.Entities;

namespace CrepeDuChef.Application.Interfaces
{
    public interface ICrepePartyRepositoryInfrastructure
    {
        Task AddChefAsync(User user);
        Task UpdateChefAsync(User user);
        Task AddCrepePartyAsync(CrepesParty crepeParty);
        Task<List<User>> GetAllChefsAsync();
        Task<User?> GetChefAsync(int id);
        Task<List<CrepesParty>> GetAllCrepePartiesAsync();
        Task<List<CrepesParty>> GetCrepePartiesFromSessionAsync(int SessionNumber);
        Task<int> GetLastSessionNumberAsync();
    }
}