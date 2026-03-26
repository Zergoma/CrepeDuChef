using CrepeDuChef.Common.Models;
using System.Collections.ObjectModel;

namespace CrepeDuChef.Maui.MVVM.Models
{
    public partial class CrepePartyGroup : ObservableCollection<CrepeDisplayItem>
    {
        public int SessionNumber { get; set; }
        public string Title { get; set; } = string.Empty;

        public CrepePartyGroup(IEnumerable<CrepeDisplayItem> items) : base(items)
        {
        }
    }
}
