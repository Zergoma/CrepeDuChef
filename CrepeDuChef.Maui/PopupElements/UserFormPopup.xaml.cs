using CommunityToolkit.Maui.Views;
using CrepeDuChef.Common.DTOs;

namespace CrepeDuChef.Maui.PopupElements;

public partial class UserFormPopup : Popup<UserDto?>
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;
    
    public string Title { get; set; } = string.Empty;
    public string ValidateButtonText { get; set; } = string.Empty;


    public UserFormPopup(string firstName = "", string lastName = "", string title = "", string validateButtonText = "")
    {
        FirstName = firstName;
        LastName = lastName;
        Title = title;
        ValidateButtonText = validateButtonText;
        InitializeComponent();
        BindingContext = this;
    }

    private async void ButtonCancel_Clicked(object sender, EventArgs e)
    {
        await CloseAsync(null);
    }

    private async void ButtonOk_Clicked(object sender, EventArgs e)
    {
        UserDto user = new()
        {
            FirstName = FirstName,
            LastName = LastName,
        };

        await CloseAsync(user);
    }
    private void UpdateOkButtonState()
    {
        ButtonOk.IsEnabled =
            !string.IsNullOrWhiteSpace(FirstName) &&
            !string.IsNullOrWhiteSpace(LastName);
    }

    private void FirstNameEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        FirstName = e.NewTextValue;
        UpdateOkButtonState();
    }

    
    private void LastNameEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        LastName = e.NewTextValue;
        UpdateOkButtonState();
    }
}