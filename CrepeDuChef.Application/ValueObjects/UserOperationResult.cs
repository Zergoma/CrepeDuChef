using CrepeDuChef.Application.DTOs;

namespace CrepeDuChef.Application.ValueObjects
{
    public record UserOperationResult(
        OperationStatus Status,
        string ErrorMessage,
        UserDto? User
    );
}
