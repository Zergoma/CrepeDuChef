using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Application.Mappers;
using CrepeDuChef.Application.Models;
using CrepeDuChef.Application.Resources;
using CrepeDuChef.Domain.Entities;
using CrepeDuChef.Domain.Interfaces;
using Microsoft.Extensions.Localization;

namespace CrepeDuChef.Application.Services
{
    public class CrepePartyService : ICrepePartyService
    {
        private readonly ICrepePartyRepository _repo;
        private readonly IStringLocalizer<CrepePartyResources> _localizer;

        public CrepePartyService(
            ICrepePartyRepository repo,
            IStringLocalizer<CrepePartyResources> localizer)
        {
            _repo = repo;
            _localizer = localizer;
        }

        public async Task<IEnumerable<CrepePartySession>> GetSessionsAsync()
        {
            List<CrepesParty> crepes = await _repo.GetAllCrepePartiesAsync();
            List<User> chefs = await _repo.GetAllChefsAsync();

            var chefsById = chefs.ToDictionary(c => c.Id);

            return crepes
                .GroupBy(cp => cp.SessionNumber)
                .OrderByDescending(cp => cp.Key)
                .Select(group => CrepePartyMapper.ToSession(group, chefsById, _localizer));
        }

        public async Task AddCrepePartyAsync(CrepesPartyDto dto)
        => await _repo.AddCrepePartyAsync(dto.ToEntity());

        public async Task<List<UserDto>> GetAllChefsAsync()
        {
            return [.. (await _repo.GetAllChefsAsync()).Select(u => u.ToDto())];
        }
    }
}
