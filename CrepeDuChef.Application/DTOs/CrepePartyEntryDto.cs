namespace CrepeDuChef.Application.DTOs
{
    public sealed record CrepePartyEntryDto
    {
        public Guid ChefId { get; init; }

        /// <summary>Null when the chef no longer exists.</summary>
        public string? ChefName { get; init; }

        public DateTime DateUtc { get; init; }
    }
}
