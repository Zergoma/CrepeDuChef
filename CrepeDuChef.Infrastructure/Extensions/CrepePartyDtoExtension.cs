using CrepeDuChef.Common.DTOs;
using CrepeDuChef.Infrastructure.Entities;

namespace CrepeDuChef.Infrastructure.Extensions
{
    public static class CrepePartyDtoExtension
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
    }
}
