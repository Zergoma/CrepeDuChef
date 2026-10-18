using CrepeDuChef.Application.DTOs;

namespace CrepeDuChef.Maui.UI.Popups.Models
{
    /// <summary>
    /// Result of <see cref="Views.SelectUsersPopup"/>; wraps the list so it can be the popup's XAML type argument.
    /// </summary>
    public sealed record ChefSelectionPopupResult(IReadOnlyList<ChefDto> Chefs);
}
