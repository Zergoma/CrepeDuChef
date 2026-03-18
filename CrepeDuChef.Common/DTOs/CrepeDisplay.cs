namespace CrepeDuChef.Common.DTOs
{
    public class CrepeDisplay
    {
        public string  Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public DateTime DateLocal => Date.ToLocalTime();
    }
}
