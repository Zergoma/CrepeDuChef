namespace CrepeDuChef.Application.Models.Requests.Chefs
{
    public record AddChefClientRequest(
        string FirstName,
        string LastName,
        Guid OriginDeviceId
    );
}
