using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Application.Mappers;
using CrepeDuChef.Application.ValueObjects;
using CrepeDuChef.Domain.Entities;
using DomainInterface = CrepeDuChef.Domain.Interfaces;
using DomainServices = CrepeDuChef.Domain.Services;

namespace CrepeDuChef.Application.Services
{
    public class ChefRotationService : IChefRotationService
    {
        private readonly ICrepePartyRepository _repo;
        private readonly DomainInterface.IRandomProvider _random;

        public ChefRotationService(
            ICrepePartyRepository repo,
            DomainInterface.IRandomProvider random)
        {
            _repo = repo;
            _random = random;
        }

        public async Task<ChefSelectionResult> GetNextChefAsync(List<UserDto>? availableChefs = null)
        {
            List<User> allChefs =
                await _repo.GetAllChefsAsync();

            List<Guid>? allowedIds =
                availableChefs?.Select(u => u.Id).ToList();

            List<User>? availableChefsEntities =
                allowedIds == null ? null
                                   : [.. allChefs.Where(u => allowedIds.Contains(u.Id))];

            int lastSessionId =
                await _repo.GetLastSessionNumberAsync();

            List<CrepesParty> sessionCrepes =
                await _repo.GetCrepePartiesFromSessionAsync(lastSessionId);

            (User user, int sessionNumber) =
                DomainServices.ChefRotationAlgorithm.SelectNextChef(
                    allChefs,
                    lastSessionId,
                    sessionCrepes,
                    _random,
                    availableChefsEntities);
            
            return new ChefSelectionResult(user.ToDto(), sessionNumber);
        }
    }
}
