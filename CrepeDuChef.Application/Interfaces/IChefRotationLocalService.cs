using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.ValueObjects;

namespace CrepeDuChef.Application.Interfaces
{
    public interface IChefRotationLocalService
    {
        Task<ChefSelectionResult> GetNextChefAsync(List<UserDto>? availableChefs = null);
    }
}
