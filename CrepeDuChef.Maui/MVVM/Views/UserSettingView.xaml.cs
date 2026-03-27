using CrepeDuChef.Maui.MVVM.ViewModels;

namespace CrepeDuChef.Maui.MVVM.Views;

public sealed partial class UserSettingView : ContentPage
{
    private bool _isInit = false;
    public UserSettingView(UserSettingViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        if(_isInit is false)
        {
            _isInit = true;
            if (BindingContext is UserSettingViewModel vm)
                vm.UpdateUserCommand.Execute(null);
        }
    }
}