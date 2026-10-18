using CrepeDuChef.Maui.UI.Popups.Models;
using CrepeDuChef.Maui.UI.Popups.Views;

namespace CrepeDuChef.Maui.UI.Popups.Factories
{
    public interface IMessagePopupFactory
    {
        MessagePopup Create(string title, string message, MessageKind kind);
    }
}
