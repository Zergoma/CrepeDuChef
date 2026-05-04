using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Extensions;
using CrepeDuChef.Application.Models;
using CrepeDuChef.Domain.Entities;
using CrepeDuChef.Localization.Resources;
using Microsoft.Extensions.Localization;

namespace CrepeDuChef.Application.Mappers
{
    public static class CrepePartyMapper
    {
        extension(CrepesPartyDto crepesPartyDto)
        {
            public CrepesParty ToEntity()
            {
                return new()
                {
                    Id = crepesPartyDto.Id,
                    Date = crepesPartyDto.Date,
                    SessionNumber = crepesPartyDto.SessionNumber,
                    UserId = crepesPartyDto.UserId,
                };
            }
        }



        public static CrepePartySession ToSession(
           IGrouping<int, CrepesParty> group,
           Dictionary<Guid, User> chefsById,
           IStringLocalizer<CrepePartyResources> localizer)
        {
            IEnumerable<CrepeDisplayItem> items = group
                .OrderByDescending(c => c.Date)
                .Select(dto => ToDisplayItem(dto, chefsById, localizer));

            return new CrepePartySession()
            {
                SessionNumber = group.Key,
                Title = $"{localizer["Session"]} {group.Key}",
                Items = [.. items],
            };
        }


        public static CrepeDisplayItem ToDisplayItem(
            CrepesParty dto,
            Dictionary<Guid, User> chefsById,
            IStringLocalizer<CrepePartyResources> localizer)
        {
            chefsById.TryGetValue(dto.UserId, out var chef);

            return new CrepeDisplayItem
            {
                Date = dto.Date,
                Name = chef?.FullName() ?? localizer["NoName"],
            };
        }
    }
}
