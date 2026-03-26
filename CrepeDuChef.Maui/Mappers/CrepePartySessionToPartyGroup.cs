using CrepeDuChef.Common.Models;
using CrepeDuChef.Maui.MVVM.Models;

namespace CrepeDuChef.Maui.Mappers
{
    public static class CrepePartySessionToPartyGroup
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
