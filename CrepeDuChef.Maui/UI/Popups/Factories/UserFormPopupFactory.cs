using CommunityToolkit.Maui.Views;
using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Maui.UI.Popups.Views;
using CrepeDuChef.ViewModels.ViewModels;

namespace CrepeDuChef.Maui.UI.Popups.Factories
{
    public class UserFormPopupFactory : IUserFormPopupFactory
    {
        public Popup CreateAddUserForm()
            => new UserFormPopup(ChefEditorViewModel.ForAdd());

        public Popup CreateUpdateUserForm(ChefDto chef)
            => new UserFormPopup(ChefEditorViewModel.ForEdit(chef));
    }
}
