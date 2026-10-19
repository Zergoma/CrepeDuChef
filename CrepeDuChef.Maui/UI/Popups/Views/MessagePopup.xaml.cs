using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Views;
using CrepeDuChef.Localization.Resources;
using CrepeDuChef.Maui.UI.Popups.Models;

namespace CrepeDuChef.Maui.UI.Popups.Views;

public partial class MessagePopup : Popup
{
    public string Title { get; }
    public string Message { get; }
    public string IconSource { get; }
    public Color IconColor { get; }
    public string OkMsg => UiResources.Ok;

    public MessagePopup(string title, string message, MessageKind kind)
    {
        Title = title;
        Message = message;
        (IconSource, IconColor) = Appearance(kind);

        InitializeComponent();
        BindingContext = this;

        KindIcon.Behaviors.Add(new IconTintColorBehavior { TintColor = IconColor });
    }

    private static (string Icon, Color Color) Appearance(MessageKind kind)
    {
        bool dark = Microsoft.Maui.Controls.Application.Current?.RequestedTheme == AppTheme.Dark;

        string key = kind switch
        {
            MessageKind.Success => "Success",
            MessageKind.Warning => "Warning",
            _ => "Error",
        };

        Color color = Microsoft.Maui.Controls.Application.Current?.Resources.TryGetValue(key + (dark ? "Dark" : "Light"), out object? value) == true
            ? (Color)value
            : Colors.Gray;

        return ($"icon_{key.ToLowerInvariant()}.png", color);
    }

    private async void OkButton_Clicked(object sender, EventArgs e)
        => await CloseAsync();
}
