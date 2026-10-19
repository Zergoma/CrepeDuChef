using CommunityToolkit.Maui.Views;
using CrepeDuChef.ViewModels.Models;
using CrepeDuChef.ViewModels.ViewModels;

namespace CrepeDuChef.Maui.UI.Popups.Views;

public partial class ChefFormPopup : Popup<ChefFormData?>
{
    public ChefFormPopup(ChefEditorViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        vm.RequestClose += async result => await CloseAsync(result);
    }
}
