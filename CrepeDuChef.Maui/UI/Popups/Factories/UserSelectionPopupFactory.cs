using CommunityToolkit.Maui.Views;
using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Maui.UI.Popups.Views;

namespace CrepeDuChef.Maui.UI.Popups.Factories
{
    public class UserSelectionPopupFactory : IUserSelectionPopupFactory
    {
        public Popup CreateSelectionPopup(
            string title,
            string message,
            IReadOnlyList<ChefDto> allChefs,
            IReadOnlyList<ChefDto> selectedChefs)
        {
            return new SelectUsersPopup(title, message, allChefs, selectedChefs);
        }
    }
}
