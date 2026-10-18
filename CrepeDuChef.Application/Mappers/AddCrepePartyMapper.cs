using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Domain.Entities;

namespace CrepeDuChef.Application.Mappers
{
    public static class AddCrepePartyMapper
    {
        extension(AddCrepePartyDto addCrepeDto)
        {
            public CrepesParty ToEntityWithoutMetadata()
            =>
                new()
                {
                    Date = addCrepeDto.Date,
                    SessionNumber = addCrepeDto.SessionNumber,
                    UserId = addCrepeDto.UserId,
                };
        }
    }
}
