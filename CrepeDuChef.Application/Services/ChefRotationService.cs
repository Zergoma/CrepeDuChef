using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Application.Mappers;
using CrepeDuChef.Domain.Entities;
using CrepeDuChef.Domain.Interfaces;
using CrepeDuChef.Domain.Services;

namespace CrepeDuChef.Application.Services
{
    public class ChefRotationService : IChefRotationService
    {
        private readonly IChefRepository _chefs;
        private readonly ICrepePartyRepository _crepeParties;
        private readonly IRandomProvider _random;

        public ChefRotationService(
            IChefRepository chefs,
            ICrepePartyRepository crepeParties,
            IRandomProvider random)
        {
            _chefs = chefs;
            _crepeParties = crepeParties;
            _random = random;
        }

        public async Task<ChefSelectionDto> GetNextChefAsync(IReadOnlyCollection<Guid>? availableChefIds = null)
        {
            List<Chef> allChefs = await _chefs.GetAllAsync();

            List<Chef>? availableChefs = availableChefIds is null
                ? null
                : [.. allChefs.Where(c => availableChefIds.Contains(c.Id))];

            int lastSessionNumber = await _crepeParties.GetLastSessionNumberAsync();

            List<CrepeParty> sessionCrepes = await _crepeParties.GetBySessionAsync(lastSessionNumber);

            (Chef chef, int sessionNumber) =
                ChefRotationAlgorithm.SelectNextChef(
                    allChefs,
                    lastSessionNumber,
                    sessionCrepes,
                    _random,
                    availableChefs);

            return new ChefSelectionDto(chef.ToDto(), sessionNumber);
        }
    }
}
