using CrepeDuChef.Application.Models;
using CrepeDuChef.Avalonia.Models.UI;

namespace CrepeDuChef.Avalonia.Mappers
{
    public static class CrepeDisplayToAvaMapper
    {
        public static CrepePartyItem_Ava ToPartyItemAva(CrepeDisplayItem item)
        {
            return new CrepePartyItem_Ava()
            {
                ChefName = item.Name,
                Date = item.Date,
            };
        }
    }
}
