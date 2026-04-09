using System.Collections.Generic;

namespace CrepeDuChef.Avalonia.Models.UI
{
    public class CrepePartyGroup_Ava
    {
        public int SessionNumber { get; set; }
        public string Title { get; set; } = "";
        public List<CrepePartyItem_Ava> Items { get; set; } = new();
    }
}
