using System;

namespace CrepeDuChef.Avalonia.Models.UI
{
    public class CrepePartyItem_Ava
    {
        public string ChefName { get; set; } = "";
        public DateTime Date { get; set; }
        public DateTime DateLocal => Date.ToLocalTime();

    }
}
