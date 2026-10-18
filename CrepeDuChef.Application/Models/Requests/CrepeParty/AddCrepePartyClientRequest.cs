using AppDtos = CrepeDuChef.Application.DTOs;
namespace CrepeDuChef.Application.Models.Requests.CrepeParty
{
    public record AddCrepePartyClientRequest(
        AppDtos.AddCrepePartyDto Data
    );
}
