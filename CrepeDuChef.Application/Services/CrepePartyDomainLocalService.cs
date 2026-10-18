using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Domain.Entities;

namespace CrepeDuChef.Application.Services
{
    public class CrepePartyDomainLocalService : ICrepePartyDomainLocalService
    {
        private readonly ICrepePartyRepository _repo;
        public CrepePartyDomainLocalService(ICrepePartyRepository repo)
        {
            _repo = repo;
        }
        public async Task<List<CrepesParty>> GetCrepePartiesFromSessionAsync(int sessionNumber)
        {
            List<CrepesParty> all =
                await _repo.GetAllCrepePartiesAsync();
            
            return all.Where(p => p.SessionNumber == sessionNumber).ToList();
        }

        public async Task<int> GetLastSessionNumberAsync()
        {
            List<CrepesParty> all =
                await _repo.GetAllCrepePartiesAsync();

            return all.Any() ? all.Max(p => p.SessionNumber) : 0;
        }
    }
}
