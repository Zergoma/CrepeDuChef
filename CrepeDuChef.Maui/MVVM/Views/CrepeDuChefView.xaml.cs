using CrepeDuChef.Maui.MVVM.ViewModels;

namespace CrepeDuChef.Maui.MVVM.Views;

public partial class CrepeDuChefView : ContentPage
{
	public CrepeDuChefView(CrepeSessionsViewModel_MauiAdapter vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
		if(BindingContext is CrepeSessionsViewModel_MauiAdapter vm)
		{
			await vm.Shared.OnAppearingAsync();
		}
    }
}