namespace CrepeDuChef.Common.Models
{
    public class CrepeDisplayItem
    {
        public string  Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public DateTime DateLocal => Date.ToLocalTime();
    }
}
