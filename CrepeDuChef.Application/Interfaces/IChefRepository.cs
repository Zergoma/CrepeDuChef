using CrepeDuChef.Domain.Entities;

namespace CrepeDuChef.Application.Interfaces
{
    public interface IChefRepository
    {
        Task AddAsync(Chef chef);
        Task UpdateAsync(Chef chef);
        Task<Chef?> GetByIdAsync(Guid id);
        Task<List<Chef>> GetAllAsync();
    }
}
