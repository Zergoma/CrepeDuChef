using CrepeDuChef.Maui.MVVM.ViewModels;

namespace CrepeDuChef.Maui.MVVM.Views;

public partial class CrepeDuChefView : ContentPage
{
	public CrepeDuChefView(CrepeDuChefViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    protected override async void OnAppearing()
    {
		if(BindingContext is CrepeDuChefViewModel vm)
		{
			await vm.OnAppearingAsync();
		}
    }
}