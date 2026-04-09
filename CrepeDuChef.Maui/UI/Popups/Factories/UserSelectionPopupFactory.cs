using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Maui.UI.Popups.Views;
using CommunityToolkit.Maui.Views;

namespace CrepeDuChef.Maui.UI.Popups.Factories
{
    public class UserSelectionPopupFactory : IUserSelectionPopupFactory
    {
        public Popup CreateSelectionPopup(
            string title,
            string message,
            List<UserDto> allUsers,
            List<UserDto> selectedUsers)
        {
            return new SelectUsersPopup(
                title,
                message,
                allUsers,
                selectedUsers);
        }
    }
}
