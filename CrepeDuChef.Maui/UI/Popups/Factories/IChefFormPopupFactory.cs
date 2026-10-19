using CommunityToolkit.Maui.Views;
using CrepeDuChef.Application.DTOs;

namespace CrepeDuChef.Maui.UI.Popups.Factories
{
    public interface IChefFormPopupFactory
    {
        Popup CreateAddForm();
        Popup CreateUpdateForm(ChefDto chef);
    }
}
