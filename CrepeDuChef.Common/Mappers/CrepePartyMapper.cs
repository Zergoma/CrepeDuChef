using CrepeDuChef.Common.DTOs;
using CrepeDuChef.Common.Extensions;
using CrepeDuChef.Common.Interfaces;
using CrepeDuChef.Common.Models;

namespace CrepeDuChef.Common.Mappers
{
    public class CrepePartyMapper
    {
        public static CrepePartySession ToSession(
            IGrouping<int, CrepesPartyDto> group,
            Dictionary<int, UserDto> chefsById,
            ITradCrepePartyDefault trad)
        {
            IEnumerable<CrepeDisplayItem> items = group
                .OrderByDescending(c => c.Date)
                .Select(dto => ToDisplayItem(dto, chefsById, trad));

            return new CrepePartySession()
            {
                SessionNumber = group.Key,
                Title = $"{trad.Session} {group.Key}",
                Items = [.. items],
            };
        }

        public static CrepeDisplayItem ToDisplayItem(
            CrepesPartyDto dto,
            Dictionary<int, UserDto> chefsById,
            ITradCrepePartyDefault trad)
        {
            chefsById.TryGetValue(dto.UserId, out var chef);

            return new CrepeDisplayItem
            {
                Date = dto.Date,
                Name = chef?.FullName() ?? trad.NoName,
            };
        }
    }
}
