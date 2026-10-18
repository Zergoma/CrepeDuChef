using CommunityToolkit.Maui.Views;
using CrepeDuChef.Application.DTOs;

namespace CrepeDuChef.Maui.UI.Popups.Factories
{
    public interface IChefSelectionPopupFactory
    {
        Popup CreateSelectionPopup(
            string title,
            string message,
            IReadOnlyList<ChefDto> allChefs,
            IReadOnlyList<ChefDto> selectedChefs);
    }
}
