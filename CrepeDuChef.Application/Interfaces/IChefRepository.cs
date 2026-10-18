using CrepeDuChef.Domain.Entities;

namespace CrepeDuChef.Application.Interfaces
{
    public interface IChefRepository
    {
        Task AddChefAsync(User user);
        Task UpdateChefAsync(User user);
        Task<List<User>> GetAllChefsAsync();
        Task<User?> GetChefAsync(Guid id);
    }
}