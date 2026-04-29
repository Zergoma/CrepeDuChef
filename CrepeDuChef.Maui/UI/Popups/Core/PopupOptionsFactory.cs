using CommunityToolkit.Maui;
using Microsoft.Maui.Controls.Shapes;

namespace CrepeDuChef.Maui.UI.Popups.Core
{
    public class PopupOptionsFactory
    {
        private static Color OverlayColor =>
            App.Current!.RequestedTheme == AppTheme.Dark
                ? Colors.Black.WithAlpha(0.5f)
                : Colors.White.WithAlpha(0.5f);

        public PopupOptions Create()
        {
            return new PopupOptions
            {
                Shape = new RoundRectangle
                {
                    CornerRadius = new CornerRadius(12),
                    Stroke = Colors.Transparent,
                    StrokeThickness = 0
                },
                Shadow = null,
                PageOverlayColor = OverlayColor
            };
        }
    }
}
