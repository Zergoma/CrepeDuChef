using CrepeDuChef.Common.DTOs;
using CrepeDuChef.Domain.Entities;

namespace CrepeDuChef.Application.Mappers
{
    public static class CrepePartyMapper
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
