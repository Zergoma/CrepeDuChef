using CrepeDuChef.Common.Models;

namespace CrepeDuChef.Application.Interfaces
{
    public interface ICrepePartyService
    {
        Task<IEnumerable<CrepePartySession>> GetSessionsAsync();
    }
}
