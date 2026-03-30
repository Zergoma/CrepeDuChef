using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Application.Mappers;
using CrepeDuChef.Application.ValueObjects;
using CrepeDuChef.Domain.Entities;
using CrepeDuChef.Domain.Interfaces;
using CrepeDuChef.Domain.Services;

namespace CrepeDuChef.Application.Services
{
    public class ChefRotationService : IChefRotationService
    {
        private readonly ICrepePartyRepository _repo;
        private readonly IRandomProvider _random;

        public ChefRotationService(ICrepePartyRepository repo, IRandomProvider random)
        {
            _repo = repo;
            _random = random;
        }

        public async Task<ChefSelectionResult> GetNextChefAsync(List<UserDto>? availableChefs = null)
        {
            List<User>? availableChefsEntities =
                availableChefs?.Select(u => u.ToEntity()).ToList();

            List<User> allChefs =
                await _repo.GetAllChefsAsync();

            int lastSessionId =
                await _repo.GetLastSessionNumberAsync();

            List<CrepesParty> sessionCrepes =
                await _repo.GetCrepePartiesFromSessionAsync(lastSessionId);

            (User user, int sessionNumber) =
                ChefRotationAlgorithm.SelectNextChef(
                    allChefs,
                    lastSessionId,
                    sessionCrepes,
                    _random,
                    availableChefsEntities);
            
            return new ChefSelectionResult(user.ToDto(), sessionNumber);
        }
    }
}
