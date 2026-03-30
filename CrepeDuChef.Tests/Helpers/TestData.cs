using CrepeDuChef.Domain.Entities;

namespace CrepeDuChef.Tests.Helpers
{
    public static class TestData
    {
        public static List<User> UsersWithIds(params int[] ids)
        {
            return [.. ids.Select(id => new User { Id = id })];
        }

        public static List<CrepesParty> CrepePartiesWithUserIds(params int[] ids)
        {
            return [..ids.Select(id => new CrepesParty { UserId = id })];
        }    
    }
}
