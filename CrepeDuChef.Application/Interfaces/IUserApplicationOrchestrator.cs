using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.ValueObjects;

namespace CrepeDuChef.Application.Interfaces
{
    public interface IUserApplicationOrchestrator
    {
        Task<UserOperationResult> AddUserAsync(UserFormData userFormData);
        Task<UserOperationResult> UpdateUserAsync(UserDto user, UserFormData userFormData);
    }
}
