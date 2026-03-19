using CrepeDuChef.Common.DTOs;
using CrepeDuChef.Common.Models;

namespace CrepeDuChef.Common.Interfaces
{
    public interface IUserDtoPopupService
    {
        Task ShowAbortedOperationAsync(string message);
        Task ShowUpdateSuccessedOperationAsync();
        Task<UserDataResult> ShowAddUserFormAsync();
        Task<UserDataResult> ShowUpdateUserFormAsync(UserDto usr);
    }
}