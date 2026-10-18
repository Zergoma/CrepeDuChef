using CommunityToolkit.Maui.Views;
using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Maui.UI.Popups.Views;
using CrepeDuChef.ViewModels.ViewModels;

namespace CrepeDuChef.Maui.UI.Popups.Factories
{
    public class ChefFormPopupFactory : IChefFormPopupFactory
    {
        public Popup CreateAddForm()
            => new ChefFormPopup(ChefEditorViewModel.ForAdd());

        public Popup CreateUpdateForm(ChefDto chef)
            => new ChefFormPopup(ChefEditorViewModel.ForEdit(chef));
    }
}
