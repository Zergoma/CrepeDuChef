using CrepeDuChef.Maui.UI.Popups.Models;
using CrepeDuChef.Maui.UI.Popups.Views;

namespace CrepeDuChef.Maui.UI.Popups.Factories
{
    public class MessagePopupFactory : IMessagePopupFactory
    {
        public MessagePopup Create(string title, string message, MessageKind kind)
            => new(title, message, kind);
    }
}
