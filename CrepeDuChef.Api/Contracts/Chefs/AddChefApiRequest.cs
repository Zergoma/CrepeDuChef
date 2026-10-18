namespace CrepeDuChef.Api.Contracts.Chefs
{
    public record AddChefApiRequest(
        string FirstName,
        string LastName,
        Guid OriginDeviceId
    );
}
