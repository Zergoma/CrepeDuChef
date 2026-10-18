using CrepeDuChef.Domain.Entities;

namespace CrepeDuChef.Application.Interfaces
{
    public interface ICrepePartyRepository
    {
        Task AddCrepePartyAsync(CrepesParty crepeParty);
        Task<List<CrepesParty>> GetAllCrepePartiesAsync();
    }
}