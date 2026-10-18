using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Domain.Entities;

namespace CrepeDuChef.Application.Mappers
{
    public static class CrepePartyMapper
    {
        public static CrepePartySessionDto ToSessionDto(
            IGrouping<int, CrepeParty> session,
            IReadOnlyDictionary<Guid, Chef> chefsById)
        {
            return new CrepePartySessionDto
            {
                SessionNumber = session.Key,
                Entries = [.. session
                    .OrderByDescending(cp => cp.Date)
                    .Select(cp => cp.ToEntryDto(chefsById))],
            };
        }

        extension(CrepeParty crepeParty)
        {
            public CrepePartyEntryDto ToEntryDto(IReadOnlyDictionary<Guid, Chef> chefsById) => new()
            {
                ChefId = crepeParty.ChefId,
                ChefName = chefsById.TryGetValue(crepeParty.ChefId, out Chef? chef) ? chef.FullName : null,
                DateUtc = crepeParty.Date,
            };
        }
    }
}
