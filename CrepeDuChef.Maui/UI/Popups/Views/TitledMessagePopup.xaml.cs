using CommunityToolkit.Maui.Views;
using CrepeDuChef.Localization.Resources;


namespace CrepeDuChef.Maui.UI.Popups.Views;

public partial class TitledMessagePopup : Popup
{
    public string Title { get; init; } = "";
    public string Message { get; init; } = "";
    public Color IconColor { get; set; } = Colors.Transparent;
    public string OkMsg => UiResources.Ok;
    public TitledMessagePopup(string title, string message)
	{
        Title = title;
        Message = message;
        InitializeComponent();
        BindingContext = this;
    }

    private async void OkButton_Clicked(object sender, EventArgs e)
    {
        await CloseAsync();
    }
}