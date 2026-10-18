using CrepeDuChef.Application.Common;
using CrepeDuChef.Application.DTOs;

namespace CrepeDuChef.Application.Interfaces
{
    public interface IChefService
    {
        Task<IReadOnlyList<ChefDto>> GetAllAsync();
        Task<Result<ChefDto>> AddAsync(AddChefDto chef);
        Task<Result<ChefDto>> UpdateAsync(UpdateChefDto chef);
    }
}
