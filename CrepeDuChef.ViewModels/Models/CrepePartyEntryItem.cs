namespace CrepeDuChef.ViewModels.Models
{
    public sealed class CrepePartyEntryItem
    {
        public string ChefName { get; }
        public DateTime DateUtc { get; }
        public DateTime DateLocal => DateUtc.ToLocalTime();

        public CrepePartyEntryItem(string chefName, DateTime dateUtc)
        {
            ChefName = chefName;
            DateUtc = dateUtc;
        }
    }
}
