using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.ValueObjects;

namespace CrepeDuChef.Application.Interfaces
{
    public interface IUserOrchestrator
    {
        Task<UserOperationResult> AddUserAsync(UserFormData userFormData);
        Task<UserOperationResult> UpdateUserAsync(UserDto user, UserFormData userFormData);
    }
}
