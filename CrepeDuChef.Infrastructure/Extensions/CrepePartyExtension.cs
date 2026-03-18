using CrepeDuChef.Common.DTOs;
using CrepeDuChef.Infrastructure.Entities;

namespace CrepeDuChef.Infrastructure.Extensions
{
    public static class CrepePartyExtension
    {
        extension(CrepesParty crepeParty)
        {
            public CrepesPartyDto ToDto()
            {
                return new()
                {
                    Id = crepeParty.Id,
                    Date = crepeParty.Date,
                    SessionNumber = crepeParty.SessionNumber,
                    UserId = crepeParty.UserId,
                };
            }
        }
    }
}
