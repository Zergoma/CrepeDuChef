using AppDtos = CrepeDuChef.Application.DTOs;

namespace CrepeDuChef.Api.Contracts.CrepeParties
{
    public record AddCrepePartyApiRequest(
        AppDtos.AddCrepePartyDto Data
    );
}
