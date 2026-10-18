using CrepeDuChef.ViewModels.ViewModels;

namespace CrepeDuChef.Maui.MVVM.Views;

public partial class CrepeDuChefView : ContentPage
{
    private readonly CrepeSessionsViewModel _vm;

    public CrepeDuChefView(CrepeSessionsViewModel vm)
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
