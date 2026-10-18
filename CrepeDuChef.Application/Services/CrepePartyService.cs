using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Application.Mappers;
using CrepeDuChef.Domain.Entities;

namespace CrepeDuChef.Application.Services
{
    public class CrepePartyService : ICrepePartyService
    {
        private readonly ICrepePartyRepository _crepeParties;
        private readonly IChefRepository _chefs;
        private readonly IDeviceIdProvider _deviceId;
        private readonly IDateTimeProvider _dateTime;

        public CrepePartyService(
            ICrepePartyRepository crepeParties,
            IChefRepository chefs,
            IDeviceIdProvider deviceId,
            IDateTimeProvider dateTime)
        {
            _crepeParties = crepeParties;
            _chefs = chefs;
            _deviceId = deviceId;
            _dateTime = dateTime;
        }

        public async Task<IReadOnlyList<CrepePartySessionDto>> GetSessionsAsync()
        {
            List<CrepeParty> crepeParties = await _crepeParties.GetAllAsync();
            Dictionary<Guid, Chef> chefsById = (await _chefs.GetAllAsync()).ToDictionary(c => c.Id);

            return [.. crepeParties
                .GroupBy(cp => cp.SessionNumber)
                .OrderByDescending(session => session.Key)
                .Select(session => CrepePartyMapper.ToSessionDto(session, chefsById))];
        }

        public async Task AddAsync(AddCrepePartyDto crepeParty)
        {
            CrepeParty entity = new()
            {
                Id = Guid.NewGuid(),
                ChefId = crepeParty.ChefId,
                SessionNumber = crepeParty.SessionNumber,
                Date = _dateTime.UtcNow,
                UpdatedAt = _dateTime.UtcNow,
                OriginDeviceId = _deviceId.DeviceId,
            };

            await _crepeParties.AddAsync(entity);
        }
    }
}
