using CommunityToolkit.Maui.Views;
using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Maui.UI.Popups.Views;
using CrepeDuChef.ViewModels.ViewModels;

namespace CrepeDuChef.Maui.UI.Popups.Factories
{
    public class ChefSelectionPopupFactory : IChefSelectionPopupFactory
    {
        public Popup CreateSelectionPopup(
            string title,
            string message,
            IReadOnlyList<ChefDto> allChefs,
            IReadOnlyList<ChefDto> selectedChefs)
        {
            return new SelectChefsPopup(new SelectChefsDialogViewModel(title, message, allChefs, selectedChefs));
        }
    }
}
