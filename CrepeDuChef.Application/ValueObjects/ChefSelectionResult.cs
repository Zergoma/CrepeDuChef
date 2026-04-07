using CrepeDuChef.Application.DTOs;

namespace CrepeDuChef.Application.ValueObjects
{
    public record ChefSelectionResult(UserDto User, int SessionNumber);
}