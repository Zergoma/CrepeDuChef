namespace CrepeDuChef.Common.Models
{
    public class CrepePartySession
    {
        public int SessionNumber { get; init; }
        public string Title { get; set; } = string.Empty;
        public IReadOnlyList<CrepeDisplayItem> Items { get; init; } = [];
    }
}
