using CrepeDuChef.Common.DTOs;
using CrepeDuChef.Infrastructure.Entities;

namespace CrepeDuChef.Infrastructure.Extensions
{
    public static class UserExtension
    {
        extension(User user)
        {
            public string AllToString()
            {
                return user.FirstName + " " + user.LastName + " ID: " + user.Id;
            }

            public UserDto ToDto()
            {
                return new UserDto()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                };
            }
        }
    }
}
