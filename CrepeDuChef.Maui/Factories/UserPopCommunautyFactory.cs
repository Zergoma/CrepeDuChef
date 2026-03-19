using CommunityToolkit.Maui.Views;
using CrepeDuChef.Common.DTOs;
using CrepeDuChef.Common.Interfaces;
using CrepeDuChef.Maui.PopupElements;
using CrepeDuChef.Maui.Resources.languages;

namespace CrepeDuChef.Maui.Factories
{
    public class UserPopCommunautyFactory : IUserPopupFactory<Popup, UserDto>
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
