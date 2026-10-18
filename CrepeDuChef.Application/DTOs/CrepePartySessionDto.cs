namespace CrepeDuChef.Application.DTOs
{
    public sealed record CrepePartySessionDto
    {
        public int SessionNumber { get; init; }

        /// <summary>Entries of the session, most recent first.</summary>
        public IReadOnlyList<CrepePartyEntryDto> Entries { get; init; } = [];
    }
}
