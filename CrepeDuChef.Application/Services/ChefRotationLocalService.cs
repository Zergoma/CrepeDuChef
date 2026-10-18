using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Extensions;
using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Application.Mappers;
using CrepeDuChef.Application.ValueObjects;
using CrepeDuChef.Domain.Entities;
using CrepeDuChef.Domain.Interfaces;
using CrepeDuChef.Domain.Services;

namespace CrepeDuChef.Application.Services
{
    public class ChefRotationLocalService : IChefRotationLocalService
    {
        private readonly ICrepePartyDomainLocalService _localCrepes;
        private readonly IChefLocalService _localChefs;
        private readonly IRandomProvider _random;

        public ChefRotationLocalService(
            IChefLocalService localChefs,
            ICrepePartyDomainLocalService localCrepes,
            IRandomProvider random)
        {
            _localChefs = localChefs;
            _localCrepes = localCrepes;
            _random = random;
        }

        public async Task<ChefSelectionResult> GetNextChefAsync(List<UserDto>? availableChefs = null)
        {
            List<User> allChefs =
                await _localChefs.GetAllChefsAsync();

            List<Guid>? allowedIds =
                availableChefs?.Select(c => c.Id).ToList();

            List<User>? availableEntities = 
                allowedIds == null
                ? null
                : allChefs.Where(c => allowedIds.Contains(c.Id)).ToList();

            int lastSession =
                await _localCrepes.GetLastSessionNumberAsync();

            List<CrepesParty> sessionCrepes =
                await _localCrepes.GetCrepePartiesFromSessionAsync(lastSession);

            var (user, sessionNumber) = ChefRotationAlgorithm.SelectNextChef(
                allChefs,
                lastSession,
                sessionCrepes,
                _random,
                availableEntities
            );

            return new ChefSelectionResult(
                user.ToDto(),
                sessionNumber);
        }
    }
}
