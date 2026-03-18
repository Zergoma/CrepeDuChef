using CrepeDuChef.Common.DTOs;

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