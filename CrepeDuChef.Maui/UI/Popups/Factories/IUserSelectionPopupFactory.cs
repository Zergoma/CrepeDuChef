using CommunityToolkit.Maui.Views;
using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Maui.UI.Popups.Views;

namespace CrepeDuChef.Maui.UI.Popups.Factories
{
    public interface IUserSelectionPopupFactory
    {
        Popup CreateSelectionPopup(
            string title,
            string message,
            List<UserDto> allUsers,
            List<UserDto> selectedUsers);
    }
}
