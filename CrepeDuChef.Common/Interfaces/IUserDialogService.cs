using CrepeDuChef.Common.DTOs;

namespace CrepeDuChef.Common.Interfaces
{
    public interface IUserDialogService
    {
        Task ShowWarningAsync(string title, string message);
        Task ShowMessageAsync(string title, string message);

        Task<IEnumerable<UserDto>?> SelectUsersAsync(
            string title,
            IEnumerable<UserDto> allUsers,
            IEnumerable<UserDto> selectedUsers,
            string propertyToDisplay);
    }
}
