using CommunityToolkit.Maui.Views;
using CrepeDuChef.Maui.UI.Popups.Models;
using CrepeDuChef.ViewModels.ViewModels;

namespace CrepeDuChef.Maui.UI.Popups.Views
{
    public partial class SelectUsersPopup : Popup<ChefSelectionPopupResult?>
    {
        public SelectUsersPopup(SelectChefsDialogViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
            vm.RequestClose += async chefs =>
                await CloseAsync(chefs is null ? null : new ChefSelectionPopupResult(chefs));
        }
    }
}
