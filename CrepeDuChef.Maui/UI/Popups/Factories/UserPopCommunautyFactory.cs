using CommunityToolkit.Maui.Views;
using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Localization.Resources.languages;
using CrepeDuChef.Maui.UI.Popups.Views;

namespace CrepeDuChef.Maui.UI.Popups.Factories
{
    public class UserPopCommunautyFactory : IUserFormPopupFactory
    {
        public Popup CreateAddUserForm()
        {
            return new UserFormPopup(
                title: Traduction.AddAChef,
                validateButtonText: Traduction.Add);
        }

        public Popup CreateUpdateUserForm(UserDto user)
        {
            return new UserFormPopup(
                firstName: user.FirstName,
                lastName: user.LastName,
                title: Traduction.UpdateAChef,
                validateButtonText: Traduction.Update);
        }
    }
}
