using CommunityToolkit.Maui.Views;
using CrepeDuChef.Application.DTOs;

namespace CrepeDuChef.Maui.UI.Popups.Factories
{
    public interface IUserFormPopupFactory
    {
        Popup CreateAddUserForm();
        Popup CreateUpdateUserForm(UserDto user);
    }
}
