using CrepeDuChef.ViewModels.ViewModels;

namespace CrepeDuChef.Maui.MVVM.Views;

public sealed partial class UserSettingView : ContentPage
{
    private readonly ChefSettingsViewModel _vm;

    public UserSettingView(ChefSettingsViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.LoadChefsCommand.ExecuteAsync(null);
    }
}
