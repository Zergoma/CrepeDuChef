using CrepeDuChef.Application.Models;
using CrepeDuChef.Avalonia.Models.UI;
using System.Collections.Generic;
using System.Linq;

namespace CrepeDuChef.Avalonia.Mappers
{
    public class CrepePartySessionToGroupMapper_Ava
    {
        public static List<CrepePartyGroup_Ava> MapToGroups(IEnumerable<CrepePartySession> sessions)
        {
            return sessions
                .Select(s => new CrepePartyGroup_Ava
                {
                    Title = s.Title,
                    SessionNumber = s.SessionNumber,
                    Items = s.Items
                            .Select(item => CrepeDisplayToAvaMapper.ToPartyItemAva(item))
                            .ToList()
                })
                .ToList();
        }
    }
}
