using CrepeDuChef.Common.DTOs;
using CrepeDuChef.Common.Interfaces;

namespace CrepeDuChef.Application.Services
{
    public class ChefRotationService : IChefRotationService
    {
        private readonly ICrepePartyRepository _repo;
        private readonly IRandomProvider _random;

        public ChefRotationService(ICrepePartyRepository Repo, IRandomProvider random)
        {
            _repo = Repo;
            _random = random;
        }

        public async Task<(UserDto User, int SessionNumber)> SelectNextChefAsync(List<UserDto>? availableChefs = null)
        {
            List<UserDto> allChefs = await _repo.GetAllChefsAsync();

            int lastSessionId = await _repo.GetLastSessionNumberAsync();

            List<CrepesPartyDto> sessionCrepes =
                await _repo.GetCrepePartiesFromSessionAsync(lastSessionId);

            return ChefRotationAlgorithm.SelectNextChef(
                allChefs,
                lastSessionId,
                sessionCrepes,
                _random,
                availableChefs);
        }
    }
}
