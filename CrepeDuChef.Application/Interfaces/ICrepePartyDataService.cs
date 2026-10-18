using CrepeDuChef.Application.Models;
using CrepeDuChef.Application.Models.Requests.CrepeParty;

namespace CrepeDuChef.Application.Interfaces
{
    public interface ICrepePartyDataService
    {
        Task<List<CrepePartySession>> GetSessionsAsync();
        Task AddCrepePartyAsync(AddCrepePartyClientRequest request);
    }
}
