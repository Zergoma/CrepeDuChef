using CommunityToolkit.Maui.Views;
using CrepeDuChef.Application.DTOs;

namespace CrepeDuChef.Maui.UI.Popups.Factories
{
    public interface IUserSelectionPopupFactory
    {
        Popup CreateSelectionPopup(
            string title,
            string message,
            IReadOnlyList<ChefDto> allChefs,
            IReadOnlyList<ChefDto> selectedChefs);
    }
}
