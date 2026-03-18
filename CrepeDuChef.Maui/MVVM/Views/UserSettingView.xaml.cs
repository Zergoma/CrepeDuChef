using CrepeDuChef.Common.DTOs;
using CrepeDuChef.Maui.MVVM.ViewModels;

namespace CrepeDuChef.Maui.MVVM.Views;

public sealed partial class UserSettingView : ContentPage
{
    public UserSettingView(UserSettingViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is UserSettingViewModel vm)
            vm.UpdateUserCommand.Execute(null);
    }
}