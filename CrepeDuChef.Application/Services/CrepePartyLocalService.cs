using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Application.Mappers;
using CrepeDuChef.Application.Models;
using CrepeDuChef.Domain.Entities;
using CrepeDuChef.Localization.Resources;

using Microsoft.Extensions.Localization;

namespace CrepeDuChef.Application.Services
{
    public class CrepePartyLocalService : ICrepePartyLocalService
    {
        private readonly ICrepePartyRepository _crepesRepo;
        private readonly IChefRepository _chefsRepo;
        private readonly IStringLocalizer<CrepePartyResources> _localizer;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CrepePartyLocalService(
            ICrepePartyRepository repo,
            IDateTimeProvider dateTime,
            IChefRepository chefsRepo,
            IStringLocalizer<CrepePartyResources> localizer)
        {
            _crepesRepo = repo;
            _dateTimeProvider = dateTime;
            _chefsRepo = chefsRepo;
            _localizer = localizer;
        }


        public async Task AddCrepePartyAsync(AddCrepePartyDto dto, Guid deviceId)
        {
            CrepesParty entity =
                dto.ToEntityWithoutMetadata();

            entity.Id = Guid.NewGuid();
            entity.Date = _dateTimeProvider.UtcNow;
            entity.UpdatedAt = _dateTimeProvider.UtcNow;
            entity.OriginDeviceId = deviceId;

            await _crepesRepo.AddCrepePartyAsync(entity);
        }

        public async Task<List<CrepePartySession>> GetSessionsAsync()
        {
            List<CrepesParty> crepes =
                await _crepesRepo.GetAllCrepePartiesAsync();

            List<User> chefs =
                await _chefsRepo.GetAllChefsAsync();

            var chefsById = chefs.ToDictionary(c => c.Id);

            return crepes
                .GroupBy(cp => cp.SessionNumber)
                .OrderByDescending(cp => cp.Key)
                .Select(group =>
                {
                    CrepePartySession session =
                        CrepePartyMapper.ToSession(group, chefsById);

                    session.Title = $"{_localizer["Session"]} {session.SessionNumber}";

                    foreach (var item in session.Items)
                    {
                        if (string.IsNullOrWhiteSpace(item.Name))
                        {
                            item.Name = _localizer["NoName"];
                        }
                    }

                    return session;
                })
                .ToList();
        }
    }
}
