using CrepeDuChef.Application;
using CrepeDuChef.Common.DTOs;
using CrepeDuChef.Common.Interfaces;
using CrepeDuChef.Tests.Fakes;

namespace CrepeDuChef.Tests.Helpers
{
    public static class TestData
    {
        public static List<UserDto> UsersWithIds(params int[] ids)
        {
            return [.. ids.Select(id => new UserDto { Id = id })];
        }

        public static List<CrepesPartyDto> CrepePartiesWithUserIds(params int[] ids)
        {
            return [..ids.Select(id => new CrepesPartyDto { UserId = id })];
        }

        public delegate (UserDto User, int SessionNumber) ChefAlgo(
            List<UserDto> allChefs,
            int lastSessionId,
            List<CrepesPartyDto> sessionCrepes,
            IRandomProvider random,
            List<UserDto>? availableChefs = null);      
    }
}
