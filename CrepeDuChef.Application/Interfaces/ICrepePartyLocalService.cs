using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Models;
using CrepeDuChef.Domain.Entities;

namespace CrepeDuChef.Application.Interfaces
{
    public interface ICrepePartyLocalService
    {
        Task<List<CrepePartySession>> GetSessionsAsync();
        Task AddCrepePartyAsync(AddCrepePartyDto dto, Guid deviceId);
    }
}
