using CrepeDuChef.Common.Models;

namespace CrepeDuChef.Common.Interfaces
{
    public interface ICrepePartyService
    {
        Task<IEnumerable<CrepePartySession>> GetSessionsAsync();
    }
}
