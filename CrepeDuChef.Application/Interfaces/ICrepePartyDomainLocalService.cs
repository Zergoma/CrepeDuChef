using CrepeDuChef.Domain.Entities;

namespace CrepeDuChef.Application.Interfaces
{
    public interface ICrepePartyDomainLocalService
    {
        Task<List<CrepesParty>> GetCrepePartiesFromSessionAsync(int SessionNumber);
        Task<int> GetLastSessionNumberAsync();
    }
}