using AppDtos = CrepeDuChef.Application.DTOs;

namespace CrepeDuChef.Api.Contracts.CrepeParties
{
    public record AddCrepePartyRequest(
        Guid DeviceId,
        AppDtos.CrepesPartyDto Data
    );
}
