using CrepeDuChef.Application.DTOs;

namespace CrepeDuChef.Application.Interfaces
{
    public interface ICrepePartyService
    {
        /// <summary>Sessions, most recent first.</summary>
        Task<IReadOnlyList<CrepePartySessionDto>> GetSessionsAsync();
        Task AddAsync(AddCrepePartyDto crepeParty);
    }
}
