using CrepeDuChef.Maui.UI.Popups.Views;

namespace CrepeDuChef.Maui.UI.Popups.Factories
{
    public interface IMessagePopupFactory
    {
        TitledMessagePopup Create(string title, string message, Color iconColor);
    }
}
