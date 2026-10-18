using CrepeDuChef.Domain.Entities;

namespace CrepeDuChef.Application.Interfaces
{
    public interface IChefLocalService
    {
        Task<List<User>> GetAllChefsAsync();
        Task<User> AddChefAsync(string firstName, string lastName, Guid OriginDeviceId);
        Task<User?> UpdateChefAsync(Guid id, string firstName, string lastName);
    }
}
