using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.ValueObjects;

namespace CrepeDuChef.Maui.UI.Popups.Presenters
{
    public interface IUserPopupPresenter
    {
        Task ShowAbortedOperationAsync(string message);
        Task ShowUpdateSuccessedOperationAsync();
        Task<UserFormData?> ShowAddUserFormAsync();
        Task<UserFormData?> ShowUpdateUserFormAsync(UserDto usr);
    }
}