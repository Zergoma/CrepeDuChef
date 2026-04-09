using CrepeDuChef.Application.DTOs;

namespace CrepeDuChef.ViewModels.Presenters
{
    public interface IUserSelectionPresenter
    {
        Task<UserDto[]?> SelectUsersAsync(
            string title,
            string message,
            List<UserDto> allUsers,
            List<UserDto> selectedUsers);
    }
}
