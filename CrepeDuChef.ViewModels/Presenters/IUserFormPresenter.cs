using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.ValueObjects;

namespace CrepeDuChef.ViewModels.Presenters
{
    public interface IUserFormPresenter
    {
        Task<UserFormData?> ShowAddUserFormAsync();
        Task<UserFormData?> ShowUpdateUserFormAsync(UserDto user);
    }
}
