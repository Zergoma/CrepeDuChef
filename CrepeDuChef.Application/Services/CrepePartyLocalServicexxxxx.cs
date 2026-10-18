using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Application.Mappers;
using CrepeDuChef.Application.Models;
using CrepeDuChef.Application.Models.Requests.CrepeParty;
using CrepeDuChef.Domain.Entities;
using CrepeDuChef.Localization.Resources;

using Microsoft.Extensions.Localization;

namespace CrepeDuChef.Application.Services
{
    public class CrepePartyLocalServicexxxxx : ICrepePartyDataService
    {
        private readonly ICrepePartyRepository _crepesRepo;
        private readonly IChefRepository _chefsRepo;
        private readonly IStringLocalizer<CrepePartyResources> _localizer;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CrepePartyLocalServicexxxxx(
            ICrepePartyRepository repo,
            IStringLocalizer<CrepePartyResources> localizer,
            IDateTimeProvider dateTime,
            IChefRepository chefsRepo)
        {
            _crepesRepo = repo;
            _localizer = localizer;
            _dateTimeProvider = dateTime;
            _chefsRepo = chefsRepo;
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

        public async Task AddCrepePartyAsync(AddCrepePartyClientRequest request)
        {
            AddCrepePartyDto dto
                = request.Data;

            CrepesParty entity =
                dto.ToEntityWithoutMetadata();

            entity.Id = Guid.NewGuid();
            entity.UpdatedAt = _dateTimeProvider.UtcNow;
            //entity.OriginDeviceId = dto.OriginDeviceId;

            await _crepesRepo.AddCrepePartyAsync(entity);
        }

        //public async Task<List<UserDto>> GetAllChefsAsync()
        //{
        //    return [.. (await _chefsRepo.GetAllChefsAsync()).Select(u => u.ToDto())];
        //}
    }
}
