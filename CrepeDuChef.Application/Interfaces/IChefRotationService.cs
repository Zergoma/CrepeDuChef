using CrepeDuChef.Application.DTOs;

namespace CrepeDuChef.Application.Interfaces
{
    public interface IChefRotationService
    {
        /// <param name="availableChefIds">Chefs present today; null means everyone is available.</param>
        Task<ChefSelectionDto> GetNextChefAsync(IReadOnlyCollection<Guid>? availableChefIds = null);
    }
}
