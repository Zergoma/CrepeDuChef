using CrepeDuChef.Application.Models;
using CrepeDuChef.Maui.Models.UI;

namespace CrepeDuChef.Maui.Mappers
{
    public static class CrepePartySessionToGroupMapper
    {
        public static IEnumerable<CrepePartyGroup> MapToGroups(IEnumerable<CrepePartySession> sessions)
        {
            IEnumerable<CrepePartyGroup> groups =
                sessions.Select(s => {
                    return new CrepePartyGroup(s.Items)
                    {
                        Title = s.Title,
                        SessionNumber = s.SessionNumber,
                    };
                });
            return groups;
        }
    }
}
