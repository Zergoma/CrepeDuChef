using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Localization.Resources;
using CrepeDuChef.ViewModels.Models;

namespace CrepeDuChef.ViewModels.Mappers
{
    public static class CrepePartySessionMapper
    {
        extension(CrepePartySessionDto session)
        {
            public CrepePartySessionGroup ToGroup() => new(
                session.SessionNumber,
                $"{UiResources.Session} {session.SessionNumber}",
                session.Entries.Select(e => new CrepePartyEntryItem(
                    e.ChefName ?? UiResources.NoName,
                    e.DateUtc)));
        }
    }
}
