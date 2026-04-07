using CrepeDuChef.Application.DTOs;

namespace CrepeDuChef.Maui.UI.Dialogs
{
    public interface IDialogPresenter
    {
        Task ShowWarningAsync(string title, string message);
        Task ShowMessageAsync(string title, string message);

        Task<DialogResult<List<UserDto>>> SelectUsersAsync(
            string title,
            List<UserDto> allUsers,
            List<UserDto> selectedUsers,
            string? propertyToDisplay = null);
    }
}
