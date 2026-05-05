using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Models;

namespace CrepeDuChef.Application.Interfaces
{
    public interface ICrepePartyService
    {
        Task<IEnumerable<CrepePartySession>> GetSessionsAsync();
        Task AddCrepePartyAsync(CrepesPartyDto dto, Guid deviceId);
        Task<List<UserDto>> GetAllChefsAsync();
    }
}
