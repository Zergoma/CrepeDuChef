using CrepeDuChef.Maui.UI.Popups.Views;

namespace CrepeDuChef.Maui.UI.Popups.Factories
{
    public class MessagePopupFactory : IMessagePopupFactory
    {
        public TitledMessagePopup Create(string title, string message, Color iconColor)
        {
            return new TitledMessagePopup(title, message)
            {
                IconColor = iconColor
            };
        }
    }
}
