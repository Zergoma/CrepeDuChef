using CrepeDuChef.ViewModels.ViewModels;

namespace CrepeDuChef.Maui.MVVM.Views;

public sealed partial class ChefSettingsView : ContentPage
{
    private readonly ChefSettingsViewModel _vm;

    public ChefSettingsView(ChefSettingsViewModel vm)
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
