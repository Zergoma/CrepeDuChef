namespace CrepeDuChef.Application.DTOs
{
    public record AddCrepePartyDto(
        DateTime Date,
        int SessionNumber,
        Guid UserId
    );
}
