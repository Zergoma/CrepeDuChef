using CommunityToolkit.Maui.Views;
using CrepeDuChef.Application.ValueObjects;
using CrepeDuChef.Maui.UI.Popups.ViewModels;

namespace CrepeDuChef.Maui.UI.Popups.Views;

public partial class UserFormPopup : Popup<UserFormData?>
{
    private readonly UserFormPopupViewModel _vm;

    public UserFormPopup(
        string firstName = "",
        string lastName = "",
        string title = "",
        string validateButtonText = "")
    {
        InitializeComponent();
        _vm = new UserFormPopupViewModel(firstName, lastName, title, validateButtonText);
        BindingContext = _vm;
    }

    private async void ButtonCancel_Clicked(object sender, EventArgs e)
        => await CloseAsync(null);
    

    private async void ButtonOk_Clicked(object sender, EventArgs e)
        => await CloseAsync(_vm.ToResult());
}