namespace CrepeDuChef.ViewModels.Models
{
    /// <summary>
    /// A session and its entries. Being a list itself, it can feed grouped lists directly
    /// (MAUI CollectionView with IsGrouped, or a nested ItemsControl bound to the group).
    /// </summary>
    public sealed class CrepePartySessionGroup : List<CrepePartyEntryItem>
    {
        public int SessionNumber { get; }
        public string Title { get; }

        public CrepePartySessionGroup(int sessionNumber, string title, IEnumerable<CrepePartyEntryItem> entries)
            : base(entries)
        {
            SessionNumber = sessionNumber;
            Title = title;
        }
    }
}
