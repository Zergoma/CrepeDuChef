namespace CrepeDuChef.Application.DTOs
{
    /// <summary>
    /// Records that a chef made crepes; the date is set by the application, not the caller.
    /// </summary>
    public sealed record AddCrepePartyDto
    {
        public Guid ChefId { get; init; }
        public int SessionNumber { get; init; }
    }
}
