using CrepeDuChef.ViewModels.ViewModels;

namespace CrepeDuChef.Maui.MVVM.Views;

public partial class CrepeSessionsView : ContentPage
{
    private readonly CrepeSessionsViewModel _vm;

    public CrepeSessionsView(CrepeSessionsViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.OnAppearingAsync();
    }
}
