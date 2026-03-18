using CrepeDuChef.Common.DTOs;

namespace CrepeDuChef.Common.Interfaces
{
    public interface IChefRotationService
    {
        Task<(UserDto User, int SessionNumber)> SelectNextChefAsync(List<UserDto>? availableChefs = null);
    }
}