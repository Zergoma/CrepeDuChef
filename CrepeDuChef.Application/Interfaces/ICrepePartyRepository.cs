using CrepeDuChef.Domain.Entities;

namespace CrepeDuChef.Application.Interfaces
{
    public interface ICrepePartyRepository
    {
        Task AddAsync(CrepeParty crepeParty);
        Task<List<CrepeParty>> GetAllAsync();
        Task<List<CrepeParty>> GetBySessionAsync(int sessionNumber);
        Task<int> GetLastSessionNumberAsync();
    }
}
