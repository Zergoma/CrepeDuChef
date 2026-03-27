using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Common.Interfaces;
using CrepeDuChef.Common.Mappers;
using CrepeDuChef.Common.Models;

namespace CrepeDuChef.Application.Services
{
    public class CrepePartyService : ICrepePartyService
    {
        private readonly ICrepePartyRepositoryApplication _repo;
        private readonly ITradCrepePartyDefault _trad;

        public CrepePartyService(ICrepePartyRepositoryApplication repo, ITradCrepePartyDefault trad)
        {
            _repo = repo;
            this._trad = trad;
        }

        public async Task<IEnumerable<CrepePartySession>> GetSessionsAsync()
        {
            var crepes = await _repo.GetAllCrepePartiesAsync();
            var chefs = await _repo.GetAllChefsAsync();

            var chefsById = chefs.ToDictionary(c => c.Id);

            return crepes
                .GroupBy(cp => cp.SessionNumber)
                .OrderByDescending(cp => cp.Key)
                .Select(group => CrepePartyMapper.ToSession(group, chefsById, _trad));
        }
    }
}
