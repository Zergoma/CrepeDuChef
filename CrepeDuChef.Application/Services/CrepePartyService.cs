using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Application.Mappers;
using CrepeDuChef.Application.Models;
using CrepeDuChef.Localization.Resources;
using CrepeDuChef.Domain.Entities;

using Microsoft.Extensions.Localization;

namespace CrepeDuChef.Application.Services
{
    public class CrepePartyService : ICrepePartyService
    {
        private readonly ICrepePartyRepository _repo;
        private readonly IStringLocalizer<CrepePartyResources> _localizer;
        private readonly IDeviceIdProvider _deviceId;
        private readonly IDateTimeProvider _dateTime;

        public CrepePartyService(
            ICrepePartyRepository repo,
            IStringLocalizer<CrepePartyResources> localizer,
            IDeviceIdProvider deviceId,
            IDateTimeProvider dateTime)
        {
            _repo = repo;
            _localizer = localizer;
            _deviceId = deviceId;
            _dateTime = dateTime;
        }

        public async Task<IEnumerable<CrepePartySession>> GetSessionsAsync()
        {
            List<CrepesParty> crepes = await _repo.GetAllCrepePartiesAsync();
            List<User> chefs = await _repo.GetAllChefsAsync();

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
                });
        }

        public async Task AddCrepePartyAsync(CrepesPartyDto dto)
        {
            CrepesParty entity =
                dto.ToEntity();

            entity.Id = Guid.NewGuid();
            entity.UpdatedAt = _dateTime.UtcNow;
            entity.OriginDeviceId = _deviceId.DeviceId;

            await _repo.AddCrepePartyAsync(entity);
        }

        public async Task<List<UserDto>> GetAllChefsAsync()
        {
            return [.. (await _repo.GetAllChefsAsync()).Select(u => u.ToDto())];
        }
    }
}
